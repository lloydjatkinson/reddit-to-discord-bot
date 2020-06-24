using System.Text.Json.Serialization;

namespace RedditToDiscordBot.Services.RedditApi.Response
{

    public class RedditResponse
    {
        [JsonPropertyName("kind")]
        public string Kind { get; set; }

        [JsonPropertyName("data")]
        public ResponseData Data { get; set; }
    }
}