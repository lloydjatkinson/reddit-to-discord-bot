using System;

using Humanizer;

namespace RedditToDiscordBot.Services.Uptime
{
    public class Uptime : IUptime
    {
        public (TimeSpan TimeSpan, string Friendly) System { get => throw new NotImplementedException(); }

        public (TimeSpan TimeSpan, string Friendly) Bot
        {
            get
            {
                var difference = _botStartupInitial - DateTimeOffset.UtcNow;

                return (difference, difference.Humanize(3));
            }
        }

        private readonly DateTimeOffset _botStartupInitial;

        private readonly DateTimeOffset _systemStartupInitial;

        public Uptime()
        {
            _botStartupInitial = DateTimeOffset.UtcNow;
        }
    }
}