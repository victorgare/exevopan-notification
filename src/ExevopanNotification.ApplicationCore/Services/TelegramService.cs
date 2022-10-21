using ExevopanNotification.ApplicationCore.Interfaces.Services;
using ExevopanNotification.Domain.Config;
using ExevopanNotification.Domain.Entities;
using ExevopanNotification.Domain.Notifications;
using Microsoft.Extensions.Options;
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

        public async Task Notify(List<Auction> auctions)
        {
            foreach (var auction in auctions)
            {
                var telegramNotification = new TelegramAuctionNotification(auction);
                await _telegramBotClient.SendTextMessageAsync(_telegramConfig.GroupId, telegramNotification.ToString(), replyMarkup: telegramNotification.GetInlineLinkButton());

            }
        }
    }
}
