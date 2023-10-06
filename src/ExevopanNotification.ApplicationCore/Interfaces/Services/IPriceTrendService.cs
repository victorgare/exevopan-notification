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

        /// <summary>
        /// Analyze the lasts auctions similar to character in the parameter
        /// return the trend price of characters with similar characteristics
        /// </summary>
        /// <param name="characterName">The character name to analyze</param>
        /// <param name="history">If it is to analyze the current auction or history auctions</param>
        /// /// <param name="filterLimits">The limits to be applied to the filters in the analysis</param>
        /// <returns>The trend price of similar finished auctions</returns>
        Task<int> Analyze(string characterName, bool history, FilterLimits filterLimits);
    }
}
