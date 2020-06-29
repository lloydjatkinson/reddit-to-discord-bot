using System.Collections.Generic;
using System.Linq;

namespace RedditToDiscordBot.Configuration
{
    /// <summary>
    /// Configures options for Reddit post retrieval.
    /// </summary>
    public sealed class RedditConfiguration
    {
        /// <summary>
        /// Gets or sets the number of popular Reddit posts to post to Discord.
        /// </summary>
        /// <value>
        /// The number of popular Reddit posts to post to Discord.
        /// </value>
        public int NumberOfPopular { get; set; }

        /// <summary>
        /// Gets or sets the number of controversial Reddit posts to post to Discord.
        /// </summary>
        /// <value>
        /// The number of controversial Reddit posts to post to Discord.
        /// </value>
        public int NumberOfControversial { get; set; }

        /// <summary>
        /// Gets or sets the blacklisted subreddits. These will not be posted to Discord.
        /// </summary>
        /// <value>
        /// The blacklisted subreddits.
        /// </value>
        public IEnumerable<string> BlacklistedSubreddits { get; set; } = Enumerable.Empty<string>();
    }
}