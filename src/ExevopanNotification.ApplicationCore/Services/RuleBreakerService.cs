using ExevopanNotification.ApplicationCore.Interfaces.Repositories;
using ExevopanNotification.ApplicationCore.Interfaces.Services;
using ExevopanNotification.Domain.Constants;
using ExevopanNotification.Domain.Entities;
using ExevopanNotification.Domain.Notifications;

namespace ExevopanNotification.ApplicationCore.Services
{
    public class RuleBreakerService : IRuleBreakerService
    {
        private readonly IExevoPanRepository _exevoPanRepository;
        private readonly INotifyService _notifyService;

        public RuleBreakerService(IExevoPanRepository exevoPanRepository, INotifyService notifyService)
        {
            _exevoPanRepository = exevoPanRepository;
            _notifyService = notifyService;
        }

        public async Task FindAndNotify()
        {
            var auctionFilter = new AuctionFilter
            {
                MinLevel = 45,
                PageSize = 100,
                ServerSet = [ServersConstant.Obscubra, ServersConstant.Jacabra]
            };

            var auctions = await _exevoPanRepository.GetAuctions(auctionFilter);

            var notify = auctions.Auctions
                .Select(c => new AuctionNotification
                {
                    Auction = c
                })
                .ToList();

            await _notifyService.NotifyRuleBreaker(notify);
        }
    }
}
