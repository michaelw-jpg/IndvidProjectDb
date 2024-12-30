using System;
using System.Collections.Generic;

namespace Lab3;

public partial class Student
{
    public int StudentId { get; set; }

    public int? FkClassId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Personnummer { get; set; } = null!;

    public virtual Class? FkClass { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
