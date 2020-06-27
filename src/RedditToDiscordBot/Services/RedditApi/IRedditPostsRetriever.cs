using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using RedditToDiscordBot.Services.RedditApi.Domain;

namespace RedditToDiscordBot.Services.RedditApi
{
    public interface IRedditPostsRetriever
    {
        Task<Maybe<IEnumerable<RedditPost>>> GetMostPopularTodayAsync(string subreddit, CancellationToken cancellationToken = default);

        Task<Maybe<IEnumerable<RedditPost>>> GetMostControversialTodayAsync(string subreddit, CancellationToken cancellationToken = default);

        void Initialise();
    }
}