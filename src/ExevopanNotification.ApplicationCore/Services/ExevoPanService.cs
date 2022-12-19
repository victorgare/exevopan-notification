using ExevopanNotification.ApplicationCore.Interfaces.Repositories;
using ExevopanNotification.ApplicationCore.Interfaces.Services;
using ExevopanNotification.Domain.Config;
using ExevopanNotification.Domain.Entities;
using ExevopanNotification.Domain.Enums;
using Microsoft.Extensions.Options;

namespace ExevopanNotification.ApplicationCore.Services
{
    public class ExevoPanService : IExevoPanService
    {
        private readonly IExevoPanRepository _exevoPanRepository;
        private readonly INotifyService _notifyService;
        private readonly QueryConfig _queryConfig;

        public ExevoPanService(IExevoPanRepository exevoPanRepository, INotifyService notifyService, IOptions<ApplicationConfig> appConfig)
        {
            _exevoPanRepository = exevoPanRepository;
            _notifyService = notifyService;
            _queryConfig = appConfig.Value.QueryConfig;
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

            var auctions = await _exevoPanRepository.GetCurrentAuctions(auctionFilter);

            // get all auctions that lefts `minutesToGo` minutes
            // and price is less than `maximumBid`
            var auctionsFinishingSoon = auctions.Auctions.Where(c => (c.AuctionEndDateTime - DateTime.Now).TotalMinutes <= _queryConfig.MinutesToGo &&
                                                                     c.CurrentBid <= _queryConfig.MaximumBid).ToList();

            await _notifyService.NotifyAuctions(auctionsFinishingSoon);
        }

    }
}
