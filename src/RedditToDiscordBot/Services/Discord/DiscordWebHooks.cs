using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace RedditToDiscordBot.Services.Discord
{
    public class DiscordWebHooks : IDiscordWebHooks
    {
        private ILogger<DiscordWebHooks> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public DiscordWebHooks(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task SendMessageAsync(DiscordMessage message)
        {
            var client = _httpClientFactory.CreateClient();

            //client
        }
    }
}