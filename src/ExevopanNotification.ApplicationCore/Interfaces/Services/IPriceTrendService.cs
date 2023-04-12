using ExevopanNotification.Domain.Entities;

namespace ExevopanNotification.ApplicationCore.Interfaces.Services
{
    public interface IPriceTrendService
    {
        /// <summary>
        /// Analyze the lasts auctions similar to the one in the parameter
        /// return the trend price of characters with similar characteristics
        /// </summary>
        /// <param name="auction">The current  auction to analyze</param>
        /// <returns>The trend price of similar finished auctions</returns>
        Task<int> Analyze(Auction auction);

        /// <summary>
        /// Analyze the lasts auctions similar to the one in the parameter
        /// return the trend price of characters with similar characteristics
        /// </summary>
        /// <param name="auction">The current  auction to analyze</param>
        /// <param name="filterLimits">The limits to be applied to the filters in the analysis</param>
        /// <returns>The trend price of similar finished auctions</returns>
        Task<int> Analyze(Auction auction, FilterLimits filterLimits);
    }
}
