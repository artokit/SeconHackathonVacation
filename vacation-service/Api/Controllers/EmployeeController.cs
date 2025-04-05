using Api.Controllers.Abstractions;
using Api.Dto.Employees.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IEmployeeService = Api.Services.Interfaces.IEmployeeService;

namespace Api.Controllers;


[Route("vacation-service/employees/")]
[Authorize(Roles = "Director")]
public class EmployeeController : BaseController
{
    private IEmployeeService _employeeService;
    
    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee(CreateEmployeeRequestDto request)
    {
        return Ok(await _employeeService.CreateAsync(UserId, request));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteEmployee(Guid employeeId)
    {
        await _employeeService.DeleteAsync(UserId, employeeId);
        return Ok();
    }

    [HttpPatch("{employeeId}")]
    public async Task<IActionResult> UpdateEmployee(Guid employeeId, [FromBody] UpdateEmployeeRequestDto updateRequestDto)
    {
        return Ok(await _employeeService.UpdateAsync(UserId, employeeId, updateRequestDto));
    }
}