angular.module('starter.services', [])

  .factory('Chats', function ($http) {

    var chats = [];

    var getData = function () {

      // Angular $http() and then() both return promises themselves 
      return $http({ method: "GET", url: "http://readshortswebclientdev.azurewebsites.net/api/Invoker?controller=Short" }).then(function (result) {
      // return $http({ method: "GET", url: "http://readshortsgatewayappint.azurewebsites.net/api/Invoker?controller=Short" }).then(function (result) {

        // What we return here is the data that will be accessible 
        // to us after the promise resolves
        chats = result.data.Shorts;
        console.log(chats);
        return result.data;
      });
    };



    // var chats = [{
    //   id: 0,
    //   name: 'Ben Sparrow',
    //   lastText: 'You on your way?',
    //   face: 'img/ben.png'
    // }, {
    //   id: 1,
    //   name: 'Max Lynx',
    //   lastText: 'Hey, it\'s me',
    //   face: 'img/max.png'
    // }, {
    //   id: 2,
    //   name: 'Adam Bradleyson',
    //   lastText: 'I should buy a boat',
    //   face: 'img/adam.jpg'
    // }, {
    //   id: 3,
    //   name: 'Perry Governor',
    //   lastText: 'Look at my mukluks!',
    //   face: 'img/perry.png'
    // }, {
    //   id: 4,
    //   name: 'Mike Harrington',
    //   lastText: 'This is wicked good ice cream.',
    //   face: 'img/mike.png'
    // }];

    return {

      getData: getData,

      all: function () {
        console.log("dddddD:" + mData);
        return mData;
      },
      remove: function (chat) {
        chats.splice(chats.indexOf(chat), 1);
      },
      get: function (chatId) {
        for (var i = 0; i < chats.length; i++) {
          if (chats[i].RecordKey === parseInt(chatId)) {
            return chats[i];
          }
        }
        return null;
      }
    };
  });
