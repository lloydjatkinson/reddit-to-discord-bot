using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using Microsoft.Extensions.Logging;

using Optional;

using Reddit;

namespace RedditToDiscordBot.Services.RedditApi
{
    public class RedditPostsdiscordEmbed : IRedditPostsRetriever
    {
        private readonly ILogger<RedditPostsdiscordEmbed> _logger;
        private RedditClient _redditClient;

        public RedditPostsdiscordEmbed(ILogger<RedditPostsdiscordEmbed> logger)
        {
            _logger = logger;
        }

        // TODO: Don't like this temporal coupling - will introduce a factory.
        public void Initialise()
        {
            _logger.LogInformation("Initialising Reddit Client.");
            _redditClient = new RedditClient("aJYSGXcFwYacHg", "546245828399-2ruCLx2UMkhE6nK3I7juTe8R9M0");
        }

        public async Task<Maybe<IEnumerable<RedditPost>>> GetMostPopularTodayAsync(string subreddit)
        {
            try
            {
                _logger.LogInformation("Getting most popular posts today for: /r/{0}", subreddit);
                var popular = _redditClient.Subreddit(subreddit).Posts.GetBest();

                var posts = popular.Select(post => new RedditPost(new Uri($"https://reddit.com{post.Permalink}"), post.Title, post.Created, Uri.IsWellFormedUriString(post.Listing.Thumbnail, UriKind.Absolute) ? post.Listing.Thumbnail : string.Empty));

                return await Task.FromResult(Maybe<IEnumerable<RedditPost>>.From(posts));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Unable to retrieve most popular posts today for: /r/{0}", subreddit);

                return await Task.FromResult(Maybe<IEnumerable<RedditPost>>.None);
            }
        }

        public async Task<Maybe<IEnumerable<RedditPost>>> GetMostControversialTodayAsync(string subreddit)
        {
            try
            {
                _logger.LogInformation("Getting most controversial posts today for: /r/{0}", subreddit);
                var controversial = _redditClient.Subreddit(subreddit).Posts.Controversial;

                var posts = controversial.Select(post => new RedditPost(new Uri($"https://reddit.com{post.Permalink}"), post.Title, post.Created, Uri.IsWellFormedUriString(post.Listing.Thumbnail, UriKind.Absolute) ? post.Listing.Thumbnail : string.Empty));

                return await Task.FromResult(Maybe<IEnumerable<RedditPost>>.From(posts));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Unable to retrieve most controversial posts today for: /r/{0}", subreddit);

                return await Task.FromResult(Maybe<IEnumerable<RedditPost>>.None);
            }
        }



        //public async Task<Option<IEnumerable<RedditPost>>> GetMostPopularTodayAsync(string subreddit)
        //{
        //    try
        //    {
        //        _logger.LogInformation("Getting most popular posts today for: /r/{0}", subreddit);
        //        var popular = _redditClient.Subreddit(subreddit).Posts.GetBest();

        //        var posts = popular.Select(post => new RedditPost(new Uri($"https://reddit.com{post.Permalink}"), post.Title, post.Created, Uri.IsWellFormedUriString(post.Listing.Thumbnail, UriKind.Absolute) ? post.Listing.Thumbnail : string.Empty));

        //        return await Task.FromResult(Option.Some(posts));
        //    }
        //    catch (Exception exception)
        //    {
        //        _logger.LogError(exception, "Unable to retrieve most popular posts today for: /r/{0}", subreddit);

        //        return await Task.FromResult(Option.None<IEnumerable<RedditPost>>());
        //    }
        //}

        //public async Task<Option<IEnumerable<RedditPost>>> GetMostControversialTodayAsync(string subreddit)
        //{
        //    try
        //    {
        //        _logger.LogInformation("Getting most controversial posts today for: /r/{0}", subreddit);
        //        var popular = _redditClient.Subreddit(subreddit).Posts.Controversial;

        //        var posts = popular.Select(post => new RedditPost(new Uri($"https://reddit.com{post.Permalink}"), post.Title, post.Created, post.Listing.Thumbnail));

        //        return await Task.FromResult(Option.Some(posts));
        //    }
        //    catch (Exception exception)
        //    {
        //        _logger.LogError(exception, "Unable to retrieve most controversial posts today for: /r/{0}", subreddit);

        //        return await Task.FromResult(Option.None<IEnumerable<RedditPost>>());
        //    }
        //}
    }
}