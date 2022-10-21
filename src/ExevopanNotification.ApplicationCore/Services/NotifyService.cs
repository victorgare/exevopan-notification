using ExevopanNotification.ApplicationCore.Interfaces.Services;
using ExevopanNotification.Domain.Entities;

namespace ExevopanNotification.ApplicationCore.Services
{
    public class NotifyService : INotifyService
    {
        private readonly IEnumerable<IAuctionNotification> _auctionNotifications;

        public NotifyService(IEnumerable<IAuctionNotification> auctionNotifications)
        {
            _auctionNotifications = auctionNotifications;
        }

        public async Task NotifyAuctions(List<Auction> auctions)
        {
            foreach (var notification in _auctionNotifications)
            {
                await notification.Notify(auctions);
            }
        }
    }
}
