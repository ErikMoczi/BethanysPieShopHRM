using System.Text.Json;
using BethanysPieShopHRM.Shared.Domain;

namespace BethanysPieShopHRM.Blazor.Services;

public interface IJobCategoryDataService
{
    Task<IEnumerable<JobCategory>> GetAllJobCategories();
    Task<JobCategory> GetJobCategoryById(int countryId);
}

public class JobCategoryDataService : IJobCategoryDataService
{
    private readonly HttpClient _httpClient;

    public JobCategoryDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<JobCategory>> GetAllJobCategories()
    {
        var countriesResponse = await _httpClient.GetStreamAsync("api/jobcategory");

        return await JsonSerializer.DeserializeAsync<IEnumerable<JobCategory>>(countriesResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }

    public async Task<JobCategory> GetJobCategoryById(int countryId)
    {
        var countryResponse = await _httpClient.GetStreamAsync($"api/jobcategory/{countryId}");

        return await JsonSerializer.DeserializeAsync<JobCategory>(countryResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }
}