# RedditToDiscordBot

RedditToDiscordBot is a "bot" which posts some popular as well as some controversial (downvoted) Reddit posts to any server it is invited to. It's basically just for fun and to find interesting posts or the latest memes. It runs on a schedule - a couple of times a day it will query Reddit's API and then post them to Discord in the form of a message with embeds.

It's written in C# and runs on .NET Core. It uses a .NET Core 3.0+ Worker Service and Quartz.NET Scheduler for the posting schedule.

Currently it only posts twice a day on weekdays (morning and evening) and three times a day on a weekend (morning, afternoon, evening). This is to reduce any perceived "it's posting too much" and also to allow Reddit's sorting algorithms to find the best (or most controversial) posts for your enjoyment.

## Installation

```
instructions
```

## Usage

```
usage
```

## Like this bot?

<a href="https://www.buymeacoffee.com/lloyd" target="_blank"><img src="https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png" alt="Buy Me A Coffee" style="height: 41px !important;width: 174px !important;box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;-webkit-box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;" ></a>

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change. I may or may not approve of any changes I feel change the purpose of the bot too much.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)