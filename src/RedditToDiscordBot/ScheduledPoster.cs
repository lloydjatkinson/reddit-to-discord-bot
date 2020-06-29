using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using Humanizer;

using Microsoft.Extensions.Logging;

using Quartz;

using RedditToDiscordBot.Services.Discord;
using RedditToDiscordBot.Services.Discord.Embeds;
using RedditToDiscordBot.Services.RedditApi;
using RedditToDiscordBot.Services.RedditApi.Domain;
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

            // For now, just grab the first three. Eventually this could be "take three random" to mix things up a bit, maybe look at actual votes or awards, and don't post any we've already posted.
            var posts = popular.Take(2).Concat(controversial.Take(1));

            if (posts.Any())
            {
                var highlights = posts
                    .Select(post => new DiscordEmbed(
                       title: post.Title,
                       description: post.Subreddit,
                       url: post.PermaLink,
                       timestamp: post.Posted,
                       color: 0xFF4F00, // TODO: Find out a way of getting the nearest best colour - subreddit colour, user flair, etc. Maybe even a list of subreddit colours in config?
                       thumbnail: new DiscordEmbedThumbnail(post.Thumbnail),
                       footer: null,
                       fields: new List<DiscordEmbedField>()
                           {
                               new DiscordEmbedField("Upvotes", $":arrow_up: {post.UpVotes.ToMetric(false, true, 1)}", true),
                               //post.Awards != null ? new DiscordEmbedField("Awards", $":yellow_circle:{post.Awards.Platinum}", true) : null,
                               new DiscordEmbedField("Comments", post.Comments.ToString(), true)
                           }
                    ));

                (await _discordWebHooks.SendMessageAsync(new DiscordMessage(string.Empty, highlights)).ConfigureAwait(false)).Match(
                    onSuccess: () => _logger.LogInformation("Successfully posted to all configured Webhooks"),
                    onFailure: error => _logger.LogError("Could not post to all configured Webhooks: {0}", error)
                );

            }

            await Task.CompletedTask;
        }
    }
}