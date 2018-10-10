var shortsCtrls = angular.module('shorts.controllers');

shortsCtrls.controller('ContactUsCtrl', ['$rootScope','$scope', '$state', '$stateParams' ,'$ionicHistory', 'shorts.serverAPI', 'shorts.login',function($rootScope, $scope, $state, $stateParams, $ionicHistory, serverAPI, login) {

	console.log('inside contact-us controller');
	$scope.itemPressedIndex=0;
	$scope.contactDetails = {};

	$scope.saveSettings = function() {
		serverAPI.sendContactUsData($scope.contactDetails);
	}

	$scope.rate = function(i_index) {
		$scope.itemPressedIndex = i_index;
		$scope.contactDetails.rate = i_index;
	}

}]);
