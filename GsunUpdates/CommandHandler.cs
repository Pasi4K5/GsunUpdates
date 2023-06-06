﻿using Discord;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;

namespace GsunUpdates;

public sealed class CommandHandler
{
    private readonly DiscordSocketClient _client;
    private readonly JsonDb _db;
    private readonly ChatBot _chatBot;

    public CommandHandler(DiscordSocketClient client, JsonDb db, ChatBot chatBot)
    {
        _client = client;
        _db = db;
        _chatBot = chatBot;

        _client.Ready += CreateCommand;
        _client.SlashCommandExecuted += HandleSlashCommand;
    }

    private async Task CreateCommand()
    {
        var commands = new[]
        {
            new SlashCommandBuilder()
                .WithName("setchannel")
                .WithDescription("Sets the channel to send Gsun updates to.")
                .WithDMPermission(false)
                .WithDefaultPermission(false)
                .WithDefaultMemberPermissions(GuildPermission.Administrator)
                .AddOption("channel", ApplicationCommandOptionType.Channel, "The channel to send Gsun updates to.", true),
            new SlashCommandBuilder()
                .WithName("source")
                .WithDescription("Show the link to the bot's source code.")
                .WithDMPermission(true)
                .WithDefaultPermission(true),
            new SlashCommandBuilder()
                .WithName("neuralize")
                .WithDescription("Makes the bot forget the conversation.")
                .WithDMPermission(false)
                .WithDefaultPermission(true),
            new SlashCommandBuilder()
                .WithName("shutup")
                .WithDescription("Makes the bot stop talking for the given duration and deletes its last message.")
                .WithDMPermission(false)
                .WithDefaultPermission(true)
                .AddOption("duration", ApplicationCommandOptionType.Integer, "The duration in minutes to shut up for.", true),
            new SlashCommandBuilder()
                .WithName("talk")
                .WithDescription("Makes the bot talk again.")
                .WithDMPermission(false)
                .WithDefaultPermission(true)
        };

        foreach (var command in commands)
        {
            try
            {
                await _client.CreateGlobalApplicationCommandAsync(command.Build());
            }
            catch (HttpException e)
            {
                Console.WriteLine(string.Join('\n', e.Errors));
            }
        }
    }

    private async Task HandleSlashCommand(SocketSlashCommand command)
    {
        switch (command.Data.Name)
        {
            case "setchannel":
                if (command.Data.Options.FirstOrDefault()?.Value is not SocketGuildChannel channel)
                {
                    await command.RespondAsync("No valid channel was provided.");

                    break;
                }

                var data = _db.Data;
                var channels = _db.Data["channels"]?.ToObject<List<ChannelInfo>>() ?? new();

                var existingChannel = channels.FirstOrDefault(x => x.GuildId == channel.Guild.Id);

                if (existingChannel is not null)
                {
                    channels.Remove(existingChannel);
                }

                channels.Add(new ChannelInfo
                {
                    Id = channel.Id,
                    GuildId = channel.Guild.Id
                });
                data["channels"] = JArray.FromObject(channels);
                _db.Save(data);

                await command.RespondAsync($"Set Gsun update channel to {channel.Mention()}.");

                break;
            case "source":
                await command.RespondAsync("This bot is open source on GitHub: https://github.com/Pasi4K5/GsunUpdates");

                break;
            case "neuralize":
                _chatBot.RestartConversation();

                await command.RespondAsync("I forgor 💀");

                break;
            case "shutup":
                if (_chatBot.IsShutUp)
                {
                    await command.RespondAsync("I'm already quiet. 🤐");

                    break;
                }

                if (!ulong.TryParse(command.Data.Options.First().Value.ToString(), out var minutes) || minutes > 10)
                {
                    await command.RespondAsync("The duration must be between 0 and 10 minutes.");

                    break;
                }

                await _chatBot.ShutUpAsync(command.Channel, TimeSpan.FromMinutes(minutes));

                await command.RespondAsync($"See ya in {minutes} minute{(minutes == 1 ? "" : "s")}. 🤐");

                break;
            case "talk":
                if (!_chatBot.IsShutUp)
                {
                    await command.RespondAsync("I'm not silenced. 🗣️");

                    break;
                }

                _chatBot.Unsilence();

                await command.RespondAsync($"I'm back. 🗣️");

                break;
        }
    }
}
