using ExevopanNotification.Domain.Entities;

namespace ExevopanNotification.ApplicationCore.Interfaces.Services
{
    public interface INotifyService
    {
        Task NotifyAuctions(List<Auction> auctions);
    }
}
