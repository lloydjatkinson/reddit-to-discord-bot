using System.Threading;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using RedditToDiscordBot.Services.Discord.Embeds;

namespace RedditToDiscordBot.Services.Discord
{
    public interface IDiscordWebHooks
    {
        Task<Result> SendMessageAsync(DiscordMessage message, CancellationToken cancellationToken = default);
    }
}