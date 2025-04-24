using ExevopanNotification.Domain.Entities;
using ExevopanNotification.Domain.Notifications.Base;
using System.Globalization;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace ExevopanNotification.Domain.Notifications
{
    public class TelegramAuctionNotification : AuctionBaseNotification
    {
        private readonly int _priceTrend;

        public TelegramAuctionNotification(Auction auction, int priceTrend) : base(auction)
        {
            _priceTrend = priceTrend;
        }

        public InlineKeyboardMarkup GetInlineLinkButton()
        {
            // auction link
            var auctionLinkButton = InlineKeyboardButton.WithUrl(_auction.Nickname, AuctionLink);

            // Keyboard markup
            return new InlineKeyboardMarkup(auctionLinkButton);
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

        public override string ToString()
        {
            var ptCulture = new CultureInfo("pt-BR");
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($@"{VocationIcon}{_auction.VocationId} [{_auction.Level}] - {_auction.Nickname}");
            stringBuilder.AppendLine($@"🌎 {_auction.ServerData.ServerName} - 💰 {_auction.CurrentBid.ToString("N0", ptCulture)} ({PricePerMillion.ToString(new CultureInfo("en-US"))}tc/kk)");
            stringBuilder.AppendLine($@"🕛 {_auction.AuctionEndDateTime}");
            stringBuilder.AppendLine($@"📈 {_priceTrend.ToString("N0", ptCulture)}");
            return stringBuilder.ToString();
        }
    }
}
