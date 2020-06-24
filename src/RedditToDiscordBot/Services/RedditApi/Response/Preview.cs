using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RedditToDiscordBot.Services.RedditApi.Response
{
    public class Preview
    {
        [JsonPropertyName("images")]
        public IList<Image> Images { get; set; }

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("reddit_video_preview")]
        public RedditVideoPreview RedditVideoPreview { get; set; }
    }
}