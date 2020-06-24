using System.Text.Json.Serialization;

namespace RedditToDiscordBot.Services.RedditApi.Response
{
    public class Variants
    {
        [JsonPropertyName("gif")]
        public Gif Gif { get; set; }

        [JsonPropertyName("mp4")]
        public Mp4 Mp4 { get; set; }
    }
}