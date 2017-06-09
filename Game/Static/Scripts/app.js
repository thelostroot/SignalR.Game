$(function () {
    var game = new Game();

    var broadcastHub = $.connection.broadcastHub;
    broadcastHub.client.internetUpTime = function (data) {
        console.log(data);
        game.UpdateArea(data);
    };
    $.connection.hub.start();


    // Declare a proxy to reference the hub.
    var gameHub = $.connection.gameHub;
    // Register functions
    gameHub.client.initSettings = function (settings) {
        console.log(settings);
        window.GameHub = gameHub;
        game.Init(settings);
    };
    gameHub.client.broadcastKillUnit = function (id) {
        game.UpdateKillUnit(id);
    }

    // Start the connection.
    $.connection.hub.start().done(function () {
        gameHub.server.auth("danis");
    });
});



// Get the user name and store it to prepend to messages.
//$('#displayname').val(prompt('Enter your name:', ''));
// Set initial focus to message input box.
//$('#message').focus();

function run() {
    setInterval(move, 10);

    var el = document.getElementById("el1");
    console.log(el);
    var x = 10;
    var y = 10;
    function move() {
        el.setAttribute("x", x);
        el.setAttribute("y", y);
        x+=1;
        //y+=1;
    };
}
