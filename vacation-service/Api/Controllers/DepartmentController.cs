using Api.Controllers.Abstractions;
using Api.Dto.Departments.Requests;
using Api.Mappers;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


[Route("vacation-service/departments")]
public class DepartmentController : BaseController
{
    private IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateDepartmentRequestDto departmentRequestDto)
    {
        return Ok(await _departmentService.CreateAsync(departmentRequestDto));
    }

    [HttpGet("{departmentId}")]
    public async Task<IActionResult> GetById(Guid departmentId)
    {
        return Ok(await _departmentService.GetByIdAsync(departmentId));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _departmentService.GetAllAsync());
    }

    [HttpPatch("{departmentId}")]
    public async Task<IActionResult> Update(Guid departmentId, UpdateDepartmentRequestDto departmentRequestDto)
    {
        return Ok(await _departmentService.UpdateAsync(departmentId, departmentRequestDto));
    }

    [HttpDelete("{departmentId}")]
    public async Task<IActionResult> Delete(Guid departmentId)
    {
         await _departmentService.DeleteAsync(departmentId);
         return NoContent();
    }
}