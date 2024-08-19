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
            stringBuilder.AppendLine($@" - {VocationIcon}{_auction.VocationId} [{_auction.Level}] - {_auction.Nickname} - 💰({PricePerMillion}tc/kk)");
            return stringBuilder.ToString();
        }

        private double PricePerMillion
        {
            get
            {
                var totalXp = TotalXp(_auction.Level);
                var totalMillionsXp = Math.Round(totalXp / 1000000);
                var result = (double)(_auction.CurrentBid / totalMillionsXp);
                return double.Round(result, 2);
            }
        }

        private static double TotalXp(int level)
        {
            return Math.Ceiling(50 * Math.Pow(level, 3) / 3 - 100 * Math.Pow(level, 2) + 850 * level / 3 - 200);

        }

    }
}