using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Quartz;

using RedditToDiscordBot.Services.RedditApi;

namespace RedditToDiscordBot
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IRedditPostsRetriever _redditPostsRetriever;
        private readonly ISchedulerFactory _schedulerFactory;

        public Worker(ILogger<Worker> logger, IHttpClientFactory httpClientFactory, IRedditPostsRetriever popularRedditPosts, ISchedulerFactory schedulerFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _redditPostsRetriever = popularRedditPosts;
            _schedulerFactory = schedulerFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var scheduler = await _schedulerFactory.GetScheduler(stoppingToken);
            var postingJob = JobBuilder.Create<ScheduledPoster>()
                .WithIdentity("posting-job", "common")
                .Build();


            var everyDayMorning = TriggerBuilder.Create()
                .WithIdentity("every-day-morning", "common")
                .WithSchedule(CronScheduleBuilder.AtHourAndMinuteOnGivenDaysOfWeek(10, 00, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday))
                .ForJob(postingJob)
                .Build();

            var everyDayEvening = TriggerBuilder.Create()
                .WithIdentity("every-day-evening", "common")
                .WithSchedule(CronScheduleBuilder.AtHourAndMinuteOnGivenDaysOfWeek(17, 30, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday))
                .ForJob(postingJob)
                .Build();

            var weekendAfternoon = TriggerBuilder.Create()
                .WithIdentity("every-day-evening", "common")
                .WithSchedule(CronScheduleBuilder.AtHourAndMinuteOnGivenDaysOfWeek(13, 00, DayOfWeek.Saturday, DayOfWeek.Sunday))
                .ForJob(postingJob)
                .Build();

            var spam = TriggerBuilder.Create()
                .WithIdentity("spam", "common")
                .WithSimpleSchedule(schedule => schedule
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .ForJob(postingJob)
                .Build();

            await scheduler.ScheduleJob(postingJob, new List<ITrigger>() { everyDayMorning, everyDayEvening, weekendAfternoon, spam }, true, stoppingToken);
            await scheduler.Start();


            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _redditPostsRetriever.Initialise();

            //    var popularPosts = (await _redditPostsRetriever.GetMostPopularTodayAsync("all")).Match(
            //        some: posts => posts,
            //        none: Enumerable.Empty<RedditPost>
            //    );

            //    var controversialPosts = (await _redditPostsRetriever.GetMostControversialTodayAsync("all")).Match(
            //        some: posts => posts,
            //        none: Enumerable.Empty<RedditPost>
            //    );

            //    if (popularPosts.Any())
            //    {
            //        try
            //        {
            //            var client = _httpClientFactory.CreateClient();

            //            var message = new DiscordMessage(
            //                popularPosts
            //                    .Take(2)
            //                    .Select(post => new DiscordEmbed(
            //                        post.Title, "Description",
            //                        post.PermaLink,
            //                        post.Posted,
            //                        0xFF0000,
            //                        new DiscordEmbedThumbnail(post.Thumbnail),
            //                        new DiscordEmbedFooter("Test", null))
            //                    )
            //            );

            //            var response = await client.PostAsJsonAsync("https://discordapp.com/api/webhooks/723211904792789082/MFfrr24sFhfgGMhP7ejdnKC8wx0-VB_xnrNaBm3AOL0nbs5t8rlArTjxXC6x861h97WH", message, stoppingToken);
            //            response.EnsureSuccessStatusCode();

            //        }
            //        catch (Exception exception)
            //        {
            //            _logger.LogError(exception, "Unable to post to Discord WebHook.");
            //        }
            //    }

            //    await Task.Delay(10000, stoppingToken);
            //}
        }
    }
}
