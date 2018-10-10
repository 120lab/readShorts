angular.module('starter.controllers', [])

  .controller('SocialCtrl', function ($scope, $http, $location, $ionicPopup) {

    $scope.tasks = [];
    $scope.InProcess = false;

    $scope.createSocial = function (task) {

      /*var message = task.Message;
      var url = task.Url;
      var color = task.Color;
      var writer = task.Writer;

      console.log(message);
      console.log(url);
      console.log(color);
      var req = 'http://readshortsappgateway.azurewebsites.net/api/Invoker?controller=GeneralTasks&param=' + "query.ImageFullPath%3D" + color + "%26query.message%3D" + message + "%26query.subject%3D" + url + "%26query.writer%3D" + writer + "%26query.currentTask%3D1";
      $scope.InProcess = true;

      $http({
        method: 'GET',
        url: req,
        //url: 'http://readshortsappgateway.azurewebsites.net//api/Invoker?method=UserAccount',
        //data: taskFormat
      }).success(function (response) {
        */
     var taskFormat = {
        Message: task.Message, Subject: task.Url, ImageFullPath: task.Color, Writer: task.Writer
      };


      console.log(taskFormat);

      $scope.InProcess = true;

      $http({
        method: 'POST',
        url: 'http://readshortsappgateway.azurewebsites.net/api/Invoker?method=GeneralTasks',        
        data: taskFormat
      }).success(function (response) {        

        //console.log(JSON.stringify(response));

        var msg = "";
        angular.forEach(response.Messages, function (info) {
          if (info.LogLevel == 2) {
            msg += info.Text;
          }
        });

        if (msg == "") {
          msg = "saved correctly";
        }
        else {
          msg = "Save process fail";
        }

        $ionicPopup.alert({
          title: '',
          template: msg
        });

        $scope.InProcess = false;
        task.Message = "";
        task.Url = "";
        task.Color = "";
        task.Writer = "";

      }).error(function (err) {
        console.log(err);
        $scope.InProcess = false;
        $ionicPopup.alert({
          title: '',
          template: 'Error was occured'
        });

      });

    };
  })

  .controller('DashCtrl', function ($scope, $http, $location, $ionicPopup) {

    $scope.tasks = [];
    $scope.InProcess = false;

    $scope.createWriter = function (task) {

      /*
          // Test section
          task.EmailAddress = "ida.gvili@Gmail.COM";
          task.FirstName = "d";
          task.LastName = "d";
          task.PicturePath = "d";
          task.ShortBio = "d";
          task.PersonalId = "d";
          task.Address = "d";
          task.MobilePhone = "d";
      */

      var taskFormat = {
        EmailAddress: task.EmailAddress, FirstName: task.FirstName, LastName: task.LastName,
        PicturePath: task.PicturePath, ShortBio: task.ShortBio, LUSubscriptionTypeKey: 4, LUGenderKey: 3,
        LUCountryKey: 250, IsEmailConnect: true, PersonalId: task.PersonalId, Address: task.Address,
        MobilePhone: task.MobilePhone
      };


      console.log(taskFormat);

      $scope.InProcess = true;

      $http({
        method: 'POST',
        url: 'http://readshortsappgateway.azurewebsites.net/api/Invoker?method=UserAccount',
        //url: 'http://readshortsappgateway.azurewebsites.net//api/Invoker?method=UserAccount',
        data: taskFormat
      }).success(function (response) {

        //console.log(JSON.stringify(response));

        var msg = "";
        angular.forEach(response.Messages, function (info) {
          if (info.LogLevel == 2) {
            msg += info.Text;
          }
        });

        if (msg == "") {
          if (response.Users != undefined && response.Users != null) {
            msg = "saved correctly , user key = " + response.Users[0].RecordKey;
          }
          else {
            msg = "Save process fail";
          }
        }

        $ionicPopup.alert({
          title: '',
          template: msg
        });

        task.EmailAddress = "";
        task.FirstName = "";
        task.LastName = "";
        task.PicturePath = "";
        task.ShortBio = "";
        task.PersonalId = "";
        task.Address = "";
        task.MobilePhone = "";
        $scope.InProcess = false;

      }).error(function (err) {
        console.log(err);
        $scope.InProcess = false;
        $ionicPopup.alert({
          title: '',
          template: 'Error was occured'
        });

      });

    };
  })

  .controller('ChatsCtrl', function ($scope, Chats) {
    // With the new view caching in Ionic, Controllers are only called
    // when they are recreated or on app start, instead of every page change.
    // To listen for when this page is active (for example, to refresh data),
    // listen for the $ionicView.enter event:
    //
    //$scope.$on('$ionicView.enter', function(e) {
    //});


    var myDataPromise = Chats.getData();
    myDataPromise.then(function (result) {
      $scope.chats = result.Shorts;
      console.log($scope.chats);
    });
    // $scope.chats = Chats.all();



    //console.log($scope.chats);

    $scope.remove = function (chat) {
      Chats.remove(chat);
    };
  })

  .controller('ChatDetailCtrl', function ($scope, $stateParams, Chats) {
    $scope.chat = Chats.get($stateParams.chatId);
  })

  .controller('AccountCtrl', function ($scope, $http, $location, $ionicPopup) {

    $scope.InProcess = false;
    $scope.short = {
      WriterUserKey: 0,
      WritersEmail: "",
      Title: "",
      Quote: "",
      Text: "",
      LUShortAgeRestrictionKey: "1",
      LUSysInterfacelanguageKey: "2",
      LUQuoteTypeKey: "1",
      LUStoryTypeKey: "3",
      Tags: "",
      CategoryType: "Life's Just Happened",
      CategoryPicturePath: ""
    };


    $scope.categoryChanged = function (catselected) {

      $scope.short.CategoryType = catselected;
    }

    $scope.createShort = function () {

      if ($scope.short == undefined) {
        $ionicPopup.alert({
          title: '',
          template: 'No data'
        });
        return;
      }

      var shortFormat = {
        WriterUserKey: $scope.short.WriterUserKey,
        WritersEmail: $scope.short.WritersEmail,
        Title: $scope.short.Title,
        Quote: $scope.short.Quote,
        Text: $scope.short.Text,
        LUShortAgeRestrictionKey: $scope.short.LUShortAgeRestrictionKey,
        LUSysInterfacelanguageKey: $scope.short.LUSysInterfacelanguageKey,
        LUQuoteTypeKey: $scope.short.LUQuoteTypeKey,
        LUStoryTypeKey: $scope.short.LUStoryTypeKey,
        Tags: $scope.short.Tags,
        CategoryType: $scope.short.CategoryType,
        CategoryPicturePath: $scope.short.CategoryPicturePath
      };

      console.log(shortFormat);

      $scope.InProcess = true;

      $http({
        method: 'POST',
        url: 'http://readshortsappgateway.azurewebsites.net/api/Invoker?method=Short',
        //url: 'http://readshortsappgateway.azurewebsites.net//api/Invoker?method=Short',
        data: shortFormat
      }).success(function (response) {

        console.log(JSON.stringify(response));

        var msg = "";
        angular.forEach(response.Messages, function (info) {
          if (info.LogLevel == 2) {
            msg += info.Text;
          }
        });

        if (msg == "") {

          $scope.short.WriterUserKey = "0";
          $scope.short.WritersEmail = "";
          $scope.short.Title = "";
          $scope.short.Quote = "";
          $scope.short.Text = "";
          $scope.short.LUShortAgeRestrictionKey = "1";
          $scope.short.Language = "2";
          $scope.short.LUQuoteTypeKey = "1";
          $scope.short.LUStoryTypeKey = "3";
          $scope.short.Tags = "";
          $scope.short.CategoryType = "Life's Just Happened";
          $scope.short.CategoryPicturePath = "";

          msg = "saved correctly";
        }

        $scope.InProcess = false;

        $ionicPopup.alert({
          title: '',
          template: msg
        });

      }).error(function (err) {
        console.log(err);
        $scope.InProcess = false;
        $ionicPopup.alert({
          title: '',
          template: 'Error was occured'
        });

      });

    };

  });
