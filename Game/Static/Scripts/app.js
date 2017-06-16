$(function () {
    var userName = prompt('Введитие имя:', '');

    var game = new Game();

    var broadcastHub = $.connection.broadcastHub;
    broadcastHub.client.broadcasUnits = function (data) {
        //console.log(data);
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
    gameHub.client.broadcastKillUnit = function(id) {
        game.UpdateKillUnit(id);
    };
    gameHub.client.broadcastUpdateScore = function (score) {
        game.UpdateScore(score);
    }

    // Start the connection.
    $.connection.hub.start().done(function () {
        gameHub.server.auth(userName);
    });
});