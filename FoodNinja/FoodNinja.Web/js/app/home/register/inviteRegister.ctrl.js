angular.module('app').controller('InviteRegisterController', function ($scope, $stateParams, $http, apiUrl) {
    var token = $stateParams.token;

    $http.get(apiUrl + '/invite/' + token)
         .success(function (response) {
             if (token.Invalid) {
                 $scope.tokenValid = false;
                 alert('Your token has expired');
             } else {
                 $scope.tokenValid = true;
                 $scope.token = response.data;
             }
         })
         .error(function () {
             alert('there was an error getting your token');
         });

    //Problem:don't have any interactivity
    //Solution: need interactivity
    $("#registration").hide();
    $("#two").css("color", "#FF8C00");
    $("#two").css("border-bottom", "2px solid #fff");
    //1. when user click on register #registration display block & login hide (default:login)
    $("#one").click(function () {
        $("#one").css("color", "#FF8C00");
        $("#one").css("border-bottom", "2px solid #fff");
        $("#two").css("color", "#0026ff");
        $("#two").css("border-bottom", "none");
        $("#login").hide(1000);
        $("#registration").show(1000);
    });
    $("#two").click(function () {
        $("#one").css("color", "#0026ff");
        $("#one").css("border-bottom", "none");
        $("#two").css("color", "#FF8C00");
        $("#two").css("border-bottom", "2px solid #fff");
        $("#registration").hide(1000);
        $("#login").show(1000);
    });
    //2.Color of open tab will be #ff8C00
});