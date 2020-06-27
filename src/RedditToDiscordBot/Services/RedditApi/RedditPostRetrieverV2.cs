using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using Microsoft.Extensions.Logging;

using RedditToDiscordBot.Services.RedditApi.Domain;
using RedditToDiscordBot.Services.RedditApi.Response;

namespace RedditToDiscordBot.Services.RedditApi
{
    public class RedditPostRetrieverV2 : IRedditPostsRetriever
    {
        private readonly ILogger<RedditPostRetrieverV2> _logger;
        private readonly HttpClient _httpClient;

        public RedditPostRetrieverV2(ILogger<RedditPostRetrieverV2> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://www.reddit.com");
        }

        public async Task<Maybe<IEnumerable<RedditPost>>> GetMostPopularTodayAsync(string subreddit = "all", CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Getting most popular posts today for: /r/{0}", subreddit);
                var response = await _httpClient.GetFromJsonAsync<RedditResponse>($"/r/{subreddit}/top/.json").ConfigureAwait(false);

                var posts = response.Data.Children.Select(post =>
                    new RedditPost(
                        permaLink: new Uri($"{_httpClient.BaseAddress}{post.Data.Permalink.Remove(0, 1)}"),
                        subreddit: $"/r/{post.Data.Subreddit}",
                        title: post.Data.Title,
                        posted: DateTimeOffset.FromUnixTimeSeconds((long)post.Data.CreatedUtc),
                        thumbnail: Uri.IsWellFormedUriString(post.Data.Thumbnail, UriKind.Absolute) ? new Uri(post.Data.Thumbnail) : null,
                        upVotes: post.Data.Ups,
                        awards: new Awards(post.Data.Gildings.Gid1, post.Data.Gildings.Gid2, post.Data.Gildings.Gid3),
                        comments: post.Data.NumComments
                    ));

                _logger.LogInformation("Successfully got most popular posts today for: /r/{0}", subreddit);
                return await Task.FromResult(Maybe<IEnumerable<RedditPost>>.From(posts)).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Unable to retrieve most popular posts today for: /r/{0}", subreddit);
                return await Task.FromResult(Maybe<IEnumerable<RedditPost>>.None).ConfigureAwait(false);
            }
        }

        public async Task<Maybe<IEnumerable<RedditPost>>> GetMostControversialTodayAsync(string subreddit = "all", CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Getting most controversial posts today for: /r/{0}", subreddit);
                var response = await _httpClient.GetFromJsonAsync<RedditResponse>($"/r/{subreddit}/controversial/.json").ConfigureAwait(false);

                var posts = response.Data.Children.Select(post =>
                    new RedditPost(
                        permaLink: new Uri($"{_httpClient.BaseAddress}{post.Data.Permalink.Remove(0, 1)}"),
                        subreddit: $"/r/{post.Data.Subreddit}",
                        title: post.Data.Title,
                        posted: DateTimeOffset.FromUnixTimeSeconds((long)post.Data.CreatedUtc),
                        thumbnail: Uri.IsWellFormedUriString(post.Data.Thumbnail, UriKind.Absolute) ? new Uri(post.Data.Thumbnail) : null,
                        upVotes: post.Data.Ups,
                        awards: new Awards(post.Data.Gildings.Gid1, post.Data.Gildings.Gid2, post.Data.Gildings.Gid3),
                        comments: post.Data.NumComments
                    ));

                _logger.LogInformation("Successfully got most controversial posts today for: /r/{0}", subreddit);
                return await Task.FromResult(Maybe<IEnumerable<RedditPost>>.From(posts)).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Unable to retrieve most controversial posts today for: /r/{0}", subreddit);
                return await Task.FromResult(Maybe<IEnumerable<RedditPost>>.None).ConfigureAwait(false);
            }
        }

        // TODO: This can go.
        public void Initialise()
        {
            throw new NotSupportedException();
        }
    }
}