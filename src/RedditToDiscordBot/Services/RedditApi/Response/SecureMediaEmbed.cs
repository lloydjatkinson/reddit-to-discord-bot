using System.Text.Json.Serialization;

namespace RedditToDiscordBot.Services.RedditApi.Response
{
    public class SecureMediaEmbed
    {
        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("width")]
        public int? Width { get; set; }

        [JsonPropertyName("scrolling")]
        public bool? Scrolling { get; set; }

        [JsonPropertyName("media_domain_url")]
        public string MediaDomainUrl { get; set; }

        [JsonPropertyName("height")]
        public int? Height { get; set; }
    }
}