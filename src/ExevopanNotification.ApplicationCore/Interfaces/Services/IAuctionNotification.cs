using ExevopanNotification.Domain.Notifications;

namespace ExevopanNotification.ApplicationCore.Interfaces.Services
{
    public interface IAuctionNotification
    {
        Task Notify(List<AuctionNotification> auctionsNotifications);
        Task Notify(string message);
    }
}
