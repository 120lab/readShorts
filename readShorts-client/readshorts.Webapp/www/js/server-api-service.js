angular.module('shorts.services',[])
.factory('shorts.serverAPI' , ['$http', '$q', '$cookies', '$window', '$ionicLoading','ShortsUserEvents', function($http, $q, $cookies, $window, $ionicLoading,UserEvents) { 

	/* START DEBUG CODE */
	console.log('Inside shorts Server API service');
	/* END DEBUG CODE */
	var allowedShortTypes = ['0', '1', '2', '3'];
	var numberOfShortsToRetrieve = '4'	;

	var lookupTablesToDownload = ['LUEventTypes', 'LUGenders', 'LUCountries', 'LUShortAgeRestrictions','LUSysInterfaceLanguages'];
	var lookupTables = [];	//store all the lookup tabled received from the server
	var lookupTablesReady=false;

	var userData = null; //holds user settings when logged in. 


	var SERVER_API_URL_DEV = 'http://readshortswebclientdev.azurewebsites.net';
	var SERVER_API_URL_INTEGRATION = 'http://readshortsgatewayappint.azurewebsites.net'
	var SERVER_API_URL_PRODUCTION = 'http://readshortsappgateway.azurewebsites.net';

	var SERVER_API_URL = SERVER_API_URL_PRODUCTION;
	var SHORT_SERVER_URL = "http://app.readshorts.com";

	var ANONYMOUS_RECORD_KEY = 1;  //record key for anonymous user

	downloadLookUpTables();

	/**
	* downloadLookUpTables - download the lookup tables from the server. 
	*						The list of tables to download is stored in 'lookupTablesToDownload'. This function
	*						download asynchronously each table and store it in the'llokupTable' array for later use
	*/
	function downloadLookUpTables() {
		$ionicLoading.show();

		var numberOfLookupTables=lookupTablesToDownload.length;
		var receivedLookupTables=0;

		for (var i=0;i<numberOfLookupTables;i++) {
			$http({
	                method: 'GET',
	                url: SERVER_API_URL+'/api/Invoker?controller=Lookup&param=query.lUSysInterfaceLanguageKey%3D1%26query.tableName%3D'+lookupTablesToDownload[i]
	            }).success(function(response) {

					console.log('LU TAble',response);
					//save lookup tables
					lookupTables[response.TableName] = response.Lookups;

					receivedLookupTables++;
					if (receivedLookupTables == 5) {
						lookupTablesReady = true;
						console.log('all LUTables are loaded', lookupTables);
						$ionicLoading.hide();
					}
				}).error(function(err) {
					//console.log(err);
	                $ionicLoading.hide();
	                console.log("Server Error - Can't get Lookup table");
				});
        }
	};

	function readCookie(i_tokenName) {
		var token = $cookies.get(i_tokenName);

		return token;
	}

	function writeCookie(i_tokenName, i_value) {
		var now = new Date();
        var expireDate = now.getTime();
        expireDate += 2*365*24*60*60*1000;  //set expiration date to two years....
		$cookies.put(i_tokenName, i_value,  {'expires': new Date(expireDate)});
	}

	var serverAPI = {
		/** 
		* perform initialization:
		* check if user has already loggedin in the past (we have the user email store internally)
		*/
		init: function() {
			var deferred = $q.defer();

			//check if we have the user's email address stored as a cookie
			var userEmailAddress = readCookie('shortsEmailToken')
			var shortsUserData = readCookie('shortsUserData');
			if  (shortsUserData != null)
				userData= JSON.parse(shortsUserData);

			if (userEmailAddress != null) {
				// Re-write the cookie  to re-set its expiration date 
				writeCookie('shortsEmailToken',userEmailAddress);

				serverAPI.getUserPassword(userEmailAddress, function(err, data) {
					if (!err) {
						serverAPI.emailLogin(userEmailAddress, data.token,function(err,data) {
							if (!err) {
								serverAPI.eventUserAccount(shortsUserEvents.UserLoginToApp, "");
								console.log('Logged in silently.....');
								deferred.resolve(data);
							}
							else {	
								console.log('Error - failed Logging in silently.....');
								deferred.reject(err);
							}
						});
					} else {
						console.log('Error - Couldn\'t get user token');
						deferred.reject(err);
					}
				})
		    	return deferred.promise;
			} else
				return $q.reject();
		},


		/**
		* isLoggedIn - check if a user is loggedin
		*/
		isLoggedIn: function() {
			if (userData == null)
				return false;
			else 
				return true;
		},


		/**
		* emailLogin - perform user login based on email
		*				Since login process could have retries in case of failure, 
		*				it is up to the calling function to call the $ionicLoading.hide()
		*				in case of login failure. 
		*/
		emailLogin: function(i_userEmail, i_password, i_loggedInCB) {
			
			console.log('trying to log in user ',i_userEmail);
			$ionicLoading.show();
			
			$http({
                method: 'POST',
                url: SERVER_API_URL+'/api/users/Login',
                data: {"identity": i_userEmail,"password": i_password}
            }).success(function(response) {
            	console.log('emailLogin success. header=',response);

				//user is now logged in, so store the user information for later use.
				userData = response.Users[0]; 
				writeCookie('shortsEmailToken',i_userEmail);
				writeCookie('shortsUserData',JSON.stringify(userData));

	            $ionicLoading.hide();

                i_loggedInCB(null,response);

            }).error(function(err) {
            	//No hiding of $ionicLoading in this case
                console.log(err);
                i_loggedInCB(err,'error getting shorts ');
            });					

		},

		/** 
		*	facebookLogin - use facebook API to create the login process. User is redirected to Facebook's
		*					 APP/website, to confirm the login. If succesfull, facebook API provides the 
		*					user's email.
		*/ 
		facebookLogin: function(i_facebookLoginCB) {
			//Call facebook API for login, asking for public information and email address
			FB.login(function(response){
				if (response.status === 'connected') {
				    // Logged in into Facebook.
			        //get user data
			        FB.api('/me?fields=email,first_name,last_name,birthday', function(response) {
					    console.log(JSON.stringify(response));
					    if (!response || response.error) {
							i_facebookLoginCB('Error getting FB user data',response);
						} else {
							//Got FB user information. Now we can continue with short user create. 
							i_facebookLoginCB(null,response);
						}
					});

				} else if (response.status === 'not_authorized') {
					// The person is logged into Facebook, but not your app.
					i_facebookLoginCB('FB user not authorized');
				} else {
					// The person is not logged into Facebook, so we're not sure if
				    // they are logged into this app or not.
					i_facebookLoginCB('user not logged in into FB');
				}
			}, {scope: 'email'});
		},

		/**
		 *	Get 'shorts' items from the server by type.
		 * The following types are supported:
		 * 0 AllFeed 
		 * 1-Bookmarked 
		 * 2-TopLikes
		 * 3-FollowedWriters
		 * 
		*/
		getShorts: function(i_type, i_shortId, i_shortsReceivedCB) {
			console.log('i_type: ',i_type);
			//$ionicLoading.show();

			var user;

			if (userData)
				user =userData.UserName;
			else
				user = '@anonymous';
			
			var requestData = { UserName: user, ShortItemsAmount: numberOfShortsToRetrieve, ShortsFeedTypeItem: i_type };
			if (i_shortId)
				requestData.FirstShortKey = i_shortId;
			
			$http({
                method: 'POST',
                url: SERVER_API_URL+'/api/Invoker?method=MatchShortUserAccount',
                data: requestData
            }).success(function(response) {

	          //  $ionicLoading.hide();
                i_shortsReceivedCB(null,response);

            }).error(function(err) {
              //  $ionicLoading.hide();
                i_shortsReceivedCB(err,'error getting shorts ');
            });
		},

		/**
		* Update the sign that Short Read/view/bookmarked/next/read time/followed writer etc. by user.
		* 	SERVER_API_URL+'/api/Invoker?method=ShortUserAccount'
		*
		*	The user activity is provided in the following object:
		*	{
		*	  "ShortSendToUser": true, // update when shorts send to user 
		*	  "ShortViewByUser": true, // update when the user view the short
		*	  "ShortReadByUserTimeInMiliSeconds": 0, // update when the user read the inner text
		*	  "ShortSignAsLike": true, // update when user sign like to short
		*	  "ShortSignAsBookmark": true, update when user bookmarked the short
		*	  "UserSignNextShort": true, update when user sign the next button
		*	  "UserSignWriterAsFollowed": true,// update when user follow a writer
		*	  "ShortKey": 0,
		*	  "UserAccountKey": 0
		*	}
		*/
		shortUserAccount: function(i_userActivity, i_userActivityUpdatedCB) {
			var recordKey;

			if (userData)
				recordKey = userData.RecordKey;
			else //user not logged in, set record key to Anonymous record key
				recordKey = ANONYMOUS_RECORD_KEY;

			i_userActivity.UserAccountKey = recordKey;

			$http({
                method: 'POST',
                url: SERVER_API_URL+'/api/Invoker?method=ShortUserAccount',
                data: i_userActivity
            }).success(function(response) {
	            //$ionicLoading.hide();
                i_userActivityUpdatedCB(null,response);

            }).error(function(err) {
                console.log(err);
                //$ionicLoading.hide();
                i_userActivityUpdatedCB(err,'error updating user activity on account ');
            });

		},

		/**
		*	Create User - Creates a new user
		*/
		createUser: function(i_newUserConfiguration, i_createUserCB) {
			$ionicLoading.show();

			$http({
                method: 'POST',
                url: SERVER_API_URL+'/api/Invoker?method=UserAccount',
                data: i_newUserConfiguration
            }).success(function(response) {
            	console.log(response);
	            $ionicLoading.hide();
                i_createUserCB(null,response);

            }).error(function(err) {
                console.log(err);
                $ionicLoading.hide();
                i_createUserCB(err,'error getting shorts ');
            });

		},

		/**
		*	Update User - Update existing user information
		*/
		updateUser: function(i_updatedUserConfiguration, i_updateUserCB) {
			$ionicLoading.show();

			$http({
                method: 'PUT',
                url: SERVER_API_URL+'/api/Invoker?method=UserAccount',
                data: i_updatedUserConfiguration
            }).success(function(response) {

	            $ionicLoading.hide();
	            userData = response.Users[0];   //update user data localy. 
                i_updateUserCB(null,response);

            }).error(function(err) {
                console.log(err);
                $ionicLoading.hide();
                i_updateUserCB(err,'error getting shorts ');
            });
		},

		/**
		*	sendMail - Sends contact-us information to the back end server.
		*				The user fills this information in the 'Contact-Us' page.
		*				We use the general perpose 'GeneralTask' API to send it 
		*				to the backend sever
		*/
		sendContactUsData: function(i_contactUsData) {
			var contactUsData_str=JSON.stringify(i_contactUsData);
			$http({
                method: 'GET',
                url: SERVER_API_URL+'/api/Invoker?controller=GeneralTasks&param=query.message='+contactUsData_str+',query.currentTask=0'
            }).success(function(response) {

                console.log("send contactus data success",response);

            }).error(function(err) {
                console.log("send contactus data ERROR",err);
            });
		},

		getUserPassword: function(i_userEmail, i_getUserPwdCB) {
			if (i_userEmail == null || i_userEmail=="") {
				i_getUserPwdCB('error','Illegal email');
				return;
			}
			console.log('getting password for user ',i_userEmail);

			$http({
                method: 'GET',
                url: SERVER_API_URL+'/api/Invoker?controller=UserAccount&param=identity='+i_userEmail
            }).success(function(response) {
            	console.log('token=',response.token);

            	//set the 'sessionId' token in HTTP headers
       			$http.defaults.headers.common['sessionId'] = response.token;

                i_getUserPwdCB(null,response);

            }).error(function(err) {
                console.log(err);
                i_getUserPwdCB(err,'error getting user password ');
            });
		},

		/** 
		* 	deleteUser - Used for debugging only. SHould be removed from code in production
		* 				deletes the user from the database 
		*/
		deleteUser: function(i_userDeletedCB) {

        	userData = null;
        	writeCookie('shortsEmailToken',null);
        	writeCookie('shortsUserData',null);
            console.log("user deleted");
            i_userDeletedCB(null);

		},

		/** 
		* 	eventUserAccount - Update an event from user each action the user do we need to record with this method
		*				@param: "LUEventTypeKey": 
		*				@param: "AdditionalData": "string",
		*/
		eventUserAccount: function(i_LUEventTypeKey, i_AdditionalData) {
			var recordKey;

			if (userData)
				recordKey = userData.RecordKey;
			else //user not logged in, set record key to Anonymous record key
				recordKey = ANONYMOUS_RECORD_KEY;
			$http({
                method: 'POST',
                url: SERVER_API_URL+'/api/Invoker?method=EventUserAccount',
                data: { 
				UserAccountKey: recordKey,
				LUEventTypeKey: i_LUEventTypeKey,
				AdditionalData: i_AdditionalData 
                }
            }).success(function(response) {
//                console.log('@@@@ eventUserAccount',response);
            }).error(function(err) {
                console.log('@-@-@-@ eventUserAccount',err);
            });
		},

		/**
		* getUserInformation - retrieve the user information. This data is received from the server 
		*						on the response object of a succesful email login, and is stored in userData 
		*						object. 
		*						This API just exposes this object to other controllers as necessary
		*/
		getUserInformation: function() {
			return userData;
		},


		/**
		*	getLookUpTable - returns one or all the lookup tables
		*					if 'i_lookupTableName' is set to null, the function return all the downloaded
		*					lookup tables. 
		*					Otherwise, it return the table name provided by 'i_lookupTableName'
		*/
		getLookupTable: function(i_lookupTableName) {
			//Check if all tables are available
			//if (!lookupTablesReady)
			//	return null;

			if (i_lookupTableName==null)
				return lookupTables;

			if (i_lookupTableName)
				return lookupTables[i_lookupTableName];
		},

		/**
		*	getServerUrl - returns the server address storing shorts
		*/
		getServerUrl: function() {
			return SHORT_SERVER_URL;
		}
	};

	return serverAPI;

}]);