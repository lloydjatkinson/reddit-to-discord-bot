using System;
using System.Collections.Generic;
using System.Text;

namespace RedditToDiscordBot.Services.Discord.Embeds
{
    public sealed class DiscordEmbedField
    {
        public string Name { get; }

        public string Value { get; }

        public bool? Inline { get; }
        
        public DiscordEmbedField(string name, string value, bool? inline)
        {
            Name = name;
            Value = value;
            Inline = inline;
        }
    }
}
