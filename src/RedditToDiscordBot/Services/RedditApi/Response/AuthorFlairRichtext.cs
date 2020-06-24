using System.Text.Json.Serialization;

namespace RedditToDiscordBot.Services.RedditApi.Response
{
    public class AuthorFlairRichtext
    {
        [JsonPropertyName("e")]
        public string E { get; set; }

        [JsonPropertyName("t")]
        public string T { get; set; }

        [JsonPropertyName("a")]
        public string A { get; set; }

        [JsonPropertyName("u")]
        public string U { get; set; }
    }
}