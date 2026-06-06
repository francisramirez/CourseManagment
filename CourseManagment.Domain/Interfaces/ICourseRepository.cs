using CourseManagment.Domain.Entities;
using CourseManagment.Domain.Repository;

namespace CourseManagment.Domain.Interfaces
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
      
        /// <summary>
        /// Obtains a list of courses along with their associated department information.
        /// </summary>
        /// <returns></returns>
        Task<List<Models.CourseDepartmentModel>> GetCoursesWithDepartmentsAsync();

        /// <summary>
        /// Obtains a list of courses along with their associated department information for a specific department.
        /// </summary>
        /// <param name="departmentId">department Id</param>
        /// <returns></returns>
        Task<List<Models.CourseDepartmentModel>> GetCoursesWithDepartmentsAsync(int departmentId);

        /// <summary>
        /// Obtains a list of courses that fall within a specified date range.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<List<Course>> GetCoursesByDatesdAsync(DateTime startDate, DateTime endDate);
    }
}
