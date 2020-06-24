﻿using System.Text.Json.Serialization;

namespace RedditToDiscordBot.Services.RedditApi.Response
{
    public class RedditVideo
    {
        [JsonPropertyName("fallback_url")]
        public string FallbackUrl { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("scrubber_media_url")]
        public string ScrubberMediaUrl { get; set; }

        [JsonPropertyName("dash_url")]
        public string DashUrl { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("hls_url")]
        public string HlsUrl { get; set; }

        [JsonPropertyName("is_gif")]
        public bool IsGif { get; set; }

        [JsonPropertyName("transcoding_status")]
        public string TranscodingStatus { get; set; }
    }
}