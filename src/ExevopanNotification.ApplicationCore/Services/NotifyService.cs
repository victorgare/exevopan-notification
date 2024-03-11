using ExevopanNotification.ApplicationCore.Interfaces.Services;
using ExevopanNotification.Domain.Notifications;

namespace ExevopanNotification.ApplicationCore.Services
{
    public class NotifyService : INotifyService
    {
        private readonly IEnumerable<IAuctionNotification> _auctionNotifications;

        public NotifyService(IEnumerable<IAuctionNotification> auctionNotifications)
        {
            _auctionNotifications = auctionNotifications;
        }

        public async Task NotifyAuctions(List<AuctionNotification> auctionsNotifications)
        {
            foreach (var notification in _auctionNotifications)
            {
                await notification.Notify(auctionsNotifications);
            }
        }

        public async Task NotifyRuleBreaker(List<AuctionNotification> auctionsNotifications)
        {
            foreach (var notification in _auctionNotifications)
            {
                await notification.NotifyRuleBreaker(auctionsNotifications);
            }
        }

        public async Task NotifyTelegram(string message)
        {
            var telegramService = _auctionNotifications.FirstOrDefault(c => c is TelegramService);

            if (telegramService != null)
            {
                await telegramService.Notify(message);
            }
            else
            {
                throw new InvalidOperationException("No telegram service declared");
            }
        }
    }
}
