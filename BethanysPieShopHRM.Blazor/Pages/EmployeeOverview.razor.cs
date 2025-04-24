using BethanysPieShopHRM.Blazor.Services;
using BethanysPieShopHRM.Shared.Domain;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.Blazor.Pages;

public partial class EmployeeOverview
{
    [Inject]
    public IEmployeeDataService EmployeeDataService { get; set; } = null!;

    public List<Employee>? Employees { get; set; } = default!;

    private Employee? _selectedEmployee;

    private string Title = "Employee Overview";

    protected override async Task OnInitializedAsync()
    {
        Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
    }

    private void ShowQuickViewPopup(Employee selectedEmployee)
    {
        _selectedEmployee = selectedEmployee;
    }
}