using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RedditToDiscordBot.Services.RedditApi.Response
{
    public class ResponseData
    {
        [JsonPropertyName("modhash")]
        public string Modhash { get; set; }

        [JsonPropertyName("dist")]
        public int Dist { get; set; }

        [JsonPropertyName("children")]
        public IList<Child> Children { get; set; }

        [JsonPropertyName("after")]
        public string After { get; set; }

        [JsonPropertyName("before")]
        public object Before { get; set; }
    }
}