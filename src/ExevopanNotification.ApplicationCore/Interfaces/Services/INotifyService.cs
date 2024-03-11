using ExevopanNotification.Domain.Notifications;

namespace ExevopanNotification.ApplicationCore.Interfaces.Services
{
    public interface INotifyService
    {
        Task NotifyAuctions(List<AuctionNotification> auctionsNotifications);
        Task NotifyTelegram(string message);
        Task NotifyRuleBreaker(List<AuctionNotification> auctionsNotifications);
    }
}
