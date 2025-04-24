using ExevopanNotification.ApplicationCore.Interfaces.Repositories;
using ExevopanNotification.ApplicationCore.Interfaces.Services;
using ExevopanNotification.Domain.Constants;
using ExevopanNotification.Domain.Entities;
using ExevopanNotification.Domain.Notifications;

namespace ExevopanNotification.ApplicationCore.Services
{
    public class HardcoreNotifyService : IHardcoreNotifyService
    {
        private readonly IExevoPanRepository _exevoPanRepository;
        private readonly INotifyService _notifyService;
        private readonly IPriceTrendService _priceTrendService;

        public HardcoreNotifyService(IExevoPanRepository exevoPanRepository, INotifyService notifyService, IPriceTrendService priceTrendService)
        {
            _exevoPanRepository = exevoPanRepository;
            _notifyService = notifyService;
            _priceTrendService = priceTrendService;
        }

        public async Task FindAndNotify()
        {
            var auctionFilter = new AuctionFilter
            {
                ServerSet = [ServersConstant.Obscubra, ServersConstant.Jacabra],
                MinLevel = 300,
                PageSize = 100
            };

            var auctions = await _exevoPanRepository.GetAuctions(auctionFilter);

            var filterLimits = FilterLimits.Create(auctionFilter.MinLevel, auctionFilter.MaxLevel);
            var auctionNotifications = await PriceTrend(auctions.Auctions, filterLimits);

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
    }
}
