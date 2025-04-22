using BethanysPieShopHRM.Blazor.Services;
using BethanysPieShopHRM.Shared.Domain;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.Blazor.Pages;

public partial class EmployeeDetail
{
    [Inject]
    public IEmployeeDataService EmployeeDataService { get; set; } = null!;

    [Parameter]
    public string EmployeeId { get; set; }

    public Employee? Employee { get; set; } = new Employee();

    protected override async Task OnInitializedAsync()
    {
        Employee = await EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
    }
}