﻿using ExevopanNotification.ApplicationCore.Interfaces.Services;
using ExevopanNotification.Domain.Config;
using ExevopanNotification.Domain.Notifications;
using ExevopanNotification.Utils.Utils;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace ExevopanNotification.ApplicationCore.Services
{
    public class TelegramService : IAuctionNotification
    {
        private readonly TelegramBotClient _telegramBotClient;
        private readonly TelegramConfig _telegramConfig;
        private readonly IMemoryCache _memoryCache;

        public TelegramService(IOptions<ApplicationConfig> appConfig, IMemoryCache memoryCache)
        {
            _telegramConfig = appConfig.Value.TelegramConfig;
            _telegramBotClient = new TelegramBotClient(_telegramConfig.Key);
            _memoryCache = memoryCache;
            _memoryCache = memoryCache;
        }

        public async Task Notify(List<AuctionNotification> auctionsNotifications)
        {
            foreach (var auctionNotification in auctionsNotifications)
            {
                var telegramNotification = new TelegramAuctionNotification(auctionNotification.Auction, auctionNotification.PriceTrend);
                await _telegramBotClient.SendMessage(
                    chatId: _telegramConfig.GroupId,
                    text: telegramNotification.ToString(),
                    replyMarkup: telegramNotification.GetInlineLinkButton());
            }
        }
        public async Task NotifyRuleBreaker(List<AuctionNotification> auctionsNotifications)
        {
            var servers = auctionsNotifications.Select(c => c.Auction.ServerData.ServerName).Distinct().ToArray();

            foreach (var server in servers)
            {
                var newAuction = new StringBuilder();
                var currentAuction = new StringBuilder();
                var lastMessage = GetLastMessage<string[]>(_telegramConfig.RuleBreakerGroupId, server) ?? [];
                var serverAuctionNotifications = auctionsNotifications.Where(c => c.Auction.ServerData.ServerName == server);

                foreach (var auctionNotification in serverAuctionNotifications)
                {
                    var telegramNotification = new RuleBreakerNotification(auctionNotification.Auction);

                    var nickName = auctionNotification.Auction.Nickname;

                    if (lastMessage.Contains(nickName))
                    {
                        currentAuction.AppendLine(telegramNotification.ToString());
                        // remove element from array
                        lastMessage = lastMessage.Where(c => !c.Contains(nickName)).ToArray();
                    }
                    else
                    {
                        newAuction.AppendLine(telegramNotification.ToString());
                    }
                }

                var message = new StringBuilder()
                    .AppendLine(server.ToBold())
                    .Add("NOVOS LEILOES".ToBold(), newAuction)
                    .Add("FINALIZADOS".ToBold(), string.Join(Environment.NewLine, lastMessage))
                    .Add("CONTINUAM".ToBold(), currentAuction);

                SetLastMessage(_telegramConfig.RuleBreakerGroupId, server, serverAuctionNotifications.Select(c => c.Auction.Nickname).ToArray());

                await _telegramBotClient.SendMessage(
                    chatId: _telegramConfig.RuleBreakerGroupId,
                    text: message.ToString(),
                    parseMode: ParseMode.Markdown);
            }

        }

        public async Task Notify(string message)
        {
            await _telegramBotClient.SendMessage(
                chatId: _telegramConfig.GroupId,
                text: message);
        }

        private void SetLastMessage<T>(string groupId, string serverName, T message)
        {
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            };

            _memoryCache.Set($"{groupId}-{serverName}", message, cacheOptions);
        }
        private T? GetLastMessage<T>(string groupId, string serverName)
        {
            if (_memoryCache.TryGetValue($"{groupId}-{serverName}", out T data))
            {
                return data;
            }

            return default;
        }


    }

    internal static class ExtensionHelper
    {
        internal static StringBuilder Add(this StringBuilder sb, string label, StringBuilder value)
        {
            if (value.Length > 0)
            {
                sb.AddLines(label, value.ToString());
            }

            return sb;
        }

        internal static StringBuilder Add(this StringBuilder sb, string label, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                sb.AddLines(label, value);
            }

            return sb;
        }

        private static StringBuilder AddLines(this StringBuilder sb, string label, string value)
        {
            return sb.AppendLine(label)
                  .AppendLine(value.ToString())
                  .AppendLine(Environment.NewLine);
        }
    }
}
