using BethanysPieShopHRM.Models;
using BethanysPieShopHRM.Shared.Domain;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.Pages;

public partial class EmployeeOverview : ComponentBase
{
    public List<Employee>? Employees { get; set; } = default!;

    private Employee? _selectedEmployee;

    private string Title = "Employee Overview";

    protected override void OnInitialized()
    {
        Employees = MockDataService.Employees;
    }

    private void ShowQuickViewPopup(Employee selectedEmployee)
    {
        _selectedEmployee = selectedEmployee;
    }
}