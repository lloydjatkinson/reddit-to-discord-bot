namespace RedditToDiscordBot.Services.Discord.Embeds
{
    public class DiscordEmbedThumbnail
    {
        public string Url { get; }

        public DiscordEmbedThumbnail(string url)
        {
            Url = url;
        }
    }
}
