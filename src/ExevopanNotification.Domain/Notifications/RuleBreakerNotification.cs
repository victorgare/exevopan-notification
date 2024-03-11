using ExevopanNotification.Domain.Entities;
using ExevopanNotification.Domain.Notifications.Base;
using System.Globalization;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace ExevopanNotification.Domain.Notifications
{
    public class RuleBreakerNotification : AuctionBaseNotification
    {

        public RuleBreakerNotification(Auction auction) : base(auction)
        {
        }
        public InlineKeyboardMarkup GetInlineLinkButton()
        {
            // auction link
            var auctionLinkButton = InlineKeyboardButton.WithUrl(_auction.Nickname, AuctionLink);

            // Keyboard markup
            return new InlineKeyboardMarkup(auctionLinkButton);
        }

        public override string ToString()
        {
            var ptCulture = new CultureInfo("pt-BR");
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($@"{VocationIcon}{_auction.VocationId} [{_auction.Level}] - {_auction.Nickname}");
            stringBuilder.AppendLine($@"🌎 {_auction.ServerData.ServerName} - 💰 {_auction.CurrentBid.ToString("N0", ptCulture)}");
            stringBuilder.AppendLine($@"🕛 {_auction.AuctionEndDateTime}");
            return stringBuilder.ToString();
        }
    }
}
