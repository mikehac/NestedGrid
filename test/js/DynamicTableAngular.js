(function () {
    var app = angular.module("DynamicGrid", []);
    var MainController = function ($scope, $http) {
        var onGetParrentsComplete = function (response) {
            $scope.accounts = response.data;
        }
        var onError = function (reason) {
            console.log(reason);
        };
        
        $http.get("GetNestedGridData.ashx?rt=GetParrents")
        .then(onGetParrentsComplete,onError);
    };

    app.controller("MainController", ["$scope", "$http", MainController]);
}());