//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//using CSharpFunctionalExtensions;

//using Microsoft.Extensions.Logging;

//using Reddit;
//using Reddit.Inputs;

//namespace RedditToDiscordBot.Services.RedditApi
//{
//    public class RedditPostsdiscordEmbed : IRedditPostsRetriever
//    {
//        private readonly ILogger<RedditPostsdiscordEmbed> _logger;
//        private RedditClient _redditClient;

//        public RedditPostsdiscordEmbed(ILogger<RedditPostsdiscordEmbed> logger)
//        {
//            _logger = logger;
//        }

//        // TODO: Don't like this temporal coupling - will introduce a factory.
//        public void Initialise()
//        {
//            _logger.LogInformation("Initialising Reddit Client.");
//            _redditClient = new RedditClient("aJYSGXcFwYacHg", "546245828399-2ruCLx2UMkhE6nK3I7juTe8R9M0");
//        }

//        public async Task<Maybe<IEnumerable<RedditPost>>> GetMostPopularTodayAsync(string subreddit, CancellationToken cancellationToken = default)
//        {
//            try
//            {
//                _logger.LogInformation("Getting most popular posts today for: /r/{0}", subreddit);
//                var popular = _redditClient.Subreddit(subreddit).Posts.Best;
//                var topPost = _redditClient.Models.Listings.Top(new TimedCatSrListingInput(), "programming").Data;

//                var posts = popular.Select(post => new RedditPost(new Uri($"https://reddit.com{post.Permalink}"), post.Title, post.Created, Uri.IsWellFormedUriString(post.Listing.Thumbnail, UriKind.Absolute) ? post.Listing.Thumbnail : string.Empty));

//                _logger.LogInformation("Successfully got most popular posts today for: /r/{0}", subreddit);
//                return await Task.FromResult(Maybe<IEnumerable<RedditPost>>.From(posts));
//            }
//            catch (Exception exception)
//            {
//                _logger.LogError(exception, "Unable to retrieve most popular posts today for: /r/{0}", subreddit);

//                return await Task.FromResult(Maybe<IEnumerable<RedditPost>>.None);
//            }
//        }

//        public async Task<Maybe<IEnumerable<RedditPost>>> GetMostControversialTodayAsync(string subreddit, CancellationToken cancellationToken = default)
//        {
//            try
//            {
//                _logger.LogInformation("Getting most controversial posts today for: /r/{0}", subreddit);
//                var controversial = _redditClient.Subreddit(subreddit).Posts.GetControversial();

//                var posts = controversial.Select(post => new RedditPost(new Uri($"https://reddit.com{post.Permalink}"), post.Title, post.Created, Uri.IsWellFormedUriString(post.Listing.Thumbnail, UriKind.Absolute) ? post.Listing.Thumbnail : string.Empty));

//                _logger.LogInformation("Successfully got most popular posts today for: /r/{0}", subreddit);
//                return await Task.FromResult(Maybe<IEnumerable<RedditPost>>.From(posts));
//            }
//            catch (Exception exception)
//            {
//                _logger.LogError(exception, "Unable to retrieve most controversial posts today for: /r/{0}", subreddit);

//                return await Task.FromResult(Maybe<IEnumerable<RedditPost>>.None);
//            }
//        }
//    }
//}