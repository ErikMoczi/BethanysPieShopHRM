using System.Text.Json;
using BethanysPieShopHRM.Blazor.Helper;
using BethanysPieShopHRM.Shared.Domain;
using Blazored.LocalStorage;

namespace BethanysPieShopHRM.Blazor.Services;

public interface IEmployeeDataService
{
    Task<IEnumerable<Employee>> GetAllEmployees(bool refreshRequired = false);
    Task<Employee> GetEmployeeDetails(int employeeId);
    Task<Employee> AddEmployee(Employee employee);
    Task UpdateEmployee(Employee employee);
    Task DeleteEmployee(int employeeId);
}

public class EmployeeDataService : IEmployeeDataService
{
    private readonly HttpClient _httpClient;

    private readonly ILocalStorageService _localStorageService;

    public EmployeeDataService(HttpClient httpClient, ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
    }

    public Task<Employee> AddEmployee(Employee employee)
    {
        throw new NotImplementedException();
    }

    public Task DeleteEmployee(int employeeId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Employee>> GetAllEmployees(bool refreshRequired = false)
    {
        if (!refreshRequired)
        {
            var employeeExpirationExists = await _localStorageService.ContainKeyAsync(LocalStorageConstants.EmployeesListExpirationKey);

            if (employeeExpirationExists)
            {
                var employeeListExpiration = await _localStorageService.GetItemAsync<DateTime>(LocalStorageConstants.EmployeesListExpirationKey);

                if (employeeListExpiration > DateTime.Now)
                {
                    if (await _localStorageService.ContainKeyAsync(LocalStorageConstants.EmployeesListKey))
                    {
                        return await _localStorageService.GetItemAsync<List<Employee>>(LocalStorageConstants.EmployeesListKey);
                    }
                }
            }
        }

        var response = await _httpClient.GetStreamAsync("api/employee");
        var employees = await JsonSerializer.DeserializeAsync<IEnumerable<Employee>>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        await _localStorageService.SetItemAsync(LocalStorageConstants.EmployeesListKey, employees);
        await _localStorageService.SetItemAsync(LocalStorageConstants.EmployeesListExpirationKey, DateTime.Now.AddMinutes(1));

        return employees;
    }

    public async Task<Employee> GetEmployeeDetails(int employeeId)
    {
        var response = await _httpClient.GetStreamAsync($"api/employee/{employeeId}");
        return await JsonSerializer.DeserializeAsync<Employee>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public Task UpdateEmployee(Employee employee)
    {
        throw new NotImplementedException();
    }
}