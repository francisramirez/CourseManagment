
#nullable disable
using CourseManagment.Domain.Base;
using System;
using System.Collections.Generic;

namespace CourseManagment.Domain.Entities;

public partial class Course : BaseEntity
{
     

    public string Title { get; set; }

    public int Credits { get; set; }

    public int DepartmentId { get; set; }

    
}