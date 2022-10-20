using System.Text.Json.Serialization;

namespace ExevopanNotification.Domain.Entities
{
    public class AuctionResponse
    {
        [JsonPropertyName("page")]
        public List<Auction> Auctions { get; set; }

        public int PageIndex { get; set; }
        public int TotalItems { get; set; }
        public int StartOffset { get; set; }
        public int EndOffset { get; set; }
        public bool HasPrev { get; set; }
        public bool HasNext { get; set; }
        public int SortingMode { get; set; }
        public bool DescendingOrder { get; set; }
    }
}
