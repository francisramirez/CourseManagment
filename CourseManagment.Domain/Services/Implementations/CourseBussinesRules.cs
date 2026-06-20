using CourseManagment.Domain.Interfaces;
using CourseManagment.Domain.Result;
using CourseManagment.Domain.Services.Interfaces;

namespace CourseManagment.Domain.Services.Implementations
{
    public sealed class CourseBussinesRules : ICourseBussinesRules
    {
        private readonly ICourseRepository _courseRepository;

        public CourseBussinesRules(ICourseRepository courseRepository) => _courseRepository = courseRepository;

        public async Task<OperationResult> DoesInstructorExist(int instructorId, int courseId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> IsCourseNameAndCodeUnique(string courseName, string courseCode, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}