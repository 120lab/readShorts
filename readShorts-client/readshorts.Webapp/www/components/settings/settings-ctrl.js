var shortsCtrls = angular.module('shorts.controllers');

shortsCtrls.controller('SettingsCtrl', ['$rootScope','$scope', '$state', '$stateParams' ,'$ionicHistory', 'shorts.serverAPI', 'shorts.login',function($rootScope, $scope, $state, $stateParams, $ionicHistory, serverAPI, login) {

	console.log('inside settings controller');
	var dbgLastClickTimeStamp=0;
	var dbgClickCounter = 0;

	//default font size 1-small, 2-medium, 3-large
	$scope.fontSize =1;

	//get current settings of loggedin user
	$scope.personalDetails = serverAPI.getUserInformation();

	//Save configured font size to RootScope, as we need it accross application
	$scope.setFontSize = function(i_fontSize) {
		$rootScope.shortFontSize = i_fontSize;
	}

	$scope.getFontSize = function() {
		return $rootScope.shortFontSize;
	}

	//save setting to backend
	$scope.saveSettings = function() {
		serverAPI.updateUser($scope.personalDetails, function(err,data) {
			if (err) {
				console.log('error in updating user', data);
			}
			if (!err) {
				console.log('user updated');
				serverAPI.eventUserAccount(shortsUserEvents.UserUpdateProfileInfoPage, "");
			}
		})
	}
}]);
