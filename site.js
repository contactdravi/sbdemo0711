// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const authConfig = {
    auth:
    {
        clientId: '',
        authority: 'https://login.microsoftonline.com/'
    }
};

const msalInstance = new Msal.UserAgentApplication(authConfig)

function signIn() {

    msalInstance.loginPopup({ scopes: ["user.read"] })
        .then(() => {
            msalInstance
                .acquireTokenSilent({ scopes: ["user.read"] })
                .then(function (tokenResponse) {
                    callApi(tokenResponse.accessToken);
                })
        })
}

function signOut() {
    msalInstance.logout()
}

function callApi(accessToken) {
    var headers = new Headers();
    var bearer = "Bearer " + accessToken;
    headers.append("Authorization", bearer);
    var options = {
        method: "GET",
        headers: headers
    };
    var graphEndpoint = "https://graph.microsoft.com/v1.0/me";

    fetch(graphEndpoint, options)
        .then(response => response.json())
        .then(json => {
            let profileElement = document.getElementById('profile');
            profileElement.value = JSON.stringify(json, null, 4)
        })
}
