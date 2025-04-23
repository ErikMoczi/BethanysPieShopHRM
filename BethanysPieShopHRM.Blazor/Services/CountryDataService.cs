using System.Text.Json;
using BethanysPieShopHRM.Shared.Domain;

namespace BethanysPieShopHRM.Blazor.Services;

public interface ICountryDataService
{
    Task<IEnumerable<Country>> GetAllCountries();
    Task<Country> GetCountryById(int countryId);
}

public class CountryDataService : ICountryDataService
{
    private readonly HttpClient _httpClient;

    public CountryDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Country>> GetAllCountries()
    {
        var countriesResponse = await _httpClient.GetStreamAsync("api/country");

        return await JsonSerializer.DeserializeAsync<IEnumerable<Country>>(countriesResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }

    public async Task<Country> GetCountryById(int countryId)
    {
        var countryResponse = await _httpClient.GetStreamAsync($"api/country/{countryId}");

        return await JsonSerializer.DeserializeAsync<Country>(countryResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }
}