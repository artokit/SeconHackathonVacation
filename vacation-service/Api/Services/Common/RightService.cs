using Api.Exceptions.Departments;
using Api.Exceptions.Users;
using Api.Services.Interfaces;
using Common;
using DataAccess.Common.Interfaces.Repositories;
using DataAccess.Models;

namespace Api.Services.Common;

public class RightService : IRightService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IDepartmentsRepository _departmentsRepository;

        public RightService(
            IUsersRepository usersRepository,
            IDepartmentsRepository departmentsRepository)
        {
            _usersRepository = usersRepository;
            _departmentsRepository = departmentsRepository;
        }

        public async Task ValidateUserAccessAsync(Guid userId, Guid targetUserId)
        {
            var user = await _usersRepository.GetByIdAsync(userId) ?? throw new UserNotFoundRequest();
            var targetUser = await _usersRepository.GetByIdAsync(targetUserId) ?? throw new UserNotFoundRequest();

            if (user.Id != targetUserId && user.Role != Roles.Director && user.Role != Roles.Hr)
            {
                throw new CantEditUserException();
            }

            await ValidateSameCompanyAsync(user, targetUser);
        }

        public async Task ValidateDepartmentAccessAsync(Guid userId, Guid departmentId)
        {
            var user = await _usersRepository.GetByIdAsync(userId) ?? throw new UserNotFoundRequest();
            var department = await _departmentsRepository.GetDepartmentByIdAsync(departmentId) ?? throw new DepartmentNotFoundRequest();

            if (user.DepartmentId != departmentId && user.Role != Roles.Director && user.Role != Roles.Hr)
            {
                throw new CantAddUserToAnotherDepartment();
            }

            await ValidateSameCompanyAsync(user, department.CompanyId);
        }

        public async Task ValidateCompanyAccessAsync(Guid userId, Guid companyId)
        {
            var user = await _usersRepository.GetByIdAsync(userId) ?? throw new UserNotFoundRequest();
            await ValidateSameCompanyAsync(user, companyId);
        }
        

        public async Task ValidateUserCreationRightsAsync(Guid userId, Guid targetDepartmentId, Roles role)
        {
            if (role == Roles.Director) throw new CantCreateDirectorException();

            var user = await _usersRepository.GetByIdAsync(userId) ?? throw new UserNotFoundRequest();
            var department = await _departmentsRepository.GetDepartmentByIdAsync(targetDepartmentId) ?? throw new DepartmentNotFoundRequest();

            if (user.Role != Roles.Director && user.Role != Roles.Hr && department.SupervisorId != userId)
            {
                throw new CantAddUserToAnotherDepartment();
            }

            await ValidateSameCompanyAsync(user, department.CompanyId);
        }

        public async Task ValidateUserDeletionRightsAsync(Guid userId, Guid targetUserId)
        {
            var user = await _usersRepository.GetByIdAsync(userId) ?? throw new UserNotFoundRequest();
            var targetUser = await _usersRepository.GetByIdAsync(targetUserId) ?? throw new UserNotFoundRequest();

            if (user.Role != Roles.Director && user.Role != Roles.Hr)
            {
                throw new CantDeleteUserException();
            }

            await ValidateSameCompanyAsync(user, targetUser);
        }

        public async Task ValidateUserUpdateRightsAsync(Guid userId, Guid targetUserId, Guid? newDepartmentId)
        {
            var user = await _usersRepository.GetByIdAsync(userId) ?? throw new UserNotFoundRequest();
            var targetUser = await _usersRepository.GetByIdAsync(targetUserId) ?? throw new UserNotFoundRequest();

            if (user.Id != targetUserId && user.Role != Roles.Director && user.Role != Roles.Hr)
            {
                throw new CantEditUserException();
            }

            await ValidateSameCompanyAsync(user, targetUser);

            if (newDepartmentId.HasValue)
            {
                if (user.Role != Roles.Director && user.Role != Roles.Hr)
                {
                    throw new CantEditDepartmentException();
                }

                var newDepartment = await _departmentsRepository.GetDepartmentByIdAsync(newDepartmentId.Value) 
                    ?? throw new DepartmentNotFoundRequest();
                
                await ValidateSameCompanyAsync(user, newDepartment.CompanyId);
            }
        }

        private async Task ValidateSameCompanyAsync(DbUser user, Guid companyId)
        {
            var userDepartment = await _departmentsRepository.GetDepartmentByIdAsync((Guid)user.DepartmentId!) 
                ?? throw new DepartmentNotFoundRequest();
            
            if (userDepartment.CompanyId != companyId)
            {
                throw new DepartmentNotFoundRequest();
            }
        }

        private async Task ValidateSameCompanyAsync(DbUser user1, DbUser user2)
        {
            var user1Department = await _departmentsRepository.GetDepartmentByIdAsync((Guid)user1.DepartmentId!) 
                ?? throw new DepartmentNotFoundRequest();
            var user2Department = await _departmentsRepository.GetDepartmentByIdAsync((Guid)user2.DepartmentId!) 
                ?? throw new DepartmentNotFoundRequest();
            
            if (user1Department.CompanyId != user2Department.CompanyId)
            {
                throw new DepartmentNotFoundRequest();
            }
        }
    }