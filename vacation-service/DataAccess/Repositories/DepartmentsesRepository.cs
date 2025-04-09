using DataAccess.Common.Interfaces.Dapper;
using DataAccess.Common.Interfaces.Repositories;
using DataAccess.Dapper;
using DataAccess.Models;

namespace DataAccess.Repositories;

public class DepartmentsesRepository : IDepartmentsRepository
{
    private IDapperContext _dapperContext;

    public DepartmentsesRepository(IDapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    } 
        
    public async Task<DbDepartment?> GetDepartmentByIdAsync(Guid id)
    {
        var queryObject = new QueryObject(
            @"SELECT id as ""Id"", name as ""Name"", description as ""Description"", supervisor_id as ""SupervisorId"", company_id as ""CompanyId"" FROM departments
                WHERE id = @id",
            new { id });
        return await _dapperContext.FirstOrDefault<DbDepartment>(queryObject);
    }

    public async Task<List<DbDepartment>> GetAllDepartments()
    {
        var queryObject = new QueryObject(
            @"SELECT id as ""Id"", name as ""Name"", description as ""Description"", supervisor_id as ""SupervisorId"" FROM departments");
        return await _dapperContext.ListOrEmpty<DbDepartment>(queryObject);
    }


    public async Task<DbDepartment> CreateDepartmentAsync(DbDepartment department)
    {
        var queryObject = new QueryObject(
            @"INSERT INTO departments (name, description, supervisor_id, company_id) VALUES (@Name, @Description, @SupervisorId, @CompanyId)
                RETURNING id as ""Id"", name as ""Name"", description as ""Description"", supervisor_id as ""SupervisorId""",
            new
            {
                department.Id,
                department.Name,
                department.Description,
                department.SupervisorId,
                department.CompanyId
            });
        return await _dapperContext.CommandWithResponse<DbDepartment>(queryObject);
    }
    
    public async Task<DbDepartment> UpdateDepartmentAsync(Guid departmentId, DbDepartment department)
    {
        var queryObject = new QueryObject(
            @"UPDATE departments SET name = @name, description = @description, supervisor_id = @supervisor_Id
                WHERE id=@id
                RETURNING id as ""Id"", name as ""Name"", description as ""Description"", supervisor_id as ""SupervisorId""",
            new
            {
                id = departmentId, name = department.Name, description = department.Description,
                supervisor_id = department.SupervisorId
            });
        return await _dapperContext.CommandWithResponse<DbDepartment>(queryObject);
    }

    public async Task DeleteDepartmentAsync(Guid id)
    {
        var queryObject = new QueryObject(
            @"DELETE FROM departments WHERE id = @id",
            new { id });
        await _dapperContext.Command(queryObject);
    }
}