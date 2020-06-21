using System;
using System.Text.Json.Serialization;

namespace RedditToDiscordBot.Services.Discord.Embeds
{
    public class DiscordEmbedFooter
    {
        public string Text { get; }

        [JsonPropertyName("icon_url")]
        public Uri IconUrl { get; }

        public DiscordEmbedFooter(string text, Uri iconUrl)
        {
            Text = text;
            IconUrl = iconUrl;
        }
    }
}