angular.module('app').controller('AdminRegisterController', function ($scope, $stateParams, $http, apiUrl, AuthenticationService, $state) {
    $scope.registration = {};

    $scope.register = function () {
        AuthenticationService.registerAdmin($scope.registration).then(
            function (response) {
                // As soon as we know that the user has successfully registered
                AuthenticationService.login({ username: $scope.registration.EmailAddress, password: $scope.registration.Password }).then(
                    function () {
                        $state.go('app.dashboard');
                    },
                    function () {
                        alert('This should NOT have happened O_O');
                    }
                );
            },
            function (error) {
                bootbox.alert("Failed To Register");
            }
        );
    };

    $scope.login = function () {
        AuthenticationService.login($scope.loginData).then(
            function (response) {
                $state.go('app.dashboard');
            },
            function (err) {
                bootbox.alert(err.error_description);
            }
        );
    };

    $("#registration").hide();
    $('#teamregistration').hide();
    $("#two").css("color", "#FF8C00");
    $("#two").css("border-bottom", "2px solid #fff");

    $("#one").click(function () {
        $("#one").css("color", "#FF8C00");
        $("#one").css("border-bottom", "2px solid #fff");
        $("#two").css("color", "#0026ff");
        $("#two").css("border-bottom", "none");
        $("#login").hide(1000);
        $("#registration").show(1000);
        $('#teamregistration').show(1000);
    });

    $("#two").click(function () {
        $("#one").css("color", "#0026ff");
        $("#one").css("border-bottom", "none");
        $("#two").css("color", "#FF8C00");
        $("#two").css("border-bottom", "2px solid #fff");
        $("#registration").hide(1000);
        $('#teamregistration').hide(1000);
        $("#login").show(1000);
    });
});