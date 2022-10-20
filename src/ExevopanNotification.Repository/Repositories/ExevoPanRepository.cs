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

        public async Task<AuctionResponse> GetCurrentAuctions(AuctionFilter auctionFilter)
        {
            var response = await _httpClient.PostAsJsonAsync("", auctionFilter.ToJson());

            return await response.Content.ReadAsAsync<AuctionResponse>();
        }
    }
}
