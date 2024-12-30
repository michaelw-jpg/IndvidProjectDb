using System;
using System.Collections.Generic;

namespace Lab3;

public partial class Grade
{
    public int GradeId { get; set; }

    public int? FkStudentId { get; set; }

    public int? FkTeacherId { get; set; }

    public int? FkCourseId { get; set; }

    public string Grade1 { get; set; } = null!;

    public DateOnly GradeDate { get; set; }

    public int? GradeNr { get; set; }

    public virtual Course? FkCourse { get; set; }

    public virtual Student? FkStudent { get; set; }

    public virtual Employee? FkTeacher { get; set; }
}
