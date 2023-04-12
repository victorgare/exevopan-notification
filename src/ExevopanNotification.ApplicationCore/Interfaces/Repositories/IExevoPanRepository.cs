using ExevopanNotification.Domain.Entities;

namespace ExevopanNotification.ApplicationCore.Interfaces.Repositories
{
    public interface IExevoPanRepository
    {
        Task<AuctionResponse> GetAuctions(AuctionFilter auctionFilter);
    }
}
