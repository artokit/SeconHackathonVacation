using Api.Controllers.Abstractions;
using Api.Dto.Departments.Requests;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Hr,Director")]
    public async Task<IActionResult> Create(CreateDepartmentRequestDto departmentRequestDto)
    {
        return Ok(await _departmentService.CreateAsync(UserId, departmentRequestDto));
    }

    [HttpGet("{departmentId}")]
    public async Task<IActionResult> GetById(Guid departmentId)
    {
        return Ok(await _departmentService.GetByIdAsync(UserId, departmentId));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _departmentService.GetAllAsync(UserId));
    }

    [HttpPatch("{departmentId}")]
    public async Task<IActionResult> Update(Guid departmentId, UpdateDepartmentRequestDto departmentRequestDto)
    {
        return Ok(await _departmentService.UpdateAsync(UserId, departmentId, departmentRequestDto));
    }

    [HttpDelete("{departmentId}")]
    public async Task<IActionResult> Delete(Guid departmentId)
    {
         await _departmentService.DeleteAsync(UserId, departmentId);
         return NoContent();
    }
}