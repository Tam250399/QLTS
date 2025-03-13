// Cấu hình OIDC User manager
var config = {
    authority: "https://test.qltsc.com:8084/", // URL của SSO
    client_id: "js_oidc", // ID client do SSO cung cấp
    client_secret: 'F535C4AB-4177-4300-84BF-46A3359969E9', // Client secret key do SSO cung cấp
    redirect_uri: window.location.origin + "/Home/CallBackSSO", // Đường dẫn đến trang callback.html để redirect sau khi đăng nhập thành công. *** Đường dẫn này phải được SSO add vào White List
    post_logout_redirect_uri: window.location.origin + "/logout", // Đường dẫn redirect sau khi đăng xuất thành công. *** Đường dẫn này phải được SSO add vào White List

    response_type: "code",
    scope: "openid offline_access", // scope cho phép của client

    automaticSilentRenew: true, // Tự động renew access_token 60s trước khi hết hạn 
    silent_redirect_uri: window.location.origin + "/silent.html", // Đường dẫn của trang renew access_token. *** Đường dẫn này phải được SSO add vào White List
};
Oidc.Log.logger = window.console;
Oidc.Log.level = Oidc.Log.DEBUG;
var mgr = new Oidc.UserManager(config);


// Thêm log các sự kiện của OIDC User manager
mgr.events.addUserLoaded(function (user) {
    console.log("User loaded");
    showTokens();
});
mgr.events.addUserUnloaded(function () {
    console.log("User logged out locally");
    showTokens();
});
mgr.events.addAccessTokenExpiring(function () {
    console.log("Access token expiring...");
});
mgr.events.addSilentRenewError(function (err) {
    console.log("Silent renew error: " + err.message);
});
mgr.events.addUserSignedIn(function (e) {
    console.log("user logged in to the token server");
});
mgr.events.addUserSignedOut(function () {
    console.log("User signed out of OP");
    window.location.href = window.location.origin + '/logout';
});


// Đăng nhập
function login() {
    
    mgr.signinRedirect();
}
// Đăng xuất
function logout() {
    
    mgr.signoutRedirect();
}
// Hủy access_token
function revoke() {
    mgr.revokeAccessToken().then(function () {
        //log("Access Token Revoked.")
    }).catch(function (err) {
        //log(err);
    });
}
// Làm mới access_token
function renewToken() {
    mgr.signinSilent()
        .then(function () {
            //log("silent renew success");
            showTokens();
        }).catch(function (err) {
            //log("silent renew error", err);
        });
}
// Gọi Test API Service
function callApi() {
    mgr.getUser().then(function (user) {
        
        var xhr = new XMLHttpRequest();
        xhr.onload = function (e) {
            if (xhr.status >= 400) {
                display("#ajax-result", {
                    status: xhr.status,
                    statusText: xhr.statusText,
                    wwwAuthenticate: xhr.getResponseHeader("WWW-Authenticate")
                });
            }
            else {
                display("#ajax-result", xhr.response);
            }
        };
        xhr.open("GET", "https://test.qltsc.com:8081/identity", true);
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();
    });
}
// Ghi log vào message panel
function log(data) {
    document.getElementById('response').innerText = '';

    Array.prototype.forEach.call(arguments, function (msg) {
        if (msg instanceof Error) {
            msg = "Error: " + msg.message;
        }
        else if (typeof msg !== 'string') {
            msg = JSON.stringify(msg, null, 2);
        }
        document.getElementById('response').innerHTML += msg + '\r\n';
    });
}
// Hiển thị kết quả ra các panel
function display(selector, data) {
    if (data && typeof data === 'string') {
        try {
            data = JSON.parse(data);
        }
        catch (e) { }
    }
    if (data && typeof data !== 'string') {
        data = JSON.stringify(data, null, 2);
    }
    document.querySelector(selector).textContent = data;
}
// Hiển thị access_token
function showTokens() {
    mgr.getUser().then(function (user) {
        if (user) {
            display("#id-token", user);

            startSignalR();
        }
        else {
            //log("Not logged in");
        }
    });
}
// Kết nối signalR
const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://test.qltsc.com:8084/notification")
    .configureLogging(signalR.LogLevel.Information)
    .build();

async function startSignalR() {
    try {
        await connection.start();
        console.log("signalR connected");
    } catch (err) {
        console.log(err);
        setTimeout(() => startSignalR(), 5000);
    }
};


// Tự động logout khi tài khoản đăng nhập ở nơi khác
connection.on("onForceLogout", message => {
    let _url = "/NguoiDung/LogoutAjax";
    let _data = null;
    ajaxPost(_data, _url).done(() => {
        //log out sso. cần thư viện singleSignOnApp.js ở trước
        logout();
    });
});


//document.querySelector(".login").addEventListener("click", login, false);
//document.querySelector(".renew").addEventListener("click", renewToken, false);
//document.querySelector(".call").addEventListener("click", callApi, false);
//document.querySelector(".revoke").addEventListener("click", revoke, false);
//document.querySelector(".logout").addEventListener("click", logout, false);
//login();