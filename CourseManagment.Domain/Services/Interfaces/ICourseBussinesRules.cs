

using CourseManagment.Domain.Result;

namespace CourseManagment.Domain.Services.Interfaces
{
    public interface ICourseBussinesRules
    {
            Task<OperactionResult> IsCourseNameUnique(string courseName, CancellationToken cancellationToken);
            Task<OperactionResult> IsCourseCodeUnique(string courseCode, CancellationToken cancellationToken);
            Task<OperactionResult> DoesInstructorExist(int instructorId, CancellationToken cancellationToken);
    }
}
