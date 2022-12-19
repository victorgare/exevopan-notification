using ExevopanNotification.Domain.Enums;

namespace ExevopanNotification.Domain.Entities
{
    public class AuctionFilter
    {
        public List<VocationEnum>? Vocation { get; set; }
        public List<PvpEnum>? Pvp { get; set; }
        public int MinLevel { get; set; } = 0!;
        public int MaxLevel { get; set; } = 4000!;
        public bool TransferAvailable { get; set; }
        public int PageSize { get; set; } = 10;
    }
}
