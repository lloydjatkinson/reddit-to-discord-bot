using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Quartz.DependencyInjection.Microsoft.Extensions;

using RedditToDiscordBot.Services.Discord;
using RedditToDiscordBot.Services.RedditApi;

namespace RedditToDiscordBot
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddQuartz();
                    services.AddHttpClient();
                    services.AddTransient<IRedditPostsRetriever, RedditPostsdiscordEmbed>();
                    services.AddTransient<IDiscordWebHooks, DiscordWebHooks>();
                    services.AddTransient<ScheduledPoster>();
                    services.AddHostedService<Worker>();
                });
    }
}