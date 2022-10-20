using ExevopanNotification.Domain.Enums;

namespace ExevopanNotification.Domain.Entities
{
    public class FilterOptions
    {
        public List<object>? AuctionIds { get; set; }
        public string? NicknameFilter { get; set; } = string.Empty;
        public List<VocationEnum>? Vocation { get; set; }
        public List<PvpEnum>? Pvp { get; set; }
        public List<object>? Battleye { get; set; }
        public List<object>? Location { get; set; }
        public List<object>? ServerSet { get; set; }
        public List<object>? Tags { get; set; }
        public int MinLevel { get; set; } = 0;
        public int MaxLevel { get; set; } = 3000;
        public int MinSkill { get; set; } = 0;
        public int MaxSkill { get; set; } = 150;
        public int BossPoints { get; set; } = 0;
        public int TcInvested { get; set; } = 0;
        public int? Addon { get; set; }
        public bool Sex { get; set; }
        public List<object>? SkillKey { get; set; }
        public List<object>? ImbuementsSet { get; set; }
        public List<object>? CharmsSet { get; set; }
        public bool RareNick { get; set; }
        public List<object>? QuestSet { get; set; }
        public List<object>? OutfitSet { get; set; }
        public List<object>? StoreOutfitSet { get; set; }
        public List<object>? MountSet { get; set; }
        public List<object>? StoreMountSet { get; set; }
        public List<object>? AchievementSet { get; set; }
        public bool CharmExpansion { get; set; }
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
