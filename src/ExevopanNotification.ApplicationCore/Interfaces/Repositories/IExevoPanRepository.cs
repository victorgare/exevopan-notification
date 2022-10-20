using ExevopanNotification.Domain.Entities;

namespace ExevopanNotification.ApplicationCore.Interfaces.Repositories
{
    public interface IExevoPanRepository
    {
        Task<AuctionResponse> GetCurrentAuctions(AuctionFilter auctionFilter);
    }
}
