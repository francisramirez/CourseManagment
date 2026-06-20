using CourseManagment.Domain.Entities;
using CourseManagment.Domain.Result;
namespace CourseManagment.Domain.Services.Interfaces
{
    public interface IDepartmentBussinesRules
    {
        Task<OperationResult> ValidateForCreateAsync(Department entity, CancellationToken cancellationToken);
        Task<OperationResult> ValidateForUpdateAsync(Department entity, CancellationToken cancellationToken);
    }
}
