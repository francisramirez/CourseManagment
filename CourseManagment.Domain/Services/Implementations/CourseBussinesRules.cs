using CourseManagment.Domain.Interfaces;
using CourseManagment.Domain.Result;
using CourseManagment.Domain.Services.Interfaces;

namespace CourseManagment.Domain.Services.Implementations
{
    public sealed class CourseBussinesRules : ICourseBussinesRules
    {
        private readonly ICourseRepository _courseRepository;

        public CourseBussinesRules(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public Task<OperationResult> IsCourseNameUnique(string courseName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> IsCourseCodeUnique(string courseCode, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> DoesInstructorExist(int instructorId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}