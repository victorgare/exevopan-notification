﻿using ExevopanNotification.ApplicationCore.Interfaces.Services;
using ExevopanNotification.Domain.Config;
using ExevopanNotification.Domain.Notifications;
using Microsoft.Extensions.Options;
using System.Text;
using Telegram.Bot;

namespace ExevopanNotification.ApplicationCore.Services
{
    public class TelegramService : IAuctionNotification
    {
        private readonly TelegramBotClient _telegramBotClient;
        private readonly TelegramConfig _telegramConfig;

        public TelegramService(IOptions<ApplicationConfig> appConfig)
        {
            _telegramConfig = appConfig.Value.TelegramConfig;
            _telegramBotClient = new TelegramBotClient(_telegramConfig.Key);
        }

        public async Task Notify(List<AuctionNotification> auctionsNotifications)
        {
            foreach (var auctionNotification in auctionsNotifications)
            {
                var telegramNotification = new TelegramAuctionNotification(auctionNotification.Auction, auctionNotification.PriceTrend);
                await _telegramBotClient.SendTextMessageAsync(_telegramConfig.GroupId, telegramNotification.ToString(), replyMarkup: telegramNotification.GetInlineLinkButton());
            }
        }
        public async Task NotifyRuleBreaker(List<AuctionNotification> auctionsNotifications)
        {
            var message = new StringBuilder();
            foreach (var auctionNotification in auctionsNotifications)
            {
                var telegramNotification = new RuleBreakerNotification(auctionNotification.Auction);
                message.AppendLine(telegramNotification.ToString());
            }

            await _telegramBotClient.SendTextMessageAsync(_telegramConfig.RuleBreakerGroupId, message.ToString(), allowSendingWithoutReply: true);
        }

        public async Task Notify(string message)
        {
            await _telegramBotClient.SendTextMessageAsync(_telegramConfig.GroupId, message, allowSendingWithoutReply: true);
        }
    }
}
