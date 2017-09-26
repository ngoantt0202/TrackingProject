/// <reference path="jquery-1.10.2.js" />
function getAccessToken() {
    if (location.hash) {
        if (location.hash.split('access_token=')) {
            var accessToken = location.hash.split('access_token=')[1].split('&')[0];
            if (accessToken) {
                isUserRegistered(accessToken)
            }
        }
    }
}

function isUserRegistered(accessToken) {
    $.ajax({
        url: 'http://localhost:65076/api/Account/UserInfo',
        method: 'GET',
        headers: {
            'Authorization': 'Bearer ' + accessToken
        },
        success: function(response){
            if (response.HasRegistered) {
                loginSocial(accessToken, response.Email)

            } else {
                signupExternalUser(accessToken, response.LoginProvider);
            }
        }
    });
}

function loginSocial(accessToken, userName) {

    $.post('http://localhost:65076/User/LoginBySocial',
           { accessToken: accessToken, userName: userName }, function (data) {
               window.location.href = "http://localhost:65076/TaskManage/Index"
           });
}

function signupExternalUser(accessToken, provider) {
    $.ajax({
        url: 'http://localhost:65076/api/Account/RegisterExternal',
        method: 'POST',
        headers: {
            'content-type': 'application/JSON',
            'Authorization': 'Bearer ' + accessToken
        },
        success: function (response) {
            window.location.href = "/api/Account/ExternalLogin?provider=" + provider + "&response_type=token&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A65076%2FUser%2FLogin&state=4fAJtYnrDJO22BMTCRJLzsvr3CIaBuSZkbTkcfUEgIw1";
        },
        error: function (jqXHR) {
            console.log(jqXHR.responseText);
        }
    });
}