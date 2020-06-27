using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.Caching.Memory;

namespace RedditToDiscordBot.Services.PreviouslyPosted
{
    public class PreviouslyPosted : IPreviouslyPosted
    {
        private readonly IMemoryCache _memoryCache;

        public PreviouslyPosted(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
    }
}
