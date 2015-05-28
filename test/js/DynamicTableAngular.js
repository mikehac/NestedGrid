(function () {
    var app = angular.module("DynamicGrid", []);
    var MainController = function ($scope, $http) {
        $scope.imgSrc = 'images/plus.jpg';

        var onGetParrentsComplete = function (response) {
            $scope.accTypes = response.data;
        }
        var onGetChildrenComplete = function (response) {
            $scope.accounts = response.data;
        }
        var onError = function (reason) {
            console.log(reason);
        };
        
        $http.get("GetNestedGridData.ashx?rt=GetParrents")
        .then(onGetParrentsComplete, onError);

        $scope.GetFirstLevel = function (parrentId) {
            $scope.imgSrc = 'images/minus.jpg';
            $http.get("GetNestedGridData.ashx?rt=GetChildren&ParrentId=" + parrentId)
            .then(onGetChildrenComplete, onError);
        };
    };

    app.controller("MainController", ["$scope", "$http", MainController]);
}());