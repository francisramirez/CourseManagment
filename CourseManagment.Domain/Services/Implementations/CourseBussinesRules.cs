using CourseManagment.Domain.Interfaces;
using CourseManagment.Domain.Result;
using CourseManagment.Domain.Services.Interfaces;

namespace CourseManagment.Domain.Services.Implementations
{
    public sealed class CourseBussinesRules : ICourseBussinesRules
    {
        private readonly ICourseRepository _courseRepository;

        public CourseBussinesRules(ICourseRepository courseRepository) => _courseRepository = courseRepository;
        public async Task<OperationResult> DoesInstructorExist(int instructorId, CancellationToken cancellationToken) => await _courseRepository.DoesInstructorExist(instructorId, cancellationToken);
        public async Task<OperationResult> IsCourseNameAndCodeUnique(string courseName, string courseCode, CancellationToken cancellationToken) => await _courseRepository.IsCourseNameAndCodeUnique(courseName, courseCode, cancellationToken);
    }
}