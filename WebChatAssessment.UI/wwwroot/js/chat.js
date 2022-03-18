
"use strict";
// Creating a connection to SignalR Hub
let connection = new signalR.HubConnectionBuilder().withUrl("/signalr-hub").build();

// Starting the connection with server
connection.start().then(function() {}).catch(function(err) {
    return console.error(err.toString());
});

// Sending the message from Client
document.getElementById("btnSend").addEventListener("click", function(event) {
    let username = document.getElementById("userName").value;
    let message = document.getElementById("message").value;
    connection.invoke("SendMessage", username, message).catch(function(err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

// Subscribing to the messages broadcasted by Hub every time when a new message is pushed to it
connection.on("SendMessage", function(user, message) {
    if(!message)
        return;
    let finalMessage = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    let displayMsg = `[${new Date().toLocaleTimeString()}] ${user} : ${finalMessage}`;
    let li = document.createElement("li");
    li.textContent = displayMsg;
    document.getElementById("listMessage").prepend(li);
});