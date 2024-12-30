using System;
using System.Collections.Generic;

namespace Lab3;

public partial class ActiveEnrollment
{
    public int ActiveCourseId { get; set; }

    public int? FkCourseId { get; set; }

    public int? FkStudentId { get; set; }

    public DateOnly? CourseStartDate { get; set; }

    public DateOnly? CourseEndDate { get; set; }

    public virtual Course? FkCourse { get; set; }
}
