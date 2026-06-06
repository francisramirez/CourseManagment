
#nullable disable
using System;
using System.Collections.Generic;

namespace CourseManagment.Domain.Entities;

public partial class OnlineCourse
{
    public int CourseId { get; set; }

    public string Url { get; set; }
}