using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RedditToDiscordBot.Services.RedditApi.Response
{
    public class Mp4
    {
        [JsonPropertyName("source")]
        public Source Source { get; set; }

        [JsonPropertyName("resolutions")]
        public IList<Resolution> Resolutions { get; set; }
    }
}