using ExevopanNotification.Domain.Entities;

namespace ExevopanNotification.ApplicationCore.Interfaces.Services
{
    public interface IAuctionNotification
    {
        Task Notify(List<Auction> auctions);
        Task Notify(string message);
    }
}
