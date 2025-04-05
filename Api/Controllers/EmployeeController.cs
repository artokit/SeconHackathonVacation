using Api.Controllers.Abstractions;
using Contracts.Employees.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Interfaces;

namespace Api.Controllers;


[Route("employees/")]
[Authorize(Roles = "Director")]
public class EmployeeController : BaseController
{
    private IEmployeesService _employeesService;
    
    public EmployeeController(IEmployeesService employeesService)
    {
        _employeesService = employeesService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee(CreateEmployeeRequestDto request)
    {
        return Ok(await _employeesService.CreateAsync(UserId, request));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteEmployee(Guid employeeId)
    {
        await _employeesService.DeleteAsync(UserId, employeeId);
        return Ok();
    }

    [HttpPatch("{employeeId}")]
    public async Task<IActionResult> UpdateEmployee(Guid employeeId, [FromBody] UpdateEmployeeRequestDto updateRequestDto)
    {
        return Ok(await _employeesService.UpdateAsync(UserId, employeeId, updateRequestDto));
    }
}