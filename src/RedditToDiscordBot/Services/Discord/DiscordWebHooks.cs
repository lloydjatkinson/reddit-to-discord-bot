using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using RedditToDiscordBot.Configuration;
using RedditToDiscordBot.Services.Discord.Embeds;

namespace RedditToDiscordBot.Services.Discord
{
    public class DiscordWebHooks : IDiscordWebHooks
    {
        private readonly ILogger<DiscordWebHooks> _logger;
        private readonly IOptions<DiscordConfiguration> _discordConfiguration;
        private readonly HttpClient _httpClient;

        public DiscordWebHooks(
            ILogger<DiscordWebHooks> logger,
            IOptions<DiscordConfiguration> discordConfiguration,
            HttpClient httpClient)
        {
            _logger = logger;
            _discordConfiguration = discordConfiguration;
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://discordapp.com/");
        }

        public async Task<Result> SendMessageAsync(DiscordMessage message, CancellationToken cancellationToken = default)
        {
            var postResults = new List<Result>();

            foreach (var webhook in _discordConfiguration.Value.Webhooks)
            {
                try
                {
                    var response = await _httpClient.PostAsJsonAsync($"/api/webhooks{webhook}", message, cancellationToken).ConfigureAwait(false);

                    postResults.Add(Result.SuccessIf(response.IsSuccessStatusCode, "Unable to post message to Discord Webhook"));

                    if (response.IsSuccessStatusCode)
                    {
                        _logger.LogInformation("Posted message OK");
                    }
                    else
                    {
                        _logger.LogError("Unable to post message to Discord Webhook");
                    }
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Unable to post message to Discord Webhook");
                    postResults.Add(Result.Failure(exception.Message));
                }
            }

            return Result.Combine(postResults);
        }
    }
}