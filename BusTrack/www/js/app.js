// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
// 'starter.services' is found in services.js
// 'starter.controllers' is found in controllers.js
angular.module('starter', ['ionic', 'ngMap'])

.run(function ($ionicPlatform) {
    $ionicPlatform.ready(function () {
        // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
        // for form inputs)
        if (window.cordova && window.cordova.plugins && window.cordova.plugins.Keyboard) {
            cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
        }
        if (window.StatusBar) {
            // org.apache.cordova.statusbar required
            StatusBar.styleLightContent();
        }
    });
})

.config(function ($stateProvider, $urlRouterProvider) {

    // Ionic uses AngularUI Router which uses the concept of states
    // Learn more here: https://github.com/angular-ui/ui-router
    // Set up the various states which the app can be in.
    // Each state's controller can be found in controllers.js
    $stateProvider

    // setup an abstract state for the tabs directive
      .state('main', {
          url: "/main",
          abstract: true,
          templateUrl: "templates/main.html"
      })

    // Each tab has its own nav history stack:

    .state('main.home', {
        url: '/home',
        views: {
            'mainview': {
                templateUrl: 'templates/home.html',
                controller: 'mainCtrl'
            }
        }
    })

    .state('tab.chats', {
        url: '/chats',
        views: {
            'tab-chats': {
                templateUrl: 'templates/tab-chats.html',
                controller: 'ChatsCtrl'
            }
        }
    })
      .state('tab.chat-detail', {
          url: '/chats/:chatId',
          views: {
              'tab-chats': {
                  templateUrl: 'templates/chat-detail.html',
                  controller: 'ChatDetailCtrl'
              }
          }
      })

    .state('tab.account', {
        url: '/account',
        views: {
            'tab-account': {
                templateUrl: 'templates/tab-account.html',
                controller: 'AccountCtrl'
            }
        }
    });

    // if none of the above states are matched, use this as the fallback
    $urlRouterProvider.otherwise('/main/home');

})
.controller('mainCtrl', function ($scope) {
    document.addEventListener("deviceready", onDeviceReady, false);
    $scope.latlong = "Fetching position";
    var watchID = null;

    // device APIs are available
    //
    function onDeviceReady() {
        // Throw an error if no update is received every 30 seconds
        var options = { timeout: 30000 };
        watchID = navigator.geolocation.watchPosition(onSuccess, onError, options);
    }

    // onSuccess Geolocation
    //
    function onSuccess(position) {
        var element = document.getElementById('geolocation');
        var myLatlng = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
        $scope.map.setCenter(myLatlng);

        var marker = new google.maps.Marker({
            position: myLatlng,
            map: $scope.map
        });
        if (position.coords.Speed == undefined) {
            speed = "0";
        }
        else {
            speed = position.coords.Speed;
        }
        var a=position.coords.latitude + "," + position.coords.longitude + '  Speed: ' + speed;
        $scope.$apply(function () {
            $scope.latlong = a;
        });
        
    }

    // onError Callback receives a PositionError object
    //
    function onError(error) {
        alert('code: ' + error.code + '\n' +
              'message: ' + error.message + '\n');
    }

    $scope.$on('mapInitialized', function (event, map) {
        $scope.map = map;
        directionsDisplay = new google.maps.DirectionsRenderer();
        directionsDisplay.setMap(map);
        google.maps.event.trigger(map, 'resize');

    });

});
