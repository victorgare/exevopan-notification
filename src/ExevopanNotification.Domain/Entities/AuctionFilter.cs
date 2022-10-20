namespace ExevopanNotification.Domain.Entities
{
    public class AuctionFilter
    {
        public PaginationOptions PaginationOptions { get; set; }
        public SortOptions SortOptions { get; set; }
        public FilterOptions FilterOptions { get; set; }
    }
}
