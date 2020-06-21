using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedditToDiscordBot.Services.Discord
{
    public interface IDiscordWebHooks
    {
        Task SendMessageAsync(DiscordMessage message);
    }
}