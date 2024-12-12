using Lab3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class GradeSearch
    {
        public  static void AverageAll(labb2Context context)
        {
            var grades = context.Grades.GroupBy(g => g.FkCourse)
                                .Select(group => new
                                {
                                    Subject = group.Key,
                                    GradeAvg = group.Average(g => g.GradeNr),
                                    Max = group.Max(g => g.GradeNr),
                                    Min = group.Min(g => g.GradeNr)

                                }).ToList();
            foreach (var student in grades)
            {
                Console.WriteLine($"Max: {student.Max} Min: {student.Min} AVG:{student.GradeAvg:F2} Ämne: {student.Subject.Subjects}");
            }
        }
        
        public static void GradesLastMonth(labb2Context context)
        {
            DateTime firstDayOfLastMonth = DateGetterFirst();
            DateTime lastDayOfLastMonth = DateGetterLast();
            var gradesLastMonth = context.Grades
                                            .Where(g => g.GradeDate >= DateOnly.FromDateTime(firstDayOfLastMonth) &&
                                           g.GradeDate <= DateOnly.FromDateTime(lastDayOfLastMonth))
                                            .Select(g => new { //we skip join by accessing the right student through FKstudent property
                                                sFirstName = g.FkStudent.FirstName,
                                                sLastName = g.FkStudent.LastName,
                                                courseName = g.FkCourse.Subjects,
                                                g.Grade1
                                            })
                                            .ToList();
            foreach (var grade in gradesLastMonth)
            {
                Console.WriteLine($"{grade.sFirstName} {grade.sLastName}, {grade.courseName}, {grade.Grade1}");
            }
        }
        
        public static DateTime DateGetterFirst()
        {
            DateTime today = DateTime.Now;
            DateTime firstDayOfLastMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-1);
            return firstDayOfLastMonth;
        }
        public static DateTime DateGetterLast()
        {
            DateTime today = DateTime.Now;
            DateTime firstDayOfLastMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-1);
            DateTime lastDayOfLastMonth = firstDayOfLastMonth.AddMonths(1).AddDays(-1);
            return lastDayOfLastMonth;
        }
    }
}
