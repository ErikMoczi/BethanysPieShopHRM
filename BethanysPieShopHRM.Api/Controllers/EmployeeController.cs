﻿using BethanysPieShopHRM.Api.Models;
using BethanysPieShopHRM.Shared.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShopHRM.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class EmployeeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EmployeeController(IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
    {
        _employeeRepository = employeeRepository;
        _webHostEnvironment = webHostEnvironment;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet]
    public IActionResult GetAllEmployees()
    {
        return Ok(_employeeRepository.GetAllEmployees());
    }

    [HttpGet("{id}")]
    public IActionResult GetEmployeeById(int id)
    {
        return Ok(_employeeRepository.GetEmployeeById(id));
    }

    [HttpPost]
    public IActionResult CreateEmployee([FromBody] Employee employee)
    {
        if (employee == null)
            return BadRequest();

        if (employee.FirstName == string.Empty || employee.LastName == string.Empty)
        {
            ModelState.AddModelError("Name/FirstName", "The name or first name shouldn't be empty");
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // handle image upload
        if (employee.ImageContent != null)
        {
            var currentUrl = _httpContextAccessor.HttpContext.Request.Host.Value;
            var path = $"{_webHostEnvironment.WebRootPath}/Uploads/{employee.ImageName}";
            System.IO.File.WriteAllBytes(path, employee.ImageContent);

            employee.ImageName = $"https://{currentUrl}/Uploads/{employee.ImageName}";
        }

        var createdEmployee = _employeeRepository.AddEmployee(employee);

        return Created("employee", createdEmployee);
    }

    [HttpPut]
    public IActionResult UpdateEmployee([FromBody] Employee employee)
    {
        if (employee == null)
            return BadRequest();

        if (employee.FirstName == string.Empty || employee.LastName == string.Empty)
        {
            ModelState.AddModelError("Name/FirstName", "The name or first name shouldn't be empty");
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var employeeToUpdate = _employeeRepository.GetEmployeeById(employee.EmployeeId);

        if (employeeToUpdate == null)
            return NotFound();

        _employeeRepository.UpdateEmployee(employee);

        return NoContent(); //success
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEmployee(int id)
    {
        if (id == 0)
            return BadRequest();

        var employeeToDelete = _employeeRepository.GetEmployeeById(id);
        if (employeeToDelete == null)
            return NotFound();

        _employeeRepository.DeleteEmployee(id);

        return NoContent(); //success
    }
}