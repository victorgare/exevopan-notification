using ExevopanNotification.ApplicationCore.Interfaces.Repositories;
using ExevopanNotification.ApplicationCore.Interfaces.Services;
using ExevopanNotification.Domain.Entities;
using ExevopanNotification.Domain.Enums;

namespace ExevopanNotification.ApplicationCore.Services
{
    public class ExevoPanService : IExevoPanService
    {
        private readonly IExevoPanRepository _exevoPanRepository;

        public ExevoPanService(IExevoPanRepository exevoPanRepository)
        {
            _exevoPanRepository = exevoPanRepository;
        }

        public async Task FindAndNotify()
        {
            var minutesToGo = 30;
            var maximumBid = 750;

            var auctionFilter = new AuctionFilter
            {
                PaginationOptions = new PaginationOptions
                {
                    PageIndex = 0,
                    PageSize = 20
                },
                SortOptions = new SortOptions
                {
                    SortingMode = 0, // sorted by time
                    DescendingOrder = false
                },
                FilterOptions = new FilterOptions
                {
                    Vocation = new List<VocationEnum> { VocationEnum.Sorcerer, VocationEnum.Druid },
                    Pvp = new List<PvpEnum> { PvpEnum.Open, PvpEnum.RetroOpen, PvpEnum.RetroHardcore },
                    MinLevel = 300
                }
            };

            var auctions = await _exevoPanRepository.GetCurrentAuctions(auctionFilter);

            // get all auctions that lefts `minutesToGo` minutes
            // and price is less than `maximumBid`
            var auctionsFinishingSoon = auctions.Auctions.Where(c => (c.AuctionEndDateTime - DateTime.Now).TotalMinutes <= minutesToGo &&
                                                                     c.CurrentBid <= maximumBid);
        }

    }
}
