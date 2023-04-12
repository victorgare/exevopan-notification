using ExevopanNotification.ApplicationCore.Interfaces.Repositories;
using ExevopanNotification.Domain.Entities;
using ExevopanNotification.Utils.Utils;

namespace ExevopanNotification.Repository.Repositories
{
    public class ExevoPanRepository : IExevoPanRepository
    {
        private readonly HttpClient _httpClient;

        public ExevoPanRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(ExevoPanRepository));
        }

        public async Task<AuctionResponse> GetAuctions(AuctionFilter auctionFilter)
        {
            var qs = auctionFilter.ToQueryString();
            var response = await _httpClient.GetAsync($"auctions?{qs}");

            return (await response.Content.ReadAsStringAsync()).ParseJson<AuctionResponse>()!;
        }
    }
}
