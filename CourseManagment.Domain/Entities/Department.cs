
#nullable disable
using System;
using System.Collections.Generic;
using CourseManagment.Domain.Base;


namespace CourseManagment.Domain.Entities;

public partial class Department : BaseEntity
{
    public string Name { get; set; }

    public decimal Budget { get; set; }

    public DateTime StartDate { get; set; }

    public int? Administrator { get; set; }

   
}