using Api.Dto.Companies.Request;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("vacation-service/company")]
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
        var company = await _companyService.GetCompanyByIdAsync(id);

        if (company == null)
            return NotFound(new { Message = "Компания не найдена" });

        return Ok(company);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCompanyRequestDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _companyService.CreateCompanyAsync(dto);

        return Ok(new { Message = "Компания успешно создана" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCompanyRequestDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _companyService.UpdateCompanyAsync(id, dto);

        if (updated == null)
            return NotFound(new { Message = "Компания не найдена" });

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _companyService.DeleteCompanyAsync(id);

        if (!deleted)
            return NotFound(new { Message = "Компания не найдена или не удалось удалить" });

        return Ok(new { Message = "Компания успешно удалена" });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companies = await _companyService.GetAllCompaniesAsync();
        return Ok(companies);
    }
}
