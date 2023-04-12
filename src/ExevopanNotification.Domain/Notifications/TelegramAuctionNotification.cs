using ExevopanNotification.Domain.Entities;
using ExevopanNotification.Domain.Enums;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace ExevopanNotification.Domain.Notifications
{
    public class TelegramAuctionNotification
    {
        private readonly Auction _auction;
        private readonly int _priceTrend;

        public TelegramAuctionNotification(Auction auction, int priceTrend)
        {
            _auction = auction;
            _priceTrend = priceTrend;
        }

        private string VocationIcon
        {
            get
            {
                return _auction.VocationId switch
                {
                    VocationEnum.None => "🌱",
                    VocationEnum.Knight => "🛡",
                    VocationEnum.Paladin => "🏹",
                    VocationEnum.Sorcerer => "🧙‍",
                    VocationEnum.Druid => "🌀",
                    _ => throw new InvalidEnumArgumentException("Vocation unknown"),
                };
            }
        }

        private string AuctionLink
        {
            get
            {
                return $@"https://www.tibia.com/charactertrade/?subtopic=currentcharactertrades&page=details&auctionid={_auction.Id}";
            }
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
            stringBuilder.AppendLine($@"📈 {_priceTrend.ToString("N0", ptCulture)}");
            return stringBuilder.ToString();
        }
    }
}
