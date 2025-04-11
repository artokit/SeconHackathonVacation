using Common;

namespace Api.Services.Interfaces;

public interface IRightService
{
    public Task ValidateUserAccessAsync(Guid userId, Guid targetUserId);
    public Task ValidateDepartmentAccessAsync(Guid userId, Guid departmentId);
    public Task ValidateCompanyAccessAsync(Guid userId, Guid companyId);
    public Task ValidateUserCreationRightsAsync(Guid userId, Guid targetDepartmentId, Roles role);
    public Task ValidateUserDeletionRightsAsync(Guid userId, Guid targetUserId);
    public Task ValidateUserUpdateRightsAsync(Guid userId, Guid targetUserId, Guid? newDepartmentId);
}