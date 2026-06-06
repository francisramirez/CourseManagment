
#nullable disable
using CourseManagment.Domain.Base;
using System;
using System.Collections.Generic;

namespace CourseManagment.Domain.Entities;

public partial class EnrollmentStatus : BaseEntity
{
    

    public string Code { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool IsActive { get; set; }

   
}