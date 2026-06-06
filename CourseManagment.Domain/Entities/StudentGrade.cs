
#nullable disable
using System;
using System.Collections.Generic;

namespace CourseManagment.Domain.Entities;

public partial class StudentGrade
{
    public int EnrollmentId { get; set; }

    public int CourseId { get; set; }

    public int StudentId { get; set; }

    public decimal? Grade { get; set; }
}