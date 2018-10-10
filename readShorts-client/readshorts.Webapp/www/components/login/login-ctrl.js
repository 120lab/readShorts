	var shortsCtrls = angular.module('shorts.controllers');

shortsCtrls.controller('LoginCtrl', ['$scope', '$state', '$stateParams', '$location', '$ionicModal', '$ionicPopup', 'shorts.serverAPI', '$ionicHistory', 'ShortsUserEvents', 
	function($scope, $state, $stateParams, $location, $ionicModal, $ionicPopup, serverAPI, $ionicHistory, shortsUserEvents) {
	console.log('inside login controller');

	var loginRetryDelay = 10; //login retray delay in seconds
	var loginRetry = null;
	$scope.status = "";

	/**
	*	login() - hanles the logic of logging a created/pending user to the system. 
	*			 In case of login success, switch to shorts main page. 
	*			On failure, retry again after some delay 
	*		The function receives two parameters:
	* @param i_email - user email address
	* @param i_nextRoute - the next route/state to go into after a successful login
	*/
	function login(i_email,i_nextRoute) {
		var nextRoute = i_nextRoute;
		console.log('trying to login to server');
		var email=i_email;

		serverAPI.getUserPassword(email,function(err,data) {
			if (!err) {
				console.log('password data = ',data);
				var password=data.token;

				serverAPI.emailLogin(email, password,function(err,data) {
					if (!err) {
						$scope.status="";  //cleqr status message

						console.log('loginCompleted. token = ',password);

						serverAPI.eventUserAccount(shortsUserEvents.UserLoginToApp, "");

						loginRetry = null; //

						//if user is already registered in the system, we can skip the personal setting 
						// and redirect him to 'shorts' page
						if (data.Users[0].UserInProfilePage && nextRoute == 'personal-info')
							nextRoute = 'shorts';

						//go to shorts main page
						if (nextRoute) {
							$scope.closeModal();
							$state.go(nextRoute);
						}
					}
					else {	
						console.log('failed logging in. retry in ',loginRetryDelay,' seconds');
						$scope.status = "Failed logging in";
					}
				});
			}
			else {
				setTimeout(function() {login(email, nextRoute)}, loginRetryDelay *1000 );
			}
		});
	}

	//Try loggin into facebook. if succesfull, we get the facebook response object 
	// which holds various user information pieces of data we can use for to 
	// continue the 'user create' process into Shorts.
	$scope.facbookLogin = function() {
		$scope.status = "Connecting using Facebook";
		serverAPI.facebookLogin(function(error, response) {
			if (error) {
				/* START DEBUG CODE */
				console.log('FB Error occured',error);
				/* END DEBUG CODE */
				$scope.status="Can't login using facebook";
			} else {
				var newUserConfiguration = {
					"UserSecurityNumber": "",
					"FirstName": response.first_name,
					"LastName": response.last_name,
					"ShortBio": "",
					"PicturePath": "",
					"ClientIP": "",
					"EmailAddress": response.email,
					"MobileSerialNumber": "",
					"IsAnonimousConnect": false,
					"IsFBConnect": true, // If fb connect
					"IsTWConnect": false, // Future 
					"IsGGLConnect": false,// Future 
					"IsEmailConnect": false, // If email connect
					"BirthDate":"",
					"EmailForShortIMightLike": false, // Email settings
					"EmailForShortOfTheWeek": false, // Email settings
					"EmailForShortFollowingWriter": false,// Email settings
					"EmailForNewSAndUpdates": false,// Email settings
					"ExternalLink": "",// For writer only
					"ExternalLinkText": "",// For writer only
					"LUSubscriptionTypeKey": 2, // Require see lookup
					"LUGenderKey": 1, // Require see lookup
					"LUCountryKey": 103 // Require seelookup
				};
				serverAPI.createUser(newUserConfiguration, function(status,data) {
					console.log('createUser CB');
					//if create user succesful, notify the user to check his email
					//and start the login rettries process
					if (status==null) {
						$scope.status = "";
						$ionicPopup.alert({
						     template: '<div style="text-align: center;font-weight: bold;font-size: 1.2rem;">Signup completed</div>'
						}).then(function(res) {
							login(response.email,'shorts');
							serverAPI.eventUserAccount(shortsUserEvents.UserLoginToAppByFacebookAccount, "");
						});
					}
					else {
						$scope.status = "Sign up failed";
					}
				});
			}
		});
	}

	//login using email. 
	$scope.emailLogin = function(i_emailAddress) {
		$scope.emailAddress = i_emailAddress;
		console.log("email address=",$scope.emailAddress," i_emailAddress = ",i_emailAddress);
		if ($scope.emailAddress=="" || $scope.emailAddress==null) {
			//TODO - add email validation (maybe on the html level)
			$scope.status = "Please enter a valid mail";
			return;
		}
		else
			$scope.status = "";

		//set the new user configuration object
		var newUserConfiguration = {
			"UserSecurityNumber": "",
			"FirstName": "",
			"LastName": "",
			"ShortBio": "",
			"PicturePath": "",
			"ClientIP": "",
			"EmailAddress": $scope.emailAddress,
			"MobileSerialNumber": "",
			"IsAnonimousConnect": false,
			"IsFBConnect": false, // If fb connect
			"IsTWConnect": false, // Future 
			"IsGGLConnect": false,// Future 
			"IsEmailConnect": true, // If email connect
			"BirthDate":"",
			"EmailForShortIMightLike": true, // Email settings
			"EmailForShortOfTheWeek": true, // Email settings
			"EmailForShortFollowingWriter": true,// Email settings
			"EmailForNewSAndUpdates": true,// Email settings
			"ExternalLink": "",// For writer only
			"ExternalLinkText": "",// For writer only
			"LUSubscriptionTypeKey": 2, // Require see lookup
			"LUGenderKey": 1, // Require see lookup
			"LUCountryKey": 103 // Require seelookup
		};
		serverAPI.createUser(newUserConfiguration,function(status,data) {
			//if create user succesful, notify the user to check his email
			//and get user password before starting the login rettries process
			if (status==null) {
				$scope.status = "";
				$ionicPopup.alert({
				     template: '<div style="text-align: center;font-weight: bold;font-size: 1rem;">Please check your email to complete registration</div>'
				}).then(function(res) {
					login($scope.emailAddress,'personal-info');
					serverAPI.eventUserAccount(shortsUserEvents.UserLoginToAppByEmail, "");
				});
//				$scope.status = "Please check your email to complete registration";
			}
			else {
				$scope.status = "Sign up failed";
			}
		});
	}

	$scope.close = function() {
		$scope.closeModal();
	}

	$scope.navigateBack = function() {
		switch ($ionicHistory.backView().stateName) {
			case 'welcome': 
				$ionicHistory.nextViewOptions({   // clear back history
				  disableBack: true
				});
				$state.go('shorts');  //navigate to shorts
				break;
			case 'shorts':
				$ionicHistory.goBack(); //navigate back
				break;
			default:
				$ionicHistory.goBack(); //navigate back
		}
  	};
}]);

