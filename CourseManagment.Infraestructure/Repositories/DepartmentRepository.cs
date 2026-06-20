using CourseManagment.Domain.Entities;
using CourseManagment.Domain.Interfaces;
using CourseManagment.Domain.Result;
using CourseManagment.Domain.Services.Interfaces;
using CourseManagment.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CourseManagment.Infraestructure.Repositories
{
    public sealed class DepartmentRepository : IDepartmentRepository, IDepartmentBussinesRules
    {
        private readonly CourseMagnamentDbContext _context;
        private readonly ILogger<DepartmentRepository> _logger;

        public DepartmentRepository(CourseMagnamentDbContext context, ILogger<DepartmentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        #region "Methods"

        public async Task<OperationResult> AddAsync(Department entity)
        {
            try
            {
                if (entity is null)
                    return OperationResult.Fail("Department entity cannot be null.", "INVALID_INPUT");

                if (string.IsNullOrWhiteSpace(entity.Name))
                    return OperationResult.Fail("Department name cannot be null or empty.", "INVALID_INPUT");

                bool exists = await _context.Departments.AnyAsync(d => d.Name == entity.Name);
                if (exists)
                    return OperationResult.Fail("Department name already exists.", "DUPLICATE_DEPARTMENT");

                await _context.Departments.AddAsync(entity);
                await _context.SaveChangesAsync();

                return OperationResult.Ok("Department added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a department.");
                return OperationResult.Fail("An error occurred while adding a department.", "ERROR");
            }
        }

        public async Task<OperationResult> AddRangeAsync(IEnumerable<Department> entities)
        {
            try
            {
                await _context.Departments.AddRangeAsync(entities);
                await _context.SaveChangesAsync();
                return OperationResult.Ok("Departments added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding departments.");
                return OperationResult.Fail("An error occurred while adding departments.", "ERROR");
            }
        }

        public async Task<IReadOnlyList<Department>> GetAllAsync()
        {
            return await _context.Departments
                                 .AsNoTracking()
                                 .Where(d => !d.Deleted)
                                 .ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _context.Departments
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(d => d.Id == id && !d.Deleted);
        }

        public async Task<OperationResult> Remove(Department entity)
        {
            try
            {
                var dept = await _context.Departments.FindAsync(entity.Id);
                if (dept is null)
                    return OperationResult.Fail("Department not found.", "NOT_FOUND");

                dept.Deleted = true;
                dept.DeletedDate = DateTime.Now;
                dept.UserDeleted = entity.UserDeleted;

                _context.Departments.Update(dept);
                await _context.SaveChangesAsync();

                return OperationResult.Ok("Department removed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while removing a department.");
                return OperationResult.Fail("An error occurred while removing a department.", "ERROR");
            }
        }

        public async Task<OperationResult> Update(Department entity)
        {
            try
            {
                if (entity is null)
                    return OperationResult.Fail("Department entity cannot be null.", "INVALID_INPUT");

                var dept = await _context.Departments.FindAsync(entity.Id);
                if (dept is null)
                    return OperationResult.Fail("Department not found.", "NOT_FOUND");

                if (string.IsNullOrWhiteSpace(entity.Name))
                    return OperationResult.Fail("Department name cannot be null or empty.", "INVALID_INPUT");

                bool duplicate = await _context.Departments.AnyAsync(d => d.Name == entity.Name && d.Id != entity.Id);
                if (duplicate)
                    return OperationResult.Fail("Another department with the same name exists.", "DUPLICATE_DEPARTMENT");

                dept.Name = entity.Name;
                dept.Budget = entity.Budget;
                dept.StartDate = entity.StartDate;
                dept.Administrator = entity.Administrator;
                dept.ModifyDate = DateTime.Now;
                dept.UserMod = entity.UserMod;


                _context.Departments.Update(dept);
                await _context.SaveChangesAsync();

                return OperationResult.Ok("Department updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a department.");
                return OperationResult.Fail("An error occurred while updating a department.", "ERROR");
            }
        }

        #endregion

        #region "Bussinness Validactions"

        public async Task<OperationResult> ValidateForCreateAsync(Department entity, CancellationToken cancellationToken)
        {
            try
            {
                if (entity is null)
                    return OperationResult.Fail("Department entity cannot be null.", "INVALID_INPUT");

                if (string.IsNullOrWhiteSpace(entity.Name))
                    return OperationResult.Fail("Department name cannot be null or empty.", "INVALID_INPUT");

                bool exists = await _context.Departments.AnyAsync(d => d.Name == entity.Name, cancellationToken);
                if (exists)
                    return OperationResult.Fail("Department name already exists.", "DUPLICATE_DEPARTMENT");

                return OperationResult.Ok("Valid for create.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while validating department for create.");
                return OperationResult.Fail("An error occurred while validating department for create.", "ERROR");
            }
        }

        public async Task<OperationResult> ValidateForUpdateAsync(Department entity, CancellationToken cancellationToken)
        {
            try
            {
                if (entity is null)
                    return OperationResult.Fail("Department entity cannot be null.", "INVALID_INPUT");

                bool exists = await _context.Departments.AnyAsync(d => d.Id == entity.Id && !d.Deleted, cancellationToken);
                if (!exists)
                    return OperationResult.Fail("Department not found.", "NOT_FOUND");

                return OperationResult.Ok("Valid for update.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while validating department for update.");
                return OperationResult.Fail("An error occurred while validating department for update.", "ERROR");
            }
        }

        #endregion
    }
}
