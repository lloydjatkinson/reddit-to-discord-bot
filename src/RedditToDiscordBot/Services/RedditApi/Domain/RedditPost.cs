using System;
using System.Diagnostics;

namespace RedditToDiscordBot.Services.RedditApi.Domain
{
    [DebuggerDisplay("{PermaLink}")]
    public class RedditPost
    {
        public Uri PermaLink { get; }

        public string Title { get; }

        public string Subreddit { get; }

        public DateTimeOffset Posted { get; }

        public Uri Thumbnail { get; }

        public int UpVotes { get; }

        public Awards Awards { get; }

        public int Comments { get; }

        public RedditPost(Uri permaLink, string subreddit, string title, DateTimeOffset posted, Uri thumbnail, int upVotes, Awards awards, int comments)
        {
            PermaLink = permaLink;
            Subreddit = subreddit;
            Title = title;
            Posted = posted;
            Thumbnail = thumbnail;
            UpVotes = upVotes;
            Awards = awards;
            Comments = comments;
        }
    }
}