using System;
using System.Collections.Generic;
using System.Linq;

using RedditToDiscordBot.Services.Discord.Embeds;

namespace RedditToDiscordBot.Services
{
    public class DiscordMessage
    {
        public IEnumerable<DiscordEmbed> Embeds { get; }

        public DiscordMessage(IEnumerable<DiscordEmbed> embeds)
        {
            if (embeds.Count() > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(embeds), "A Discord message may only contain a maximum of 10 embeds.");
            }

            Embeds = embeds;
        }
    }
}