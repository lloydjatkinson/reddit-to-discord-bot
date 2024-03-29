using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Quartz;

using RedditToDiscordBot.Configuration;
using RedditToDiscordBot.Services.Discord;
using RedditToDiscordBot.Services.Discord.Embeds;
using RedditToDiscordBot.Services.RedditApi;

namespace RedditToDiscordBot
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IOptions<ScheduleConfiguration> _scheduleConfiguration;
        private readonly IOptions<DiscordConfiguration> _discordConfiguration;
        private readonly IOptions<RedditConfiguration> _redditConfiguration;
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IDiscordWebHooks _discordWebHooks;

        public Worker(
            ILogger<Worker> logger,
            ISchedulerFactory schedulerFactory,
            IDiscordWebHooks discordWebHooks,
            IOptions<ScheduleConfiguration> scheduleConfiguration,
            IOptions<DiscordConfiguration> discordConfiguration,
            IOptions<RedditConfiguration> redditConfiguration)
        {
            _logger = logger;
            _scheduleConfiguration = scheduleConfiguration;
            _discordConfiguration = discordConfiguration;
            _redditConfiguration = redditConfiguration;
            _schedulerFactory = schedulerFactory;
            _discordWebHooks = discordWebHooks;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!_discordConfiguration.Value.Webhooks.Any())
            {
                _logger.LogError("No Discord Webhooks have ben configured.");
            }

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
                .WithIdentity("every-weekend-afternoon", "common")
                .WithSchedule(CronScheduleBuilder.AtHourAndMinuteOnGivenDaysOfWeek(13, 00, DayOfWeek.Saturday, DayOfWeek.Sunday))
                .ForJob(postingJob)
                .Build();

            var spam = TriggerBuilder.Create()
                .WithIdentity("spam", "common")
                .WithSimpleSchedule(schedule => schedule
                    .WithIntervalInSeconds(30)
                    .RepeatForever())
                .ForJob(postingJob)
                .Build();

            await scheduler.ScheduleJob(postingJob, new List<ITrigger>() { everyDayMorning, everyDayEvening, weekendAfternoon, spam }, true, stoppingToken);
            await scheduler.Start();

            await _discordWebHooks.SendMessageAsync(new DiscordMessage(string.Empty, new List<DiscordEmbed>()
            {
                new DiscordEmbed("RedditToDiscordBot running", string.Empty, null, DateTimeOffset.UtcNow, 0x00FF00, null, null, new List<DiscordEmbedField>()
                {
                    new DiscordEmbedField("OS", RuntimeInformation.OSDescription, false),
                    new DiscordEmbedField("OS Architecture / Process Architcture", $"{RuntimeInformation.OSArchitecture} / {RuntimeInformation.ProcessArchitecture}", false),
                    new DiscordEmbedField(".NET", RuntimeInformation.FrameworkDescription.ToString(), false),
                    new DiscordEmbedField("Webhooks", _discordConfiguration.Value.Webhooks.Count().ToString(), false),
                }),
            }));
        }
    }
}