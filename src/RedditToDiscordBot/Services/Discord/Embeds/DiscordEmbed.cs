using System;
using System.Collections;
using System.Collections.Generic;

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

        public IEnumerable<DiscordEmbedField> Fields { get; }

        // TODO: Make a fluent builder API perhaps.
        public DiscordEmbed(string title, string description, Uri url, DateTimeOffset timestamp, int color, DiscordEmbedThumbnail thumbnail, DiscordEmbedFooter footer, IEnumerable<DiscordEmbedField> fields)
        {
            Title = title;
            Description = description;
            Url = url;
            Timestamp = timestamp;
            Color = color;
            Thumbnail = thumbnail;
            Fields = fields;
            //Footer = footer;
        }
    }
}