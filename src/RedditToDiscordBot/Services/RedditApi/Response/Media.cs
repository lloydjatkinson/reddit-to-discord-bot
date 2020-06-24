using System.Text.Json.Serialization;

namespace RedditToDiscordBot.Services.RedditApi.Response
{
    public class Media
    {
        [JsonPropertyName("reddit_video")]
        public RedditVideo RedditVideo { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("oembed")]
        public Oembed Oembed { get; set; }
    }
}