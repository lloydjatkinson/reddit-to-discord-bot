namespace RedditToDiscordBot.Services.RedditApi.Domain
{
    public sealed class Awards
    {
        public int? Silver { get; }

        public int? Gold { get; }

        public int? Platinum { get; }

        public Awards(int? silver, int? gold, int? platinum)
        {
            Silver = silver;
            Gold = gold;
            Platinum = platinum;
        }
    }
}