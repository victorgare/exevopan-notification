namespace ExevopanNotification.Domain.Entities
{
    public class FilterOptions
    {
        public List<object> AuctionIds { get; set; }
        public string NicknameFilter { get; set; }
        public List<int> Vocation { get; set; }
        public List<int> Pvp { get; set; }
        public List<object> Battleye { get; set; }
        public List<object> Location { get; set; }
        public List<object> ServerSet { get; set; }
        public List<object> Tags { get; set; }
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }
        public int MinSkill { get; set; }
        public int MaxSkill { get; set; }
        public int BossPoints { get; set; }
        public int TcInvested { get; set; }
        public int Addon { get; set; }
        public bool Sex { get; set; }
        public List<object> SkillKey { get; set; }
        public List<object> ImbuementsSet { get; set; }
        public List<object> CharmsSet { get; set; }
        public bool RareNick { get; set; }
        public List<object> QuestSet { get; set; }
        public List<object> OutfitSet { get; set; }
        public List<object> StoreOutfitSet { get; set; }
        public List<object> MountSet { get; set; }
        public List<object> StoreMountSet { get; set; }
        public List<object> AchievementSet { get; set; }
        public bool MharmExpansion { get; set; }
        public bool PreySlot { get; set; }
        public bool HuntingSlot { get; set; }
        public bool RewardShrine { get; set; }
        public bool ImbuementShrine { get; set; }
        public bool Dummy { get; set; }
        public bool Mailbox { get; set; }
        public bool GoldPouch { get; set; }
        public bool Hireling { get; set; }
        public bool TransferAvailable { get; set; }
        public bool BiddedOnly { get; set; }
    }
}
