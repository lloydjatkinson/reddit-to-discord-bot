using System;

namespace RedditToDiscordBot.Services.Discord.Embeds
{
    public class DiscordEmbedThumbnail
    {
        public Uri Url { get; }

        public DiscordEmbedThumbnail(Uri url)
        {
            Url = url;
        }
    }
}