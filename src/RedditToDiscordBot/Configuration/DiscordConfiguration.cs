using System;
using System.Collections.Generic;
using System.Linq;

namespace RedditToDiscordBot.Configuration
{
    /// <summary>
    /// Configures options for posting to Discord Webhooks.
    /// </summary>
    public sealed class DiscordConfiguration
    {
        /// <summary>
        /// Gets or sets the collection of Discord Webhook URI's to post to.
        /// </summary>
        /// <value>
        /// The webhooks.
        /// </value>
        public IEnumerable<string> Webhooks { get; set; }

        /// <summary>
        /// Gets or sets the delay between posts to each Discord Webhooks.
        /// </summary>
        /// <remarks>
        /// This option is to prevent posting at a volume/frequency that would violate the Discord Webhook rate limiting.
        /// It only has an impact if a significant number of Webhooks have been configured.
        /// 
        /// For example, if the bot is configured to post to a single Webhook then this setting will make no difference.
        /// If the bot is configured to post to ten Webhooks, it will wait the specified value after posting to one before posting to the next one.
        /// </remarks>
        /// <value>
        /// The delay between posts. Default value is two seconds.
        /// </value>
        public TimeSpan DelayBetweenPosts { get; set; } = TimeSpan.FromSeconds(2);
    }
}