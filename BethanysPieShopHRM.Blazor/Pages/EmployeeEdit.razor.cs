using BethanysPieShopHRM.Blazor.Services;
using BethanysPieShopHRM.Shared.Domain;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BethanysPieShopHRM.Blazor.Pages;

public partial class EmployeeEdit
{
    [Inject]
    public IEmployeeDataService EmployeeDataService { get; set; } = null!;

    [Inject]
    public ICountryDataService CountryDataService { get; set; } = null!;

    [Inject]
    public IJobCategoryDataService JobCategoryDataService { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Parameter]
    public string? EmployeeId { get; set; }

    public Employee Employee { get; set; } = new Employee();

    public List<Country> Countries { get; set; } = new List<Country>();

    public List<JobCategory> JobCategories { get; set; } = new List<JobCategory>();

    private string Message = string.Empty;
    private string StatusClass = string.Empty;
    private IBrowserFile selectedFile;
    private bool Saved;

    protected override async Task OnInitializedAsync()
    {
        Saved = false;
        Countries = (await CountryDataService.GetAllCountries()).ToList();
        JobCategories = (await JobCategoryDataService.GetAllJobCategories()).ToList();

        int.TryParse(EmployeeId, out var employeeId);

        if (employeeId == 0)
        {
            // add some defaults
            Employee = new Employee
            {
                CountryId = 1,
                JobCategoryId = 1,
                BirthDate = DateTime.Now,
                JoinedDate = DateTime.Now
            };
        }
        else
        {
            Employee = await EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
        }
    }

    private async Task HandleValidSubmit()
    {
        Saved = false;

        if (Employee.EmployeeId == 0) //new
        {
            //image adding
            if (selectedFile != null)
            {
                var file = selectedFile;
                await using var stream = file.OpenReadStream();
                using MemoryStream ms = new();
                await stream.CopyToAsync(ms);

                Employee.ImageName = file.Name;
                Employee.ImageContent = ms.ToArray();
            }

            var addedEmployee = await EmployeeDataService.AddEmployee(Employee);
            if (addedEmployee != null)
            {
                StatusClass = "alert-success";
                Message = "New employee added successfully.";
                Saved = true;
            }
            else
            {
                StatusClass = "alert-danger";
                Message = "Something went wrong adding the new employee. Please try again.";
                Saved = false;
            }
        }
        else
        {
            await EmployeeDataService.UpdateEmployee(Employee);
            StatusClass = "alert-success";
            Message = "Employee updated successfully.";
            Saved = true;
        }
    }

    private async Task HandleInvalidSubmit()
    {
        StatusClass = "alert-danger";
        Message = "There are some validation errors. Please try again.";
    }

    private async Task DeleteEmployee()
    {
        await EmployeeDataService.DeleteEmployee(Employee.EmployeeId);

        StatusClass = "alert-success";
        Message = "Deleted successfully.";

        Saved = true;
    }

    private async Task NavigateToOverview()
    {
        NavigationManager.NavigateTo("/employeeoverview");
    }

    private void OnInputFileChange(InputFileChangeEventArgs e)
    {
        selectedFile = e.File;
        StateHasChanged();
    }
}