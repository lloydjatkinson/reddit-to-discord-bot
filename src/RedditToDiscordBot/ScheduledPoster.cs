using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using Microsoft.Extensions.Logging;

using Quartz;

using RedditToDiscordBot.Services.Discord;
using RedditToDiscordBot.Services.Discord.Embeds;
using RedditToDiscordBot.Services.RedditApi;
using RedditToDiscordBot.Services.Uptime;

namespace RedditToDiscordBot
{
    public class ScheduledPoster : IJob
    {
        private readonly ILogger<ScheduledPoster> _logger;
        private readonly IUptime _uptime;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IRedditPostsRetriever _redditPostsRetriever;
        private readonly IDiscordWebHooks _discordWebHooks;

        public ScheduledPoster(ILogger<ScheduledPoster> logger,
            IHttpClientFactory httpClientFactory,
            IRedditPostsRetriever redditPostsRetriever,
            IDiscordWebHooks discordWebHooks,
            IUptime uptime)
        {
            _logger = logger;
            _uptime = uptime;
            _httpClientFactory = httpClientFactory;
            _redditPostsRetriever = redditPostsRetriever;
            _discordWebHooks = discordWebHooks;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("I ran!");
            _logger.LogInformation("Uptime: {0}", _uptime.Bot.Friendly);

            //_redditPostsRetriever.Initialise();

            var popular = (await _redditPostsRetriever.GetMostPopularTodayAsync("programming")).Match(
                Some: posts => posts,
                None: () =>
                {
                    _logger.LogWarning("No popular posts to post to Discord.");
                    return Enumerable.Empty<RedditPost>();
                }
            );

            var controversial = (await _redditPostsRetriever.GetMostControversialTodayAsync("programming")).Match(
                Some: posts => posts,
                None: () =>
                {
                    _logger.LogWarning("No controversial posts to post to Discord.");
                    return Enumerable.Empty<RedditPost>();
                }
            );

            var posts = popular.Concat(controversial);

            //var posts = new List<RedditPost>() { new RedditPost(new Uri("https://google.com"), "Test Message", DateTimeOffset.UtcNow, null) };

            if (posts.Any())
            {
                // For now, just grab the first three. Eventually this could be "take three random" to mix things up a bit, maybe look at actual votes or awards.
                var subset = posts.Take(3);

                var highlights = subset
                    .Select(post => new DiscordEmbed(
                       title: post.Title,
                       description: ":medal:",
                       url: post.PermaLink,
                       timestamp: post.Posted,
                       color: 0xFF0000, // TODO: Find out a way of getting the nearest best colour - subreddit colour, user flair, etc. Maybe even a list of subreddit colours in config?
                       thumbnail: new DiscordEmbedThumbnail(post.Thumbnail),
                       footer: null
                    ));

                await _discordWebHooks.SendMessageAsync(new DiscordMessage(string.Empty, highlights));
            }

            await Task.CompletedTask;
        }
    }
}