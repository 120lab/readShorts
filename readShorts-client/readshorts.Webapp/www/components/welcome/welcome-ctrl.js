var shortsCtrls = angular.module('shorts.controllers');

shortsCtrls.controller('WelcomeCtrl', ['$scope', '$state', '$stateParams' ,'$ionicHistory', 'shorts.serverAPI', 'shorts.login',function($scope, $state, $stateParams, $ionicHistory, serverAPI, login) {

	console.log('inside welcome controller');
	var dbgLastClickTimeStamp=0;
	var dbgClickCounter = 0;

	//Clear 'back' history so next view can't return to the welcome screen
/*	$ionicHistory.nextViewOptions({
		disableBack: true, 
		historyRoot: false
	}); */

	$scope.gotoShorts = function() {

		$state.go('shorts');
	}

	//for debugging only
	//TODO - remove on production
	$scope.dbgDeleteUser = function() {
		currentTime = Date.now();
		if ((currentTime - dbgLastClickTimeStamp) >1000) {
			dbgClickCounter = 1
		}
		else {
			dbgClickCounter ++;
			if (dbgClickCounter == 4) {
				console.log("ctrl-deletinguser");
				serverAPI.deleteUser(function(err) {
					if (!err)
						console.log('delete user succeded');
					else
						console.log('delete user failed');
				});
			}
		}
		dbgLastClickTimeStamp = currentTime;
	};

	$scope.login = function() {
		login.show();
	}

	$scope.isUserLoggedIn = function() {
		return serverAPI.isLoggedIn();
	}
}])
