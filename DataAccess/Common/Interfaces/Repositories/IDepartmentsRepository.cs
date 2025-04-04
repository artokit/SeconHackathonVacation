using DataAccess.Models;

namespace DataAccess.Common.Interfaces.Repositories;

public interface IDepartmentsRepository
{
    public Task<DbDepartment?> GetDepartmentsByIdAsync(int id);
    public Task<DbDepartment> CreateDepartmentAsync(DbDepartment department);
    public Task<DbDepartment> UpdateDepartmentAsync(DbDepartment department);
    public Task DeleteDepartmentAsync(int id);
}