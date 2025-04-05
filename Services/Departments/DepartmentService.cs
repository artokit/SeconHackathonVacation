using Contracts.Departments.Responses;
using DataAccess.Common.Interfaces.Repositories;
using Services.Common.Interfaces;

namespace Services.Departments;

public class DepartmentService : IDepartmentService
{
    private IDepartmentsRepository _departmentsRepository;

    public DepartmentService(IDepartmentsRepository departmentsRepository)
    {
        _departmentsRepository = departmentsRepository;
    }


    public GetDepartmentResponseDto CreateAsync()
    {
        throw new NotImplementedException();
    }

    public GetDepartmentResponseDto GetByIdAsync()
    {
        throw new NotImplementedException();
    }

    public GetDepartmentResponseDto GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public GetDepartmentResponseDto UpdateAsync()
    {
        throw new NotImplementedException();
    }

    public GetDepartmentResponseDto DeleteAsync()
    {
        throw new NotImplementedException();
    }
}