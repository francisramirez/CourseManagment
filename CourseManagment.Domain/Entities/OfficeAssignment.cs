
#nullable disable
using System;
using System.Collections.Generic;

namespace CourseManagment.Domain.Entities;

public partial class OfficeAssignment
{
    public int InstructorId { get; set; }

    public string Location { get; set; }

    public byte[] Timestamp { get; set; }
}