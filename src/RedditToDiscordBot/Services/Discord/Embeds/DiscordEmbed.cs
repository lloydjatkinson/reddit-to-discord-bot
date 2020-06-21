using System;

namespace RedditToDiscordBot.Services.Discord.Embeds
{
    public class DiscordEmbed
    {
        public string Title { get; }

        public string Description { get; }

        public Uri Url { get; }

        public DateTimeOffset Timestamp { get; }

        public int Color { get; }

        //public DiscordEmbedFooter Footer { get; }

        public DiscordEmbedThumbnail Thumbnail { get; }

        public DiscordEmbed(string title, string description, Uri url, DateTimeOffset timestamp, int color, DiscordEmbedThumbnail thumbnail, DiscordEmbedFooter footer)
        {
            Title = title;
            Description = description;
            Url = url;
            Timestamp = timestamp;
            Color = color;
            Thumbnail = thumbnail;
            //Footer = footer;
        }
    }
}