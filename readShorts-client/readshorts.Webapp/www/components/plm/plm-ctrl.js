var shortsCtrls = angular.module('shorts.controllers');

shortsCtrls.controller('PlmCtrl', ['$scope', '$state', '$stateParams' ,'$ionicHistory', 'Socialshare', 'shorts.serverAPI', function($scope, $state, $stateParams, $ionicHistory, socialshare , serverAPI) {

	console.log('inside PLM controller');
	serverAPI.eventUserAccount(shortsUserEvents.UserInPlmPage, "");

	$scope.inviteFriendsUsingEmail = function() {
		socialshare.share({
			'provider': 'email',
			'attrs': {
				'socialshareSubject': 'Join Shorts',
				'socialshareBody' : 'Join Shorts. It\'s an amazing experience:'
			}
		});
		serverAPI.eventUserAccount(shortsUserEvents.UserShareShortByMail, $scope.cards.active[0].ShortKey);
	}

}])
