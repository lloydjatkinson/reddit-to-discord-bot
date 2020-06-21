using System;

namespace RedditToDiscordBot.Services.RedditApi
{
    public class RedditPost
    {
        public Uri PermaLink { get; }

        public string Title { get; }

        public DateTimeOffset Posted { get; }

        public string Thumbnail { get; set; }

        public RedditPost(Uri permaLink, string title, DateTimeOffset posted, string thumbnail)
        {
            PermaLink = permaLink;
            Title = title;
            Posted = posted;
            Thumbnail = thumbnail;
        }
    }
}