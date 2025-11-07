using BookingService.Dto;

namespace BookingService.Services
{
    public class SpacesClient
    {
        private readonly HttpClient _httpClient;
        
        public SpacesClient (IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("SpacesService");
        }

        public async Task<SpaceDto?> GetSpaceAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<SpaceDto>($"/api/spaces/{id}");
        }
    }
}
