using ExevopanNotification.Domain.Entities;
using System.ComponentModel;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace ExevopanNotification.Domain.Notifications
{
    public class TelegramAuctionNotification
    {
        private readonly Auction _auction;

        public TelegramAuctionNotification(Auction auction)
        {
            _auction = auction;
        }

        private string VocationIcon
        {
            get
            {
                return _auction.VocationId switch
                {
                    Enums.VocationEnum.None => "🌱",
                    Enums.VocationEnum.Knight => "🛡",
                    Enums.VocationEnum.Paladin => "🏹",
                    Enums.VocationEnum.Sorcerer => "🧙‍",
                    Enums.VocationEnum.Druid => "🌀",
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
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($@"{VocationIcon}{_auction.VocationId} [{_auction.Level}] - {_auction.Nickname}");
            stringBuilder.AppendLine($@"🌎 {_auction.ServerData.ServerName} - 💰 {_auction.CurrentBid}");
            stringBuilder.AppendLine($@"🕛 {_auction.AuctionEndDateTime}");
            return stringBuilder.ToString();
        }
    }
}
