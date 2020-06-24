using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RedditToDiscordBot.Services.RedditApi.Response
{
    public class AllAwarding
    {
        [JsonPropertyName("giver_coin_reward")]
        public int? GiverCoinReward { get; set; }

        [JsonPropertyName("subreddit_id")]
        public string SubredditId { get; set; }

        [JsonPropertyName("is_new")]
        public bool IsNew { get; set; }

        [JsonPropertyName("days_of_drip_extension")]
        public int DaysOfDripExtension { get; set; }

        [JsonPropertyName("coin_price")]
        public int CoinPrice { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("penny_donate")]
        public int? PennyDonate { get; set; }

        [JsonPropertyName("award_sub_type")]
        public string AwardSubType { get; set; }

        [JsonPropertyName("coin_reward")]
        public int CoinReward { get; set; }

        [JsonPropertyName("icon_url")]
        public string IconUrl { get; set; }

        [JsonPropertyName("days_of_premium")]
        public int DaysOfPremium { get; set; }

        [JsonPropertyName("resized_icons")]
        public IList<ResizedIcon> ResizedIcons { get; set; }

        [JsonPropertyName("icon_width")]
        public int IconWidth { get; set; }

        [JsonPropertyName("static_icon_width")]
        public int StaticIconWidth { get; set; }

        [JsonPropertyName("start_date")]
        public int? StartDate { get; set; }

        [JsonPropertyName("is_enabled")]
        public bool IsEnabled { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("end_date")]
        public object EndDate { get; set; }

        [JsonPropertyName("subreddit_coin_reward")]
        public int SubredditCoinReward { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("static_icon_height")]
        public int StaticIconHeight { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("resized_static_icons")]
        public IList<ResizedStaticIcon> ResizedStaticIcons { get; set; }

        [JsonPropertyName("icon_format")]
        public string IconFormat { get; set; }

        [JsonPropertyName("icon_height")]
        public int IconHeight { get; set; }

        [JsonPropertyName("penny_price")]
        public int? PennyPrice { get; set; }

        [JsonPropertyName("award_type")]
        public string AwardType { get; set; }

        [JsonPropertyName("static_icon_url")]
        public string StaticIconUrl { get; set; }
    }
}