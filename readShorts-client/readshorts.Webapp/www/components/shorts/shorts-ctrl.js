var shortsCtrls = angular.module('shorts.controllers', []);

shortsCtrls.controller('ShortsCtrl', ['$rootScope','$scope', '$state', '$stateParams', '$location', '$window', '$ionicModal', '$timeout', 'shorts.serverAPI', 'Socialshare', '$ionicScrollDelegate','shorts.login','$ionicPlatform', function($rootScope,$scope, $state, $stateParams, $location, $window, $ionicModal, $timeout, serverAPI, socialshare, $ionicScrollDelegate,login,$ionicPlatform) {
	console.log('inside shorts controller');

	//Shorts page shows a list of shorts based on one of the following categories:
	var feedType = {shorts:{displayName:'Shorts', serverTypeValue: '0'},
				bookmarks: {displayName:'Bookmarks', serverTypeValue: '1'} ,
				topShorts: {displayName:'Top', serverTypeValue: '2'},
				followedShorts:{displayName:'Followed', serverTypeValue:'3'},
				favorites:{displayName:'My favorites', serverTypeValue:'4'},
				shared:{displayName:'Friends share', serverTypeValue:'5'}
				};

	//list of all RTL languages. Used for setting the text direction on screen
	// 2 - hebrew
	//currently single language 
	var rtlLanguages =[2];
	
	var showCard = false;

	$scope.menuActive = false;
	$scope.menuButtonColor = 'red';

	//hide/show footer on text input (addressing Android keyboard issue)
	$scope.hideFooter = false;

	//for displaying Feed Counter on navbar
	$scope.currentShort = null
	$scope.totalShorts = null;

	$scope.greeting = "";
	$scope.userName = "";

	$scope.cards={};
	$scope.cards.active=[];

	$scope.plmData = null;

	$scope.shortsReady = false; 

	$scope.selectedFeedType = $stateParams.feedType || 'shorts';
//	$scope.selectedShortId = $stateParams.shortId || '';
	$scope.selectedShortId = $location.hash();

	//load shorts when controller starts
	getShorts(feedType[$scope.selectedFeedType].serverTypeValue, $scope.selectedShortId);

	$scope.showMainMenuButton = $location.path() === '/shorts';

	$scope.shortShareActive = false;	

	//check here for all RTL languages.
	//If the short language is set to one of the RTL languages, 
	//return true
	$scope.isRtlText = function(i_index) {
		if ($scope.cards.active == null || $scope.cards.active[i_index] == null)
			return false;

	    for (i = 0; i < rtlLanguages.length; i++) {
	        if (rtlLanguages[i] === $scope.cards.active[i_index].LUSysInterfacelanguageKey) {
	            return true;
	        }
	    }
	    return false;
	}

	$scope.contentIsScrolling = function(ev) {
		var viewScrollPosition = $ionicScrollDelegate.getScrollPosition().top;
		$rootScope.$broadcast('scroll-pos', { yPos: viewScrollPosition });
	}

	$scope.isUserLoggedIn = function() {
		return serverAPI.isLoggedIn();
	}


	$scope.closeMainMenuAndLogin = function() {
		$scope.closeMainMenuModal();
		login.show();
	}

	//Return a user greeting based on current time
	$scope.getGreeting = function() {
		var currentTime =  new Date().getHours();
		if (currentTime >=3 && currentTime< 12)
			return 'Good Morning';
		if (currentTime >=12 && currentTime< 18)
			return 'Good Afternoon';
		if (currentTime >=18 && currentTime< 22)
			return 'Good Evening';
		if (currentTime >=22 || currentTime< 3)
			return 'Good Night';
	}

	$scope.getFontSize = function() {
		return $rootScope.shortFontSize;
	}

	/**
	* Logout - perform usr logout by actually deleting user credentials and navigating to welcome screen
	*/
	$scope.logout = function() {
		serverAPI.deleteUser(function(err) {
			if (!err) {
				console.log('delete user succeded');
				$scope.closeMainMenuModal();
				$state.go('welcome');
			}	
			else
				console.log('delete user failed');
		});	
	}


	//get loggine use name. 
	$scope.getUserName = function() {
		var userInfo = serverAPI.getUserInformation();
		if (!userInfo)
			return "";
		else
			return userInfo.FirstName	;
	}

	//get the name of the current feed being displayed.
	$scope.getFeedName = function() {
		return feedType[$scope.selectedFeedType].displayName;
	};

	/**
	* shortTypeSelected - Called each time the user select different short type 
	* 					  from the main menu.
	*					  This close the menu and use the ui-router to navigate 
	*					  to the 'short' state, passing the 'type' as a parameter to 
	* 					  the ui-router.
	*	@param - selection - an object in the format of ({feedType: type})
	*/
	$scope.shortTypeSelected = function(selection) {
		console.log('shortTypeSelected = ',selection);
		//if user is not loggedin, prevent navigation to bookmarks or 'followd' feeds
		if ((selection.feedType == 'bookmarks' || selection.feedType == 'followedShorts') && !serverAPI.isLoggedIn()) {
			$scope.closeMainMenuModal();
			login.show();
		}
		else {
			$scope.closeMainMenuModal();
			$state.go('shorts', selection);
		}
	}

	/**
	* toggleShortBookmark() - marks the short as bookmarked by the user. 
	* 						This operation is available just for logged in users, 
	*						so if the user is not logged in he is redireced to the login page
	*						if the user is logged in, the local field is set to true, to enable 
	*						immediate visual feedback to the user, and only then a request to
	*						the server is being made. 
	*						if the request to the server fails, local marking is removed. 
	*/
	$scope.toggleShortBookmark = function(i_shortKey, $event) {
		$event.stopPropagation();

		if (serverAPI.isLoggedIn() == false) {
			login.show();
			return;
		}
		if (!$scope.cards.active) {
			console.log('No shorts available');
			return;
		}

		//set it locally so it is reflected on UI 
		if ($scope.cards.active[0].ShortSignAsBookmark == true)
			$scope.cards.active[0].ShortSignAsBookmark = false;
		else {
			$scope.cards.active[0].ShortSignAsBookmark = true;
			serverAPI.eventUserAccount(shortsUserEvents.UserBookmarkedShort, i_shortKey);
		}

		//if logged in, call the server API to mark the short as 'Bookmarked'
		var userActivity = {
			"ShortSignAsBookmark": $scope.cards.active[0].ShortSignAsBookmark,
			"ShortKey" : i_shortKey,
			"UserAccountKey" : $scope.cards.active[0].UserAccountKey
		}
		serverAPI.shortUserAccount(userActivity, function(err,response) {
			if (!err) {
				//TODO  - alert the user?
			}
			else {
				console.log('failed setting as "bookmarked" ');
			}
		});
	}

	/**
	* toggleShortLike() - marks the short as 'liked' by the user. 
	* 						This operation is available just for logged in users, 
	*						so if the user is not logged in he is redireced to the login page
	*						if the user is logged in, the local field is set to true, to enable 
	*						immediate visual feedback to the user, and only then a request to
	*						the server is being made. 
	*						if the request to the server fails, local marking is removed. 
	*/
	$scope.toggleShortLike = function(i_shortKey,$event) {
		$event.stopPropagation();
		if (!$scope.cards.active) {
			console.log('No shorts available');
			return;
		}

		//set it locally so it is reflected on UI 
		if ($scope.cards.active[0].ShortSignAsLike == true)
			$scope.cards.active[0].ShortSignAsLike = false;
		else {
			$scope.cards.active[0].ShortSignAsLike = true;
			serverAPI.eventUserAccount(shortsUserEvents.UserLikedShort, i_shortKey);
		}

		//if logged in, call the server API to mark the short as 'Liked'
		var userActivity = {
			"ShortSignAsLike": $scope.cards.active[0].ShortSignAsLike,
			"ShortKey" : i_shortKey,
			"UserAccountKey" : $scope.cards.active[0].UserAccountKey
		}
		serverAPI.shortUserAccount(userActivity, function(err,response) {
			if (!err) {
				//TODO  - alert the user?
			}
			else {
				console.log('failed setting as "liked" ');
			}
		});
	}

	/**
	* toggleFollowWriter() - follow the short's writer. 
	* 						This operation is available just for logged in users, 
	*						so if the user is not logged in he is redireced to the login page
	*						if the user is logged in, the local field is set to true, to enable 
	*						immediate visual feedback to the user, and only then a request to
	*						the server is being made. 
	*						if the request to the server fails, local marking is removed. 
	*/
	$scope.toggleFollowWriter = function(i_shortKey) {
		if (serverAPI.isLoggedIn() == false) {
			login.show();
			return;
		}
		if (!$scope.cards.active) {
			console.log('No shorts available');
			return;
		}

		//set it locally so it is reflected on UI 
		if ($scope.cards.active[0].IsUserAccountWriterFollowed == true)
			$scope.cards.active[0].IsUserAccountWriterFollowed = false;
		else {
			$scope.cards.active[0].IsUserAccountWriterFollowed = true;
			serverAPI.eventUserAccount(shortsUserEvents.UserFollowedWriter, i_shortKey);
		}

		//loop through all the dowloaded cards and set it locally as well
		for (i=1;i<$scope.cards.active.length;i++) {
			if ($scope.cards.active[i].WriterUserKey == $scope.cards.active[0].WriterUserKey)
				$scope.cards.active[i].IsUserAccountWriterFollowed = $scope.cards.active[0].IsUserAccountWriterFollowed;
		}

		//if logged in, call the server API to mark the short as 'Liked'
		var userActivity = {
			"UserSignWriterAsFollowed": $scope.cards.active[0].IsUserAccountWriterFollowed,
			"ShortKey" : i_shortKey,
			"UserAccountKey" : $scope.cards.active[0].UserAccountKey
		}
		serverAPI.shortUserAccount(userActivity, function(err,response) {
			if (!err) {
				//TODO  - alert the user?
			}
			else {
				console.log('failed following writer ');
			}
		});
	}

	/** 
	* getShorts - access the server and retrieves array of shorts.
	* 			  if succesful, the $scope.short array is populated. 
	* @param - shortsType - the type pf shorts to retreive  - a value beteen 0 to 3
	* 			0 - shorts
	*			1 - bookmarks
	* 			2 - topShorts
	*			3 - followed shorts
	*/
	function getShorts(type, i_shortId) {
		serverAPI.getShorts(type, i_shortId, function(err,response) {

			//log 'getshorts' event
			serverAPI.eventUserAccount(shortsUserEvents.UserGetShorts, i_shortId);

			if (!err) {
				//place the plm number into an array with leading zeros so it can be dsiplayed in the 
				//PLM counter
				$scope.plmData = Array(Math.max(4 - String(response.PLMData).length + 1, 0)).join(0) + response.PLMData;

				console.log('got ',response.Matches.length, 'more shorts from server');
				console.log(response.Matches);
				for (var i=0;i<response.Matches.length;i++) {
					response.Matches[i].timeStamp = Date.now()+i;
					$scope.cards.active.push(response.Matches[i]);
				};

				setTimeout(function() {
					$scope.shortsReady = true; //mark that shorts are ready. 
				},100);

				//send looging event to server (by type)

				var eventCode;
				switch (eventCode) {
					case feedType.shorts.serverTypeValue: 
						eventCode = shortsUserEvents.UserInHomeFeed;
						break;
					case feedType.bookmarks.serverTypeValue: 
						eventCode = shortsUserEvents.UserInBookmarkFeed;
						break;
					case feedType.followedShorts.serverTypeValue: 
						eventCode = shortsUserEvents.UserInFollowedWriterFeed;
						break;
					case feedType.topShorts.serverTypeValue: 
						eventCode = shortsUserEvents.UserInTopFeed;
						break;
					default:
						eventCode = null;
				}
				if (eventCode != null)
					serverAPI.eventUserAccount(eventCode, "");

			} else {
				$state.go('server-error');
			}
		});
	}

	/**
	* $scope.publishShort() -
	*
	* Handle user selection for publish short
	* Currently redirect the user to a Google Doc. 
	* In the future the implementation might be different, 
	* so this function can be considered as stab function to be replace later
	*/

	$scope.publishShort = function() {
		$window.location.href = 'https://docs.google.com/forms/d/e/1FAIpQLSdEuaAXDMbXJekHjb_zoIGvcVFXxMvp4LY-mRz3rNQQBqgj9A/viewform'; 
	}

	/** 
	* $scope.gotoSocial - navigate to one of shortss social pages
	* @param - tyep - Social netwoprk type to navigate to
	*
	*/
	$scope.gotoSocial = function(type) {
		if (type=='facebook')
			$window.location.href = 'https://www.facebook.com/readshorts/?ref=aymt_homepage_panel'; 
	}


	/**************************************************/
	/* related implementation for PLM modal */
	/**************************************************/
	$scope.openPLM = function() {
		$ionicModal.fromTemplateUrl('components/plm/plm.html', {
			scope: $scope,
			animation: 'null',
			hardwareBackButtonClose: true
		}).then(function(modal) {
			$scope.plmModal = modal;
			console.log('opening PLM modal',$scope.plmModal);
			$scope.plmModal.show();
		});
	}

	$scope.closePLM = function() {
		if($scope.plmModal)
			$scope.plmModal.hide();
	}

	$scope.inviteFriends = function() {
		console.log('Invite Friends');
	}

	$scope.openShortsDetails = function(i_shortKey) {
		$scope.openTime=Date.now();
		console.log('opening shorts modal');
		$scope.shortCardOpen = true;
		serverAPI.eventUserAccount(shortsUserEvents.UserEnterToShortsText, i_shortKey);

		//set angular $location.href so Facebook plug in can have a unique id
		var shortKey = $scope.cards.active[0].ShortKey;
		$location.hash(shortKey);
		console.log('card opened: shortKey, $location.hash(), $location, $location.absUrl() =  ',shortKey, $location.hash(), $location, $location.absUrl(), $location.path())


		//close share window if still opened
		if($scope.shortShareActive)
			$scope.shortShareActive = false;

	}

	$scope.closeShortsDetails = function(i_shortKey) {
		var closeTime=Date.now();

		var userActivity = {
			"ShortReadByUserTimeInMiliSeconds": closeTime - $scope.openTime,
			"ShortKey" : i_shortKey,
			"UserAccountKey" : $scope.cards.active[0].UserAccountKey
		}
		serverAPI.shortUserAccount(userActivity, function(err,response) {
			$ionicScrollDelegate.$getByHandle('cardScroll').scrollTop();
			$scope.shortCardOpen = false;
			if (!err) {
				//TODO  - alert the user?
			}
			else {
				console.log('error updating data ');
			}
		});
	}

	$scope.scrollShortToTop = function() {
		$ionicScrollDelegate.scrollTop(true);
	}

	/**************************************************/
	/* related implementation for short share modal */
	/**************************************************/
	$scope.getFBCommentHref = function() {
		var ref=$location.absUrl();
		return  ref;
	}

	$scope.openShortsShare = function($event) {

		$scope.shortShareActive = !$scope.shortShareActive;

		if($scope.shortShareActive == false)
			return;

		serverAPI.eventUserAccount(shortsUserEvents.UserShareShort, shortKey);

		var shortKey = $scope.cards.active[0].ShortKey;
		$scope.shortShareUrl  = serverAPI.getServerUrl()+'/#/shorts/#'+shortKey;
		$scope.shortShareText = "Another great short you've got to read\n\r";

		$event.stopPropagation();
		return;
	}

	$scope.closeShortShare = function() {
		if($scope.shortShareActive) {
			$scope.shortShareActive = false;
		}
		$event.stopPropagation();
		return;		
	}

	$scope.whatsappShare = function () {

    	var href = 'whatsapp://send?text=' + encodeURIComponent($scope.shortShareText + ' ') + encodeURIComponent($scope.shortShareUrl);
		$window.location.href = href;
	}

	$scope.facebookShare = function() {
		$scope.shortShareActive = false;
		socialshare.share({
			'provider': 'facebook',
			'attrs': {
				'socialshareUrl' : $scope.shortShareUrl,
				'socialshareDescription' : $scope.shortShareText,
				'socialshareMedia':  $scope.cards.active[0].SharePicturePath,
				'socialshareType' : 'feed',
				'socialshareVia' :'1743912202523697',
				'socialshareText': "Shorts. Keep the story going."
			}
		});
		serverAPI.eventUserAccount(shortsUserEvents.UserShareShortByFaceBook, $scope.cards.active[0].ShortKey);
	}


	$scope.twitterShare = function() {
		$scope.shortShareActive = false;
		socialshare.share({
			'provider': 'twitter',
			'attrs': {
				'socialshareUrl' : $scope.shortShareUrl,
				'socialshareText' : $scope.shortShareText,
				'socialshareHashtags' : ''
			}
		});
		serverAPI.eventUserAccount(shortsUserEvents.UserShareShortByTwitter, $scope.cards.active[0].ShortKey);
	}

	$scope.linkedinShare = function() {
		$scope.shortShareActive = false;
		socialshare.share({
			'provider': 'linkedin',
			'attrs': {
				'socialshareUrl' : $scope.shortShareUrl,
				'socialshareText' : $scope.shortShareText,
				'socialshareDescription' : '',
				'socialshareSource' : $scope.cards.active[0].SharePicturePath
			}
		});
	}

	$scope.pinterestShare = function() {
		$scope.shortShareActive = false;
		socialshare.share({
			'provider': 'pinterest',
			'attrs': {
				'socialshareUrl' : $scope.shortShareUrl,
				'socialshareText' : $scope.shortShareText,
				'socialshareMedia' : $scope.cards.active[0].SharePicturePath //Maybe add the link to the image here
			}
		});
	}

	$scope.emailShare = function() {
		$scope.shortShareActive = false;
		socialshare.share({
			'provider': 'email',
			'attrs': {
				'socialshareSubject': 'Check this great Short',
				'socialshareBody' : $scope.shortShareText+' '+$scope.shortShareUrl
			}
		});
		serverAPI.eventUserAccount(shortsUserEvents.UserShareShortByMail, $scope.cards.active[0].ShortKey);
	}

	$scope.shortDownload = function($event) {
		$scope.shortShareActive = false;
		$event.stopPropagation();
		var downloadPath = $scope.cards.active[0].SharePicturePath;
		$scope.cards.active[0].ShortSignAsDownloaded = true;
		window.open(downloadPath, '_blank', '');  
	}

	/**************************************************/
	/* related implementation for the main menu modal */
	/**************************************************/
	$scope.openMainMenuModal = function() {
		$ionicModal.fromTemplateUrl('components/main-menu/modal-main-menu.html', {
			scope: $scope,
			animation: 'null',
			hardwareBackButtonClose: true
		}).then(function(modal) {
			$scope.mainMenuModal = modal;
			serverAPI.eventUserAccount(shortsUserEvents.UserEnterToMaimMenu, "");
			$scope.mainMenuModal.show();
		});
	};

	$scope.closeMainMenuModal = function() {
		if ($scope.mainMenuModal)
			$scope.mainMenuModal.remove();
	};

	// Cleanup the modal when we're done with it!
	$scope.$on('$destroy', function() {
		if ($scope.mainMenuModal)
			$scope.mainMenuModal.remove();
		if ($scope.plmModal)
			$scope.plmModal.hide();
	});

	// Execute action on remove modal
	$scope.$on('modal.removed', function() {
		// Execute action
	});



  // Removes a card from cards.active
  $scope.cardDestroyed = function(index) {
    //Mark that the user has seen this short
	var userActivity = {
		"ShortViewByUser": true, // update when the user view the short
		"ShortKey": $scope.cards.active[0].ShortKey, // Short key
		"UserAccountKey": ""// filled by ServerAPI loging based on login state
	}
	serverAPI.shortUserAccount(userActivity, function(err,response) {
		$ionicScrollDelegate.$getByHandle('cardScroll').scrollTop();
		$scope.shortCardOpen = false;
		if (!err) {
			console.log('ShortViewByUser  - ack ');
		}
		else {
			console.log('ShortViewByUser - fail ');
		}
	});

    $scope.cards.active.splice(index, 1);

    //Set the url path to show the ShortId
	//set angular $location.href so Facebook plug in can have a unique id
	if ($scope.cards.active.length != 0) {
		var shortKey = $scope.cards.active[0].ShortKey;
		$location.hash(shortKey);
	}

    $scope.$apply();
    if ($scope.cards.active.length==3) {
		getShorts(feedType[$scope.selectedFeedType].serverTypeValue, null)
    }
  };



	$scope.shareTextBlur = function() {
		console.log('text area blur');
		$scope.hideFooter = false;
	};

	$scope.shareTextFocus = function() {
		console.log('text area focus');
		$scope.hideFooter = true;
	}

}])

