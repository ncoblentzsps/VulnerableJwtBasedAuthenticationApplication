// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function myFetch(url, method, authenticated, data = {})
{    
    var requestInfo = {
        method: method,
        headers: { 'Content-Type': 'application/json' },
        cache: 'no-cache',
        credentials: 'same-origin',
        redirect: 'manual'
    };
    if (authenticated) {
        var token = sessionStorage.getItem('jwt');
        requestInfo.headers['Authorization'] = 'bearer ' + token;
    }
    if (method == 'POST' || method == 'PUT') {
        requestInfo.body = JSON.stringify(data);
    }

    return fetch(url, requestInfo);
}


async function isLoggedIn() {    
    var jwt = sessionStorage.getItem('jwt');
    if (jwt != null && jwt !== '') {
        myFetch('/api/v1/Internal', 'GET', true)
            .then(response => setLoggedIn(response.ok))
            .catch(exception => setLoggedIn(false));
    }
    showHideBasedOnLogin();
}


function showHideBasedOnLogin() {
    if (loggedIn) {
        document.querySelectorAll(".hide-loggedin").forEach(box => { box.style.display = 'none' });
        document.querySelectorAll(".show-loggedin").forEach(box => { box.style.display = '' });
    }
    else {
        document.querySelectorAll(".hide-loggedin").forEach(box => { box.style.display = '' });
        document.querySelectorAll(".show-loggedin").forEach(box => { box.style.display = 'none' });
    }
}

var ready = (callback) => {
    if (document.readyState != "loading") callback();
    else document.addEventListener("DOMContentLoaded", callback);
}

function setLoggedIn(result) { loggedIn = result; }

var loggedIn = false;

ready(() => {        
    isLoggedIn();    
});




document.getElementById("loginButton").addEventListener("click", e =>
{
    document.getElementById('loginStatus').innerText = "";
    var username = document.getElementById("loginUsername").value;
    var password = document.getElementById("loginPassword").value;
    data = { 'username': username, 'password': password };
    myFetch('/api/v1/Login', 'POST', false,data)
        .then(response => {
            if (response.ok) {
                return response.json();
            }
            throw new Error(response.status);

        })
        .then(data => {
            sessionStorage.setItem('jwt', data.token);            
            //document.location.href = '/';
            isLoggedIn();
            $('#modalLogin').modal('hide');
        })
        .catch(error => {
            console.log(error);
            document.getElementById('loginStatus').innerText = "Login Failed";            
        });
    
});