﻿using BethanysPieShopHRM.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShopHRM.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class JobCategoryController : Controller
{
    private readonly IJobCategoryRepository _jobCategoryRepository;

    public JobCategoryController(IJobCategoryRepository jobCategoryRepository)
    {
        _jobCategoryRepository = jobCategoryRepository;
    }

    // GET: api/<controller>
    [HttpGet]
    public IActionResult GetJobCategories()
    {
        return Ok(_jobCategoryRepository.GetAllJobCategories());
    }

    // GET api/<controller>/5
    [HttpGet("{id}")]
    public IActionResult GetJobCategoryById(int id)
    {
        return Ok(_jobCategoryRepository.GetJobCategoryById(id));
    }
}