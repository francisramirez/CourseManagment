

using CourseManagment.Domain.Result;

namespace CourseManagment.Domain.Services.Interfaces
{
    public interface ICourseBussinesRules
    {
            Task<OperationResult> IsCourseNameUnique(string courseName, CancellationToken cancellationToken);
            Task<OperationResult> IsCourseCodeUnique(string courseCode, CancellationToken cancellationToken);
            Task<OperationResult> DoesInstructorExist(int instructorId, CancellationToken cancellationToken);
    }
}
