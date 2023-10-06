using ExevopanNotification.Domain.Enums;

namespace ExevopanNotification.Domain.Entities
{
    public class AuctionFilter
    {
        public string? NicknameFilter { get; set; }
        public List<VocationEnum>? Vocation { get; set; }
        public List<PvpEnum>? Pvp { get; set; }
        public int MinLevel { get; set; } = 0!;
        public int MaxLevel { get; set; } = 4000!;
        public bool TransferAvailable { get; set; }
        public int PageSize { get; set; } = 10;

        public bool? BiddedOnly { get; set; }
        public bool? History { get; set; }
        public bool? Descending { get; set; }


        public List<bool>? Battleye { get; set; }
    }
}
