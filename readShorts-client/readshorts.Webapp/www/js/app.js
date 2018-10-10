
var appDependencies = [ 
  'ionic',
  'ngCookies',
  '720kb.socialshare',
  'ku-cards',
  'shorts.controllers',
  'shorts.services',
  'shorts.directives'
];

angular.module('shorts', appDependencies)
.run(function($ionicPlatform) {
  $ionicPlatform.ready(function() {
    // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
    // for form inputs)
    if (window.cordova && window.cordova.plugins && window.cordova.plugins.Keyboard) {
      cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
      cordova.plugins.Keyboard.disableScroll(true);

    }
    if (window.StatusBar) {
      // org.apache.cordova.statusbar required
      StatusBar.styleDefault();
    }
  });
})
.value('ShortsUserEvents',shortsUserEvents)

.config(['$stateProvider', '$urlRouterProvider','$ionicConfigProvider', function($stateProvider, $urlRouterProvider, $ionicConfigProvider) {

  // remove back button previous title text
  // use unicode em space characters to increase touch target area size of back button
  $ionicConfigProvider.backButton.previousTitleText(false).text('&emsp;&emsp;');

  //disable page animation
  $ionicConfigProvider.views.transition('none');

  //force navbar title center
  $ionicConfigProvider.navBar.alignTitle('center');


  // Ionic uses AngularUI Router which uses the concept of states
  // Learn more here: https://github.com/angular-ui/ui-router
  // Set up the various states which the app can be in.
  // Each state's controller can be found in controllers.js
  $stateProvider

  // setup an abstract state for the tabs directive
  .state('init', {
    url: '/init',
    template: '<div> </div>',
  })
  .state('login', {
    url: '/login',
    templateUrl: 'components/login/login.html',
    controller: 'LoginCtrl',
    authenticate: false
  })
  .state('welcome', {
    url: '/welcome',
    templateUrl: 'components/welcome/welcome.html',
    controller: 'WelcomeCtrl',
    authenticate: false
  })
  .state('about', {
    url: '/about',
    templateUrl: 'components/about/about.html',
    controller: 'AboutCtrl',
    authenticate: false
  })
  .state('contact-us', {
    url: '/contact-us',
    templateUrl: 'components/contact-us/contact-us.html',
    controller: 'ContactUsCtrl',
    authenticate: false
  })
  .state('shorts', {
      url: '/shorts/:shortId',
      params: {
        feedType: null
      },
      templateUrl: 'components/shorts/shorts.html',
      controller: 'ShortsCtrl',
      cache: false
  })
  .state('personal-info', {
      url: '/personal-info',
      templateUrl: 'components/personal-info/personal-info.html',
      controller: 'PersonalInfoCtrl',
      authenticate: true
  })
  .state('terms', {
      url: '/terms',
      templateUrl: 'components/terms/terms.html',
      controller: 'TermsCtrl',
      authenticate: false
  })
  .state('settings', {
      url: '/settings',
      templateUrl: 'components/settings/settings.html',
      controller: 'SettingsCtrl',
      authenticate: false
  })
  .state('server-error', {
      url: '/server-error',
      templateUrl: 'components/server-error/server-error.html',
      authenticate: false
  })

  // if none of the above states are matched, use this as the fallback
  $urlRouterProvider.otherwise('/init');
//  $urlRouterProvider.otherwise('/login');

}])
.run(['$location','$rootScope','$state', '$stateParams', '$window', 'shorts.serverAPI', 'ShortsUserEvents', function($location, $rootScope ,$state, $stateParams, $window, serverAPI, shortsUserEvents){
  console.log('App is running');

  $rootScope.shortFontSize = 'small';


  serverAPI.eventUserAccount(shortsUserEvents.UserEnterToApp, "");

  $rootScope.$on('$stateChangeStart', function(ev, nextRoute, nextRouteParams, currRoute, currRouteParams ) {
    if (currRoute.name == "") {
      console.log('Starting app. stateParams = ',ev, nextRoute, nextRouteParams, currRoute, currRouteParams );
      serverAPI.init().then(function() {
        setTimeout(function() {$state.go('shorts', nextRouteParams)},0);
      }, function() {
        //if user is not logged in, take him into welcome screen, unless there is a short id specified 
        // in the state params
        if (nextRouteParams.shortId==null)
          setTimeout(function() {$state.go('welcome')},0);
        else 
          setTimeout(function() {$state.go('shorts',  nextRouteParams)},0);

      } );
      return;
    }
    console.log('Navigating from ',currRoute,' to ', nextRoute);

    if (nextRoute.authenticate && !serverAPI.isLoggedIn()) {
      ev.preventDefault();
      console.log('required Login');
      $state.go('login');
    }

   /* if(nextRoute.name == 'welcome' && serverAPI.isLoggedIn())
      //setTimeout(function() {$state.go('shorts')},0);      
    $state.go('shorts'); */
    var isloggedIntoServer = serverAPI.isLoggedIn();
    if(nextRoute.name == 'init' && isloggedIntoServer) {
      ev.preventDefault();
      $state.go('shorts');
    }

    if(nextRoute.name == 'init' && !isloggedIntoServer) {
      ev.preventDefault();
      $state.go('welcome'); 
    }
  })
}]);
  