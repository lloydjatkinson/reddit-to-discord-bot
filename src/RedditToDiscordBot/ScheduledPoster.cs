using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using Microsoft.Extensions.Logging;

using Quartz;
using Quartz.Xml;

using RedditToDiscordBot.Services.Discord;
using RedditToDiscordBot.Services.RedditApi;

namespace RedditToDiscordBot
{
    public class ScheduledPoster : IJob
    {
        private readonly ILogger<ScheduledPoster> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IRedditPostsRetriever _redditPostsRetriever;
        private readonly IDiscordWebHooks _discordWebHooks;

        public ScheduledPoster(ILogger<ScheduledPoster> logger,
            IHttpClientFactory httpClientFactory,
            IRedditPostsRetriever redditPostsRetriever,
            IDiscordWebHooks discordWebHooks)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _redditPostsRetriever = redditPostsRetriever;
            _discordWebHooks = discordWebHooks;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("I ran!");

            _redditPostsRetriever.Initialise();

            var popular = (await _redditPostsRetriever.GetMostPopularTodayAsync("all")).Match(
                Some: posts => posts,
                None: () =>
                {
                    _logger.LogWarning("No popular posts to post to Discord.");
                    return Enumerable.Empty<RedditPost>();
                }
            );

            var controversial = (await _redditPostsRetriever.GetMostControversialTodayAsync("all")).Match(
                Some: posts => posts,
                None: () =>
                {
                    _logger.LogWarning("No controversial posts to post to Discord.");
                    return Enumerable.Empty<RedditPost>();
                }
            );

            



            await Task.CompletedTask;
        }
    }
}