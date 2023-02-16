// Creating the connection
var connectionUserEvents = new signalR.HubConnectionBuilder().withUrl("/hubs/userHub").build();

// Connect to methods that hub invokes aka receive notifications from hub
connectionUserEvents.on("updateConnectedUsers", (value) => {
    var newCountConnections = document.getElementById("totalViews");
    newCountConnections.innerText = value.toString();
})

// Invoke hub methods aka send notifciation to hub
function newConnectionEvent() {
    connectionUserEvents.send("NewUserConnected");
}

function fulfilled() {
    console.log("Connection succesfull")
    newConnectionEvent();
}

function rejected() {
    console.log("Connection rejected")
}

// Start communication
connectionUserEvents.start().then(fulfilled, rejected)
