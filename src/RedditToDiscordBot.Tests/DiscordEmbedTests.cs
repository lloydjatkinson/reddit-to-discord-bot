using System;

using AutoFixture;

using RedditToDiscordBot.Services.Discord.Embeds;

using Shouldly;

using Xunit;

namespace RedditToDiscordBot.Tests
{
    public class DiscordEmbedTests
    {
        [Fact]
        public void DiscordEmbedShouldThrowWithMoreThanTenEmbeds()
        {
            var fixture = new Fixture();
            var embeds = fixture.CreateMany<DiscordEmbed>(11);

            // TODO: USe a builder pattern rather than needing to pass string.Empty and nulls etc.
            Should.Throw<ArgumentOutOfRangeException>(() => new DiscordMessage(string.Empty, embeds));
        }
    }
}