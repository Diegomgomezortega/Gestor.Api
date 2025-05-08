using Gestor.API.Definitions;
using Gestor.API.DTOs.Response.Pais;
using System.Text.Json;

namespace Gestor.API.Endpoints
{
    public class CountriesEndpoints : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            // Agrupar los endpoints
            var productosGroup = app.MapGroup("/api/paises").WithTags("Paises");

            productosGroup.MapGet("/", async (IHttpClientFactory httpClientFactory) =>
            {
                var client = httpClientFactory.CreateClient();
                var response = await client.GetAsync("https://restcountries.com/v3.1/all");

                if (!response.IsSuccessStatusCode)
                    return Results.StatusCode((int)response.StatusCode);

                var json = await response.Content.ReadAsStringAsync();

                var rawCountries = JsonSerializer.Deserialize<List<CountryRaw>>(json);

                var paises = rawCountries?
                    .Where(c => c.name?.common is not null)
                    .Select(c => c.name.common)
                    .ToList();

                return Results.Ok(paises);
            });
        }
    }
}
