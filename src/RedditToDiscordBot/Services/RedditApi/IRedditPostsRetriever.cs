using System.Collections.Generic;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using Optional;

namespace RedditToDiscordBot.Services.RedditApi
{
    public interface IRedditPostsRetriever
    {
        Task<Maybe<IEnumerable<RedditPost>>> GetMostPopularTodayAsync(string subreddit);

        Task<Maybe<IEnumerable<RedditPost>>> GetMostControversialTodayAsync(string subreddit);

        void Initialise();
    }
}