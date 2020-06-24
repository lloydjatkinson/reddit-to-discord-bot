using System.Text.Json.Serialization;

namespace RedditToDiscordBot.Services.RedditApi.Response
{
    public class MediaEmbed
    {
        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("width")]
        public int? Width { get; set; }

        [JsonPropertyName("scrolling")]
        public bool? Scrolling { get; set; }

        [JsonPropertyName("height")]
        public int? Height { get; set; }
    }
}