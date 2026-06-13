

using CourseManagment.Domain.Result;

namespace CourseManagment.Domain.Services.Interfaces
{
    public interface ICourseBussinesRules
    {
        Task<OperationResult> IsCourseNameAndCodeUnique(string courseName, string courseCode, CancellationToken cancellationToken);
        Task<OperationResult> DoesInstructorExist(int instructorId, int courseId, CancellationToken cancellationToken);
    }
}
