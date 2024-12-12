using System;
using System.Collections.Generic;

namespace Lab3.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public int? FkTeacherId { get; set; }

    public string? Subjects { get; set; }

    public virtual Employee? FkTeacher { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
