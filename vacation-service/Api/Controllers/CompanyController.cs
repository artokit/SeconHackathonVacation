using Api.Services.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;   

    public CompanyController(ICompanyService companyService) 
    {
        _companyService = companyService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var company = _companyService.GetCompanyByIdAsync(id);

        if (company == null)
            return NotFound(new { Message = "Company not found" });

        return Ok(company);
    }

    [HttpPost]
    public async Task<IActionResult> Create(DbCompany company)
    {
        if (company == null || string.IsNullOrEmpty(company.Name))
            return BadRequest(new { Message = "Invalid company data" });

        var createdCompany = await _companyService.CreateCompanyAsync(company);

        if (createdCompany)
            return Ok(new { Message = "Company created successfully"});

        return StatusCode(500, new { Message = "Failed to create company" });
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, DbCompany updatedCompany)
    {
        var company = _companyService.GetCompanyByIdAsync(id);

        if (company == null)
            return NotFound(new { Message = "Company not found" });

        if (updatedCompany == null || string.IsNullOrEmpty(updatedCompany.Name))
            return BadRequest(new { Message = "Invalid company data" });

        _companyService.UpdateCompanyAsync(id, updatedCompany);

        return Ok(); 
        // Пересмотреть код
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var company = await _companyService.GetCompanyByIdAsync(id);

        if (company == null)
            return NotFound(new { Message = "Company not found" });

        bool successfulDelete = await _companyService.DeleteCompanyAsync(id);

        if (successfulDelete)
            return Ok(new { Message = "Company deleted successfully" });

        return StatusCode(500, new { Message = "Failed to delete company" });
    }
}