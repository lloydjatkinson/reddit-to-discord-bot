using System;
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
        private ILogger<DiscordWebHooks> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public DiscordWebHooks(ILogger<DiscordWebHooks> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Result> SendMessageAsync(DiscordMessage message, CancellationToken cancellationToken = default)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                _logger.LogInformation("Posting message to Discord WebHook");
                var response = await client.PostAsJsonAsync("https://discordapp.com/api/webhooks/723211904792789082/MFfrr24sFhfgGMhP7ejdnKC8wx0-VB_xnrNaBm3AOL0nbs5t8rlArTjxXC6x861h97WH", message, cancellationToken);
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