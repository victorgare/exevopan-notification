using ExevopanNotification.Domain.Entities;
using ExevopanNotification.Domain.Enums;
using System.ComponentModel;

namespace ExevopanNotification.Domain.Notifications.Base
{
    public class AuctionBaseNotification
    {
        internal readonly Auction _auction;

        public AuctionBaseNotification(Auction auction)
        {
            _auction = auction;
        }

        internal string VocationIcon
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

        internal string AuctionLink
        {
            get
            {
                return $@"https://www.tibia.com/charactertrade/?subtopic=currentcharactertrades&page=details&auctionid={_auction.Id}";
            }
        }
    }
}
