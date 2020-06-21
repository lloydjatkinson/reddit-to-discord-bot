using System;

namespace RedditToDiscordBot.Services.Uptime
{
    public interface IUptime
    {
        (TimeSpan TimeSpan, string Friendly) System { get; }
        (TimeSpan TimeSpan, string Friendly) Bot { get; }
    }
}