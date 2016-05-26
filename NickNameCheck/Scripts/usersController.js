angular
    .module("app", ['ngMessages', 'ngGrid'])
    .directive('nicknameAvailable', function ($q, userService) {

        return {
            require: 'ngModel',
            link: function (scope, element, attrs, usersController) {

                usersController.$asyncValidators.nicknameAvailable = function (modelValue, viewValue) {
                    return $q(function (resolve, reject) {
                        userService.checkNickNameAvailability(viewValue).then(function (result) {
                            usersController.$setValidity('nickname-available', result.data);
                            if (result.data) {
                                resolve();                                
                            } else {
                                reject();                                
                            }
                        });
                    });
                }
            }
        }
                
    })
    .service('userService', function ($http) {
        var userApiUrl = "/api/Users";

        var getAllUsers = function () {
            return $http.get(userApiUrl);
        };

        var createUser = function (user) {
            return $http.post(userApiUrl, user);
        };

        var checkNickNameAvailability = function (nickname) {
            return $http.get(userApiUrl, { params: { nickname: nickname } })
        };

        return {
            getAllUsers: getAllUsers,
            createUser: createUser,
            checkNickNameAvailability: checkNickNameAvailability
        };

    })
    .controller("usersController", ['$scope', '$timeout', 'userService', function ($scope, $timeout, userService) {
    
        $scope.model = {
            Name: "",
            NickName: ""
        };
       
        $scope.submitted = false;
                
        userService.getAllUsers().
            success(function (data) {
                $scope.users = data;
            });

        $scope.gridOptions = {
            data: 'users',
            multiSelect: false            
        };

        $scope.addUser = function (user) {

            $scope.users.push(user);

            $timeout(function () {
                var grid = $scope.gridOptions.ngGrid;
                $scope.gridOptions.selectItem($scope.users.length - 1, true);
                grid.$viewport.scrollTop((($scope.users.length - 1) * grid.config.rowHeight));
            }, 0);

        };

         $scope.createUser = function () {
            userService.
                createUser($scope.model).
                success(function (user) {
                    $scope.addUser(user);
                    $scope.userForm.$setPristine(true);
                    $scope.model.Name = "";
                    $scope.model.NickName = "";
                    $scope.submitted = false;
                });
        };

        $scope.checkNickName = function ()
        {
            $scope.checkClicked = true;
        };

        $scope.submit = function ()
        {
            $scope.submitted = true;
            if ($scope.userForm.$pending == undefined && $scope.userForm.$valid)
            {
                $scope.createUser($scope.model);
            }
            
        };
}]);

