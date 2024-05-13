"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notifHub").build();

connection.on("ReceiveNotification", function (message) {
    const liveToast = document.getElementById('liveToast');
    const timestampRef = document.getElementById('timestamp');
    const toastBodyRef = document.querySelector('.toast-body');

    timestampRef.innerText = new Date().toLocaleTimeString([], { hour12: true, hour: "2-digit", minute: "2-digit" });
    toastBodyRef.innerText = message;

    const toastBootstrap = bootstrap.Toast.getOrCreateInstance(liveToast);
    toastBootstrap.show();
});

window.addEventListener("load", () => {
    if (window.location.href.includes("Home/Search")) {
        connection.start().then(() => {
            setTimeout(() => {
                connection.invoke("SendNotification").catch(function (err) {
                    return console.error(err.toString());
                });
            }, 10000);
        }).catch(function (err) {
            return console.error(err.toString());
        });
    }
})