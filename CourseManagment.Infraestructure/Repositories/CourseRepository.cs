using CourseManagment.Domain.Entities;
using CourseManagment.Domain.Interfaces;
using CourseManagment.Domain.Models;
using CourseManagment.Domain.Result;
using CourseManagment.Domain.Services.Interfaces;
using CourseManagment.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CourseManagment.Infraestructure.Repositories
{
    public sealed class CourseRepository : ICourseRepository, ICourseBussinesRules
    {
        private readonly CourseMagnamentDbContext _context;
        private readonly ILogger<CourseRepository> _logger;

        public CourseRepository(CourseMagnamentDbContext context,
                                ILogger<CourseRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        #region "Methods"
        public async Task<OperationResult> AddAsync(Course entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                // Validaciones de campos //

                if (entity is null)
                {
                    result = OperationResult.Fail("Course entity cannot be null.", "INVALID_INPUT");
                    return result;
                }
                if (string.IsNullOrEmpty(entity.Title))
                {
                    result = OperationResult.Fail("Course title cannot be null or empty.", "INVALID_INPUT");
                    return result;
                }
                if (entity.Title.Length > 50)
                {
                    result = OperationResult.Fail("Course title cannot exceed 50 characters.", "INVALID_INPUT");
                    return result;
                }
                if (string.IsNullOrEmpty(entity.CourseCode))
                {
                    result = OperationResult.Fail("Course code cannot be null or empty.", "INVALID_INPUT");
                    return result;
                }
                if (entity.CourseCode.Length > 10)
                {
                    result = OperationResult.Fail("Course code cannot exceed 10 characters.", "INVALID_INPUT");
                    return result;
                }
                if (entity.Credits <= 0)
                {
                    result = OperationResult.Fail("Course credits must be greater than 0.", "INVALID_INPUT");
                    return result;
                }
                if (entity.DepartmentId <= 0)
                {
                    result = OperationResult.Fail("Department ID must be greater than 0.", "INVALID_INPUT");
                    return result;
                }
                bool isDepartmentValid = await _context.Departments.AnyAsync(department => department.Id == entity.DepartmentId);
                if (!isDepartmentValid)
                {
                    result = OperationResult.Fail("The specified department does not exist.", "INVALID_DEPARTMENT");
                    return result;
                }

                await _context.Courses.AddAsync(entity);
                await _context.SaveChangesAsync();

                result = OperationResult.Ok("Course added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a course.");
                result = OperationResult.Fail("An error occurred while adding a course.", "ERROR");
            }
            return result;
        }

        public async Task<OperationResult> AddRangeAsync(IEnumerable<Course> entities)
        {
            OperationResult result = new OperationResult();
            try
            {
                await _context.Courses.AddRangeAsync(entities);
                await _context.SaveChangesAsync();
                result = OperationResult.Ok("Courses added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding courses.");
                result = OperationResult.Fail("An error occurred while adding courses.", "ERROR");
            }
            return result;
        }

        public async Task<IReadOnlyList<Course>> GetAllAsync()
        {
            return await _context.Courses
                .Where(c => !c.Deleted)
                .ToListAsync();
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _context.Courses
                .Where(c => c.Id == id && !c.Deleted)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Course>> GetCoursesByDatesdAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Courses
                .Where(c => c.StartDate >= startDate && c.EndDate <= endDate && !c.Deleted)
                .ToListAsync();
        }

        public async Task<List<CourseDepartmentModel>> GetCoursesWithDepartmentsAsync()
        {
            return await (from course in _context.Courses
                          where !course.Deleted
                          join department in _context.Departments
                          on course.DepartmentId equals department.Id into courseDept
                          from dept in courseDept.DefaultIfEmpty()
                          select new CourseDepartmentModel
                          {
                              CourseId = course.Id,
                              Title = course.Title,
                              CourseCode = course.CourseCode,
                              DepartmentId = dept.Id,
                              DepartmentName = dept.Name
                          }).ToListAsync();
        }

        public async Task<List<CourseDepartmentModel>> GetCoursesWithDepartmentsAsync(int departmentId)
        {
            return await (from course in _context.Courses
                          where course.DepartmentId == departmentId && !course.Deleted
                          join department in _context.Departments
                          on course.DepartmentId equals department.Id into courseDept
                          from dept in courseDept.DefaultIfEmpty()
                          select new CourseDepartmentModel
                          {
                              CourseId = course.Id,
                              Title = course.Title,
                              CourseCode = course.CourseCode,
                              DepartmentId = dept.Id,
                              DepartmentName = dept.Name
                          }).ToListAsync();
        }


        public async Task<OperationResult> Remove(Course entity)
        {

            Course courseToRemove = await _context.Courses.FindAsync(entity.Id);

            if (courseToRemove is null)
            {
                return OperationResult.Fail("Course not found.", "NOT_FOUND");
            }

            courseToRemove.Deleted = true;
            courseToRemove.DeletedDate = DateTime.Now;
            courseToRemove.UserDeleted = entity.UserDeleted;

            _context.Courses.Update(courseToRemove);
            await _context.SaveChangesAsync();
            return OperationResult.Ok("Course removed successfully.");
        }

        public async Task<OperationResult> Update(Course entity)
        {
            OperationResult result = new OperationResult();

            Course courseToUpdate = await _context.Courses.FindAsync(entity.Id);

            if (courseToUpdate is null)
            {
                return OperationResult.Fail("Course not found.", "NOT_FOUND");
            }


            if (entity is null)
            {
                result = OperationResult.Fail("Course entity cannot be null.", "INVALID_INPUT");
                return result;
            }
            if (string.IsNullOrEmpty(entity.Title))
            {
                result = OperationResult.Fail("Course title cannot be null or empty.", "INVALID_INPUT");
                return result;
            }
            if (entity.Title.Length > 50)
            {
                result = OperationResult.Fail("Course title cannot exceed 50 characters.", "INVALID_INPUT");
                return result;
            }
            if (string.IsNullOrEmpty(entity.CourseCode))
            {
                result = OperationResult.Fail("Course code cannot be null or empty.", "INVALID_INPUT");
                return result;
            }
            if (entity.CourseCode.Length > 10)
            {
                result = OperationResult.Fail("Course code cannot exceed 10 characters.", "INVALID_INPUT");
                return result;
            }
            if (entity.Credits <= 0)
            {
                result = OperationResult.Fail("Course credits must be greater than 0.", "INVALID_INPUT");
                return result;
            }
            if (entity.DepartmentId <= 0)
            {
                result = OperationResult.Fail("Department ID must be greater than 0.", "INVALID_INPUT");
                return result;
            }
            bool isDepartmentValid = await _context.Departments.AnyAsync(department => department.Id == entity.DepartmentId);
            if (!isDepartmentValid)
            {
                result = OperationResult.Fail("The specified department does not exist.", "INVALID_DEPARTMENT");
                return result;
            }



            courseToUpdate.Title = entity.Title;
            courseToUpdate.CourseCode = entity.CourseCode;
            courseToUpdate.Description = entity.Description;
            courseToUpdate.Credits = entity.Credits;
            courseToUpdate.DepartmentId = entity.DepartmentId;
            courseToUpdate.ModifiedDate = DateTime.Now;
            courseToUpdate.UserModified = entity.UserModified;

            _context.Courses.Update(courseToUpdate);
            await _context.SaveChangesAsync();
            return OperationResult.Ok("Course updated successfully.");
        }
        #endregion

        #region "Bussinness Validactions"
        public async Task<OperationResult> DoesInstructorExist(int instructorId, int courseId, CancellationToken cancellationToken)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Checking if the instructor exists for the course. InstructorId: {InstructorId}, CourseId: {CourseId}", instructorId, courseId);

                if (instructorId <= 0 || courseId <= 0)
                {
                    _logger.LogWarning("Instructor ID and Course ID must be greater than zero. InstructorId: {InstructorId}, CourseId: {CourseId}", instructorId, courseId);
                    result = OperationResult.Fail("Instructor ID and Course ID must be greater than zero.", "INVALID_INPUT");
                    return result;
                }

                bool instructorExists = await _context.CourseInstructors.AnyAsync(instructor => instructor.PersonId == instructorId
                                                                               && instructor.CourseId == courseId, cancellationToken);
                if (!instructorExists)
                {
                    _logger.LogWarning("Instructor does not exist for the specified course. InstructorId: {InstructorId}, CourseId: {CourseId}", instructorId, courseId);
                    result = OperationResult.Fail("Instructor does not exist for the specified course.", "INSTRUCTOR_NOT_FOUND");
                }
                else
                {
                    _logger.LogInformation("Instructor exists for the specified course. InstructorId: {InstructorId}, CourseId: {CourseId}", instructorId, courseId);
                    result = OperationResult.Ok("Instructor exists for the specified course.");
                }
            }
            catch (Exception ex)
            {
                result = OperationResult.Fail("An error occurred while checking if the instructor exists for the course.", "ERROR");

                _logger.LogError(ex, "An error occurred while checking if the instructor exists for the course.");
            }
            return result;
        }
        public async Task<OperationResult> IsCourseNameAndCodeUnique(string courseName,
                                                               string courseCode,
                                                               CancellationToken cancellationToken)
        {
            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("Checking if the course name and code are unique. CourseName: {CourseName}, CourseCode: {CourseCode}", courseName, courseCode);


                if (string.IsNullOrEmpty(courseName) || string.IsNullOrEmpty(courseCode))
                {
                    result = OperationResult.Fail("Course name and code cannot be null or empty.", "INVALID_INPUT");
                    return result;
                }

                bool isDuplicate = await _context.Courses.AnyAsync(course => course.Title == courseName
                                                 && course.CourseCode == courseCode, cancellationToken);

                if (isDuplicate)
                {
                    _logger.LogWarning("A course with the same name and code already exists. CourseName: {CourseName}, CourseCode: {CourseCode}", courseName, courseCode);
                    result = OperationResult.Fail("A course with the same name and code already exists.", "DUPLICATE_COURSE");
                }
                else
                {
                    result = OperationResult.Ok("Course name and code are unique.");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while checking if the course name and code are unique.");
                result = OperationResult.Fail("An error occurred while checking if the course name and code are unique.", "ERROR");
            }
            return result;
        }

        #endregion


    }
}
