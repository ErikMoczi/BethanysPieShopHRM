using BethanysPieShopHRM.Blazor.Services;
using BethanysPieShopHRM.Shared.Domain;
using BethanysPieShopHRM.Shared.Model;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.Blazor.Pages;

public partial class EmployeeDetail
{
    [Inject]
    public IEmployeeDataService EmployeeDataService { get; set; } = null!;

    [Parameter]
    public string EmployeeId { get; set; }

    public Employee? Employee { get; set; } = new Employee();

    public List<Marker> MapMarkers { get; set; } = new List<Marker>();

    protected override async Task OnInitializedAsync()
    {
        Employee = await EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
        if (Employee.Longitude.HasValue && Employee.Latitude.HasValue)
        {
            MapMarkers =
            [
                new Marker
                {
                    Description = $"{Employee.FirstName} {Employee.LastName}",
                    ShowPopup = false,
                    X = Employee.Longitude.Value,
                    Y = Employee.Latitude.Value,
                }
            ];
        }
    }
}