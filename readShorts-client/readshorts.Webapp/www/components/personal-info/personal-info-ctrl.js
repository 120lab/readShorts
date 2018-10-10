var shortsCtrls = angular.module('shorts.controllers');

shortsCtrls.controller('PersonalInfoCtrl', ['$scope', '$state', '$stateParams', '$location', '$ionicModal', 'shorts.serverAPI', '$ionicHistory', function($scope, $state, $stateParams, $location, $ionicModal, serverAPI, $ionicHistory) {
	console.log('inside personal-info controller');

	serverAPI.eventUserAccount(shortsUserEvents.UserInProfilePage, "");

	$scope.formActive=false;

	$scope.personalDetails = serverAPI.getUserInformation();
	$scope.genderLookupTable = serverAPI.getLookupTable('LUGenders');
	$scope.languageLookupTable = serverAPI.getLookupTable('LUSysInterfaceLanguages');
	$scope.countryLookupTable = serverAPI.getLookupTable('LUCountries');

	console.log($scope.personalDetails);
	console.log('personalDetails viewHistory = ',$ionicHistory.viewHistory());
	console.log('personalDetails currentView = ',$ionicHistory.currentView());

	/**
	* editForm - set the display into 'Edit' Mode 
	*/
	$scope.editForm = function() {
		$scope.formActive = true;
		var bd = $scope.personalDetails.BirthDate;
		$scope.personalDetails.BirthDate = new Date(bd.substr(0,10));
	}

	/**
	* saveForm - saves the updated personal details to the shorts server 
	*			if succesful, switches back the display to normal view, 
	*			If save operation failed display a error message to the user and
	*			remains in 'Edit' mode.
	*/
	$scope.saveForm = function() {

		serverAPI.updateUser($scope.personalDetails, function(err,data) {
			if (err) {
				console.log('error in updating user', data);
			}
			if (!err) {
				console.log('user updated');

				//reload updated user data to scope variable
				$scope.personalDetails = serverAPI.getUserInformation();
				serverAPI.eventUserAccount(shortsUserEvents.UserUpdateProfileInfoPage, "");
			}
		})
		//save succesful
		$scope.formActive = false;
	}
}]);