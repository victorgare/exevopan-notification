using ExevopanNotification.Domain.Entities;

namespace ExevopanNotification.Domain.Notifications
{
    public class AuctionNotification
    {
        public Auction Auction { get; set; }
        public int PriceTrend { get; set; }
    }
}
