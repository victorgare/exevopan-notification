using ExevopanNotification.ApplicationCore.Interfaces.Repositories;
using ExevopanNotification.ApplicationCore.Interfaces.Services;
using ExevopanNotification.Domain.Entities;
using ExevopanNotification.Domain.Enums;
using MathNet.Numerics.Statistics;

namespace ExevopanNotification.ApplicationCore.Services
{
    public class PriceTrendService : IPriceTrendService
    {
        const double LevelRangePercentage = 0.05;
        const int PageSize = 100;
        private readonly IExevoPanRepository _exevoPanRepository;

        public PriceTrendService(IExevoPanRepository exevoPanRepository)
        {
            _exevoPanRepository = exevoPanRepository;
        }

        public async Task<int> Analyze(Auction auction)
        {
            var minLevel = GetMinLevel(auction.Level);
            var maxLevel = GetMaxLevel(auction.Level);

            var filter = GetPriceTrendFilter(minLevel, maxLevel, auction);

            return await Analyze(filter);
        }

        public async Task<int> Analyze(Auction auction, FilterLimits filterLimits)
        {
            var minLevel = GetMinLevel(auction.Level, filterLimits.MinLevel);
            var maxLevel = GetMaxLevel(auction.Level, filterLimits.MaxLevel);

            var filter = GetPriceTrendFilter(minLevel, maxLevel, auction);

            return await Analyze(filter);
        }
        public async Task<int> Analyze(string characterName, bool history, FilterLimits filterLimits)
        {
            var auction = (await _exevoPanRepository.GetAuctions(new AuctionFilter
            {
                NicknameFilter = characterName,
                History = history,
                Descending = history
            })).Auctions.FirstOrDefault();

            if (auction == null)
            {
                return 0;
            }

            var minLevel = GetMinLevel(auction.Level, filterLimits.MinLevel);
            var maxLevel = GetMaxLevel(auction.Level, filterLimits.MaxLevel);

            var filter = GetPriceTrendFilter(minLevel, maxLevel, auction);

            return await Analyze(filter);
        }

        private async Task<int> Analyze(AuctionFilter filter)
        {
            var auctionsHistory = (await _exevoPanRepository.GetAuctions(filter)).Auctions.OrderBy(c => c.CurrentBid).ToList();

            var bidsSource = auctionsHistory.Select(c => Convert.ToDouble(c.CurrentBid)).ToArray();

            if (bidsSource.Length == 0)
            {
                return 0;
            }

            var quantile = bidsSource.Quantile(0.2);
            return Convert.ToInt32(quantile);
        }

        static int GetMinLevel(int level, int? minLevelLimit = null)
        {
            var minLevel = Convert.ToInt32(level - (level * LevelRangePercentage));

            if (minLevelLimit != null && minLevel < minLevelLimit)
            {
                return minLevelLimit.Value;
            }

            return minLevel;
        }

        static int GetMaxLevel(int level, int? maxLevelLimit = null)
        {
            var maxLevel = Convert.ToInt32(level + (level * LevelRangePercentage));

            if (maxLevelLimit != null && maxLevel > maxLevelLimit)
            {
                return maxLevelLimit.Value;
            }

            return maxLevel;
        }

        static AuctionFilter GetPriceTrendFilter(int minLevel, int maxLevel, Auction auction)
        {
            return new AuctionFilter
            {
                // only history and bidded
                History = true,
                BiddedOnly = true,
                Descending = true,

                // only with transfer
                TransferAvailable = true,

                // min and max level filter
                MinLevel = minLevel,
                MaxLevel = maxLevel,

                // vocation
                Vocation = new List<VocationEnum> { auction.VocationId },

                // same battleye color (green, yellow) 
                Battleye = new List<bool> { auction.ServerData.Battleye },

                // same pvp type
                Pvp = new List<PvpEnum> { auction.ServerData.PvpType.Type },

                // get the last auctions
                PageSize = PageSize,
            };
        }

    }
}
