using ExevopanNotification.Domain.Entities;
using ExevopanNotification.Domain.Notifications.Base;
using System.Text;

namespace ExevopanNotification.Domain.Notifications
{
    public class RuleBreakerNotification : AuctionBaseNotification
    {

        public RuleBreakerNotification(Auction auction) : base(auction)
        {
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($@"{VocationIcon}{_auction.VocationId} [{_auction.Level}] - {_auction.Nickname}");
            return stringBuilder.ToString();
        }
    }
}
