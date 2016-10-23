(function () {
    'use strict';

    var app = angular.module("autocorrect", []);
})();


(function () {
    'use strict';

    angular
        .module('autocorrect')
        .controller("myCtrl", myController);

    myController.$inject = ['$scope', '$http'];

    function myController($scope, $http) {

        var ctr = this;

        $scope.gosh = debounce(httpGet, 500);

        $scope.getColour = redToGreen.bind(ctr, 0, 4);

        function debounce(func, wait) {
            var timeout;
            return function () {
                var context = this, args = arguments;
                var later = function () {
                    timeout = null;
                    func.apply(context, args);
                };
                //var callNow = immediate && !timeout;
                clearTimeout(timeout);
                timeout = setTimeout(later, wait);
               // if (callNow) func.apply(context, args);
            };
        };

        function httpGet() {
            $http.get('/api/word/' + $scope.word).then(function successCallback(response) {
                $scope.list = response.data;
            }, function errorCallback(response) {
                console.log("error: " + response);
            });
        }

        function redToGreen(min, max, value) {
            var range = max - min;
            var r2 = range / 2;

            if (value <= range / 2) {
                var r = (value / r2) * 255;
                r = Math.round(r);

                return { color: 'rgb(' + r + ', 255, 0)' }
            }
            if (value <= range) {
                var r = 255 - ((value - r2) / r2) * 255;
                r = Math.round(r);

                return { color: 'rgb(255, ' + r + ', 0)' }
                
            }

            return { color: 'rgb(0,255,0)' }
        }

        function greenToRed(min, max, value) {
            var range = max - min;

            if (value <= range / 2) {
                var r = (value / 5) * 255;

                return { color: 'rgb(255, ' + r + ', 0)' }
            }
            if (value <= range) {
                var r = 255 - ((value - 5) / 5) * 255;

                return { color: 'rgb(' + r + ', 255, 0)' }
            }

            return { color: 'rgb(0,255,0)' }
        }
    }
})();
