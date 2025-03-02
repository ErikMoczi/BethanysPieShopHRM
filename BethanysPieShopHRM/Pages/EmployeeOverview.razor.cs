using BethanysPieShopHRM.Models;
using BethanysPieShopHRM.Shared.Domain;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.Pages;

public partial class EmployeeOverview : ComponentBase
{
    public List<Employee>? Employees { get; set; } = default!;

    protected override void OnInitialized()
    {
        Employees = MockDataService.Employees;
    }
}