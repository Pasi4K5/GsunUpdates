﻿using Discord;
using Discord.WebSocket;
using GsunUpdates;

var client = new DiscordSocketClient();
var db = new JsonDb();
var osuApi = new OsuApi();
await osuApi.StartAsync();
var commandHandler = new CommandHandler(client, db);
var updateService = new UpdateService(client, db, osuApi);
var eventHandler = new Events(client);

client.Log += Log;

var token = Config.Get().BotToken;

await client.LoginAsync(TokenType.Bot, token);
await client.StartAsync();

await Task.Delay(-1);

static Task Log(LogMessage msg)
{
    Console.WriteLine(msg.ToString());

    return Task.CompletedTask;
}
