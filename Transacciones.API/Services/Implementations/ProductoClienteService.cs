using System.Text.Json;
using Transacciones.API.Models.DTOs;
using Transacciones.API.Services.Interfaces;

namespace Transacciones.API.Services.Implementations
{
    public class ProductoClienteService : IProductoClienteService
    {
        private readonly HttpClient _httpClient;

        public ProductoClienteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductoDetalleDto?> ObtenerProductoAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/productos/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ProductoDetalleDto>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<bool> ActualizarStockAsync(int id, int cantidad)
        {
            var response = await _httpClient.PatchAsync($"api/productos/{id}/stock?cantidad={cantidad}", null);
            return response.IsSuccessStatusCode;
        }
    }
}