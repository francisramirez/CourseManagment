
#nullable disable
using CourseManagment.Domain.Base;

namespace CourseManagment.Domain.Entities;

public partial class Course : BaseEntity
{
     
    public string CourseCode { get; set; }
    public string Title { get; set; }
    public int Credits { get; set; }
    public int DepartmentId { get; set; }

    
}