using DataAccess.Models;

namespace DataAccess.Common.Interfaces.Repositories;

public interface IDepartmentsRepository
{
    public Task<DbDepartment?> GetDepartmentByIdAsync(Guid id);
    public Task<List<DbDepartment>> GetAllDepartments();
    public Task<DbDepartment> CreateDepartmentAsync(DbDepartment department);
    public Task<DbDepartment> UpdateDepartmentAsync(Guid id, DbDepartment department);
    public Task DeleteDepartmentAsync(Guid id);
}