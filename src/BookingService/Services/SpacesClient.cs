using System.Net.Http.Json;
using BookingService.Dto;

namespace BookingService.Services
{
    public class SpacesClient
    {
        private readonly HttpClient _httpClient;

        public SpacesClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://pixel-spaces:8081");
        }

        public async Task<SpaceDto?> GetSpaceAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Spaces/{id}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"⚠️ No se pudo obtener el espacio {id}. Código: {response.StatusCode}");
                return null;
            }

            return await response.Content.ReadFromJsonAsync<SpaceDto>();
        }
    }
}




