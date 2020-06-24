using System.Text.Json.Serialization;

namespace RedditToDiscordBot.Services.RedditApi.Response
{
    public class Gildings
    {
        [JsonPropertyName("gid_1")]
        public int Gid1 { get; set; }

        [JsonPropertyName("gid_2")]
        public int Gid2 { get; set; }

        [JsonPropertyName("gid_3")]
        public int? Gid3 { get; set; }
    }
}