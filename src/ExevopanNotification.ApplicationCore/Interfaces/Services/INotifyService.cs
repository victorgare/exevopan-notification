using ExevopanNotification.Domain.Notifications;

namespace ExevopanNotification.ApplicationCore.Interfaces.Services
{
    public interface INotifyService
    {
        Task NotifyAuctions(List<AuctionNotification> auctionsNotifications);
        Task NotifyTelegram(string message);
    }
}
