﻿using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using Microsoft.Extensions.Logging;

using RedditToDiscordBot.Services.Discord.Embeds;

namespace RedditToDiscordBot.Services.Discord
{
    public class DiscordWebHooks : IDiscordWebHooks
    {
        private readonly ILogger<DiscordWebHooks> _logger;
        private readonly HttpClient _httpClient;

        public DiscordWebHooks(ILogger<DiscordWebHooks> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://discordapp.com/");
        }

        public async Task<Result> SendMessageAsync(DiscordMessage message, CancellationToken cancellationToken = default)
        {
            try
            {
                // /api/webhooks/723211904792789082/MFfrr24sFhfgGMhP7ejdnKC8wx0-VB_xnrNaBm3AOL0nbs5t8rlArTjxXC6x861h97WH
                //https://discordapp.com/api/webhooks/720976101702107226/2m5l1g92z1g5vuUgWJsz-08cTj6198KR9I4MtYgtz6SqrLXFdMOzrelJWEBZHYFj0wqq

                https://discordapp.com/api/webhooks/723211904792789082/MFfrr24sFhfgGMhP7ejdnKC8wx0-VB_xnrNaBm3AOL0nbs5t8rlArTjxXC6x861h97WH
                var response = await _httpClient.PostAsJsonAsync("/api/webhooks/723211904792789082/MFfrr24sFhfgGMhP7ejdnKC8wx0-VB_xnrNaBm3AOL0nbs5t8rlArTjxXC6x861h97WH", message, cancellationToken).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                _logger.LogInformation("Posted message OK");
                return Result.Success();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Unable to post message to Discord WebHook");
                return Result.Failure(exception.Message);
            }
        }
    }
}