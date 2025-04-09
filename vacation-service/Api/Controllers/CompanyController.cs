using Api.Controllers.Abstractions;
using Api.Dto.Companies.Request;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("vacation-service/companies")]
public class CompanyController : BaseController
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCompanyRequestDto dto)
    {
        return Ok(await _companyService.CreateCompanyAsync(UserId, dto));
    }

    [HttpGet]
    public async Task<IActionResult> GetById()
    {
        return Ok(await _companyService.GetCompanyByUserIdAsync(UserId));
    }

    [HttpPatch("{companyId}")]
    public async Task<IActionResult> Update(Guid companyId, UpdateCompanyRequestDto dto)
    {
        return Ok(await _companyService.UpdateCompanyAsync(UserId, companyId, dto));
    }
}
