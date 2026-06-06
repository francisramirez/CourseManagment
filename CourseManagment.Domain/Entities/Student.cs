
#nullable disable
using CourseManagment.Domain.Base;
using System;
using System.Collections.Generic;

namespace CourseManagment.Domain.Entities;

public partial class Student : BaseEntity
{
    public string StudentNumber { get; set; }
    public DateTime? EnrollmentDate { get; set; }


}