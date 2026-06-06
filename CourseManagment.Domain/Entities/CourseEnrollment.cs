
#nullable disable
using CourseManagment.Domain.Base;
using System;
using System.Collections.Generic;

namespace CourseManagment.Domain.Entities;

public partial class CourseEnrollment : BaseEntity
{
    

    public int CourseId { get; set; }

    public int StudentId { get; set; }

    public DateTime EnrollmentDate { get; set; }

    public int EnrollmentStatusId { get; set; }

    
}