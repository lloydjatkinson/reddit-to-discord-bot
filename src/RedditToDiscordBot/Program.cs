using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Quartz.DependencyInjection.Microsoft.Extensions;

using RedditToDiscordBot.Services.Discord;
using RedditToDiscordBot.Services.RedditApi;
using RedditToDiscordBot.Services.Uptime;

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
#if DEBUG
                //TODO: This will be handled by using something like Serilog.
                .ConfigureLogging(loggingBuilder =>
                {
                    loggingBuilder.AddConsole(console =>
                    {
                        console.TimestampFormat = "[HH:mm:ss] ";
                    });
                })
#endif
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IUptime, Uptime>();
                    services.AddQuartz();
                    services.AddHttpClient();
                    services.AddTransient<IRedditPostsRetriever, RedditPostsdiscordEmbed>();
                    services.AddTransient<IDiscordWebHooks, DiscordWebHooks>();
                    services.AddTransient<ScheduledPoster>();
                    services.AddHostedService<Worker>();
                });
    }
}