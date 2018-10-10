var shortsCtrls = angular.module('shorts.controllers');

shortsCtrls.controller('TermsCtrl', ['$scope', '$state', '$stateParams' ,'$ionicHistory', 'shorts.serverAPI', function($scope, $state, $stateParams, $ionicHistory, serverAPI) {

	console.log('inside Terms controller');

	serverAPI.eventUserAccount(shortsUserEvents.UserInTermsPage, "");



}])
