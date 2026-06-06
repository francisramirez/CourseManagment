
#nullable disable
using CourseManagment.Domain.Base;
using System;
using System.Collections.Generic;

namespace CourseManagment.Domain.Entities;

public abstract class Person : BaseEntity
{
   
    public string LastName { get; set; }

    public string FirstName { get; set; }

    public DateTime? HireDate { get; set; }

    public DateTime? EnrollmentDate { get; set; }

    public string Discriminator { get; set; }
}