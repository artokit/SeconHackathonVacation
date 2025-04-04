using DataAccess.Common.Interfaces.Repositories;
using DataAccess.Dapper;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.Repositories;

public class DepartmentsRepository : IDepartmentsRepository
{
    private IDapperContext _dapperContext;

    public DepartmentsRepository(IDapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    } 
        
    public async Task<DbDepartment?> GetDepartmentsByIdAsync(int id)
    {
        var queryObject = new QueryObject(
            @"SELECT id as ""Id"", name as ""Name"", description as ""Description"", supervisor_id as ""SupervisorId"" FROM department
                WHERE id == @id",
            new { id });
        return await _dapperContext.FirstOrDefault<DbDepartment>(queryObject);
    }

    public async Task<DbDepartment> CreateDepartmentAsync(DbDepartment department)
    {
        var queryObject = new QueryObject(
            @"INSERT INTO departments (name, description, supervisor_id) VALUES (@Name, @Description, @SupervisorId)
                RETURNING id as ""Id"", name as ""Name"", description as ""Description"", supervisor_id as ""SupervisorId""",
            new
            {
                department.Id,
                department.Name,
                department.Description,
                department.SupervisorId
            });
        return await _dapperContext.CommandWithResponse<DbDepartment>(queryObject);
    }
    
    public async Task<DbDepartment> UpdateDepartmentAsync(DbDepartment department)
    {
        var queryObject = new QueryObject(
            @"UPDATE departments SET name = @name, description = @description, supervisor_id = @supervisorId
                WHERE id=@id
                RETURNING id as ""Id"", name as ""Name"", description as ""Description"", supervisor_id as ""SupervisorId""",
            new
            {
                id = department.Id, name = department.Name, description = department.Description,
                supervisorId = department.SupervisorId
            });
        return await _dapperContext.CommandWithResponse<DbDepartment>(queryObject);
    }

    public async Task DeleteDepartmentAsync(int id)
    {
        var queryObject = new QueryObject(
            @"DELETE FROM departments WHERE id = @id",
            new { id = id });
        await _dapperContext.Command(queryObject);
    }
}