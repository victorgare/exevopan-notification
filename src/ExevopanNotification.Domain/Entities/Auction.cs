using ExevopanNotification.Domain.Enums;
using ExevopanNotification.Utils.Utils;

namespace ExevopanNotification.Domain.Entities
{
    public class Auction
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public double AuctionEnd { get; set; }
        public DateTime AuctionEndDateTime { get => AuctionEnd.UnixTimeStampToDateTime(); }
        public int CurrentBid { get; set; }
        public bool HasBeenBidded { get; set; }
        public string OutfitId { get; set; }
        public int ServerId { get; set; }
        public VocationEnum VocationId { get; set; }
        public bool Sex { get; set; }
        public int Level { get; set; }
        public int AchievementPoints { get; set; }
        public int BossPoints { get; set; }
        public int TcInvested { get; set; }
        public List<string> Tags { get; set; }
        public Skills Skills { get; set; }
        public List<double> Items { get; set; }
        public List<string> Charms { get; set; }
        public bool Transfer { get; set; }
        public List<string> Imbuements { get; set; }
        public List<string> Quests { get; set; }
        public List<StoreItem> StoreItems { get; set; }
        public List<Outfit> Outfits { get; set; }
        public List<StoreOutfit> StoreOutfits { get; set; }
        public List<string> Mounts { get; set; }
        public List<object> StoreMounts { get; set; }
        public List<object> RareAchievements { get; set; }
        public Hirelings Hirelings { get; set; }
        public bool HuntingSlot { get; set; }
        public bool PreySlot { get; set; }
        public CharmInfo CharmInfo { get; set; }
        public ServerData ServerData { get; set; }
    }
}
