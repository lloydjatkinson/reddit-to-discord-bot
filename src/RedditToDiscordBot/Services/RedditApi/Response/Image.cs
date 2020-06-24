using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RedditToDiscordBot.Services.RedditApi.Response
{
    public class Image
    {
        [JsonPropertyName("source")]
        public Source Source { get; set; }

        [JsonPropertyName("resolutions")]
        public IList<Resolution> Resolutions { get; set; }

        [JsonPropertyName("variants")]
        public Variants Variants { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}