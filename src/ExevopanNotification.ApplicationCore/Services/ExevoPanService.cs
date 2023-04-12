using ExevopanNotification.ApplicationCore.Interfaces.Repositories;
using ExevopanNotification.ApplicationCore.Interfaces.Services;
using ExevopanNotification.Domain.Config;
using ExevopanNotification.Domain.Entities;
using ExevopanNotification.Domain.Enums;
using ExevopanNotification.Domain.Notifications;
using Microsoft.Extensions.Options;

namespace ExevopanNotification.ApplicationCore.Services
{
    public class ExevoPanService : IExevoPanService
    {
        private readonly IExevoPanRepository _exevoPanRepository;
        private readonly INotifyService _notifyService;
        private readonly IPriceTrendService _priceTrendService;
        private readonly QueryConfig _queryConfig;

        public ExevoPanService(IExevoPanRepository exevoPanRepository, INotifyService notifyService, IOptions<ApplicationConfig> appConfig, IPriceTrendService priceTrendService)
        {
            _exevoPanRepository = exevoPanRepository;
            _notifyService = notifyService;
            _queryConfig = appConfig.Value.QueryConfig;
            _priceTrendService = priceTrendService;
        }

        public async Task FindAndNotify()
        {
            var auctionFilter = new AuctionFilter
            {
                Vocation = new List<VocationEnum> { VocationEnum.Sorcerer, VocationEnum.Druid },
                Pvp = new List<PvpEnum> { PvpEnum.Open, PvpEnum.RetroOpen, PvpEnum.RetroHardcore },
                MinLevel = 300,
                MaxLevel = 500,
                TransferAvailable = true,
                PageSize = 20
            };

            var auctions = await _exevoPanRepository.GetAuctions(auctionFilter);

            // get all auctions that lefts `minutesToGo` minutes
            // and price is less than `maximumBid`
            var auctionsFinishingSoon = auctions.Auctions.Where(c => (c.AuctionEndDateTime - DateTime.Now).TotalMinutes <= _queryConfig.MinutesToGo &&
                                                                     c.CurrentBid <= _queryConfig.MaximumBid).ToList();

            var filterLimits = GetFilterLimit(auctionFilter.MinLevel, auctionFilter.MaxLevel);
            var auctionNotifications = await PriceTrend(auctionsFinishingSoon, filterLimits);

            await _notifyService.NotifyAuctions(auctionNotifications);
        }

        private async Task<List<AuctionNotification>> PriceTrend(List<Auction> auctions, FilterLimits filterLimits)
        {
            var returnList = new List<AuctionNotification>();

            foreach (var auction in auctions)
            {
                var priceTrend = await _priceTrendService.Analyze(auction, filterLimits);

                if (auction.CurrentBid < priceTrend)
                {
                    returnList.Add(new AuctionNotification
                    {
                        Auction = auction,
                        PriceTrend = priceTrend,
                    });
                }
            }

            return returnList;
        }

        static FilterLimits GetFilterLimit(int minLevel, int maxLevel)
        {
            return new FilterLimits(minLevel, maxLevel);
        }
    }
}
