
using CourseManagment.Domain.Entities;
using CourseManagment.Domain.Interfaces;
using CourseManagment.Domain.Models;
using CourseManagment.Domain.Result;
using CourseManagment.Domain.Services.Interfaces;
using CourseManagment.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

                if (entity is not null)
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
      
                //foreach (var item in entities)
                //{
                //    await _context.Courses.AddAsync(item);
                //}


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

        public Task<IReadOnlyList<Course>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Course?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Course>> GetCoursesByDatesdAsync(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<List<CourseDepartmentModel>> GetCoursesWithDepartmentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<CourseDepartmentModel>> GetCoursesWithDepartmentsAsync(int departmentId)
        {
            throw new NotImplementedException();
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
            Course courseToUpdate = await _context.Courses.FindAsync(entity.Id);

            if (courseToUpdate is null)
            {
                return OperationResult.Fail("Course not found.", "NOT_FOUND");
            }


            if (entity is not null)
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

                if (instructorId <= 0 && courseId <= 0)
                {
                    _logger.LogWarning("Instructor ID and Course ID must be greater than zero. InstructorId: {InstructorId}, CourseId: {CourseId}", instructorId, courseId);
                    result = OperationResult.Fail("Instructor ID and Course ID must be greater than zero.", "INVALID_INPUT");
                    return result;
                }

                bool instructorExists = await _context.CourseInstructors.AnyAsync(instructor => instructor.PersonId == instructorId
                                                                               && instructor.CourseId == courseId, cancellationToken);
                if (instructorExists)
                {
                    _logger.LogInformation("Instructor exists for the specified course. InstructorId: {InstructorId}, CourseId: {CourseId}", instructorId, courseId);
                    result = OperationResult.Fail("Instructor does exist for the specified course.", "INSTRUCTOR_NOT_FOUND");
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


                if (string.IsNullOrEmpty(courseName) && string.IsNullOrEmpty(courseCode))
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
