
#nullable disable
using System;
using System.Collections.Generic;

namespace CourseManagment.Domain.Entities;

public partial class Instructor : Person
{
    public DateTime? HireDate { get; set; }


}