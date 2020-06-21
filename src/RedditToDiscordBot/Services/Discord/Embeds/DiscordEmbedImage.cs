namespace RedditToDiscordBot.Services.Discord.Embeds
{
    public class DiscordEmbedImage
    {
        public string Url { get; }

        public DiscordEmbedImage(string url)
        {
            Url = url;
        }
    }
}