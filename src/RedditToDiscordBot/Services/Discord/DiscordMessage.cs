using System;
using System.Collections.Generic;
using System.Linq;

namespace RedditToDiscordBot.Services.Discord.Embeds
{
    public class DiscordMessage
    {
        public string Content { get; }

        public IEnumerable<DiscordEmbed> Embeds { get; } = Enumerable.Empty<DiscordEmbed>();

        public DiscordMessage(string content)
        {
            Content = content;
        }

        public DiscordMessage(string content, IEnumerable<DiscordEmbed> embeds)
        {
            if (embeds.Count() > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(embeds), "A Discord message may only contain a maximum of 10 embeds.");
            }

            Content = content;
            Embeds = embeds;
        }
    }
}