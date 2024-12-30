using Lab3.Data;
using Lab3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class DbChange
    {
        public static Tuple<string, int> GetGradeInfo()
        {
            Console.WriteLine("Vilket betyg fick eleven? 1-5");
            int.TryParse(Console.ReadLine(), out int GradeNR);
            string gradeLetter;
            switch (GradeNR)
            {
                case 1:
                    gradeLetter = "F";
                    return Tuple.Create(gradeLetter, GradeNR);
                    
                case 2:
                     gradeLetter = "E";
                    var info = Tuple.Create(gradeLetter, GradeNR);
                    return Tuple.Create(gradeLetter, GradeNR);
                    
                case 3:
                    gradeLetter = "C";
                    return Tuple.Create(gradeLetter, GradeNR);
                case 4:
                     gradeLetter = "B";
                    return Tuple.Create(gradeLetter, GradeNR);
                case 5:
                    gradeLetter = "A";
                    return Tuple.Create(gradeLetter, GradeNR);
                default:
                    break;
            }
            return null;
            
        }

        public static void GetGradeSetter(labb2Context context, int studentId, int courseId)
        {
            var query = context.Courses
                    .Where(c => c.CourseId == courseId)
                    .Select(s => s.FkTeacherId)
                    .First();

            AddGrade(context, studentId, courseId, query);
        }

        public static void AddGrade(labb2Context context, int studentId, int courseId, int? teacherId)
        {
            var info = GetGradeInfo();
           

            var setGrade = new Grade
            {
                FkStudentId = studentId,
                FkCourseId = courseId,
                FkTeacherId = teacherId,
                Grade1 = info.Item1,
                GradeDate = DateOnly.FromDateTime(DateTime.Now),
                GradeNr = info.Item2
            };
            context.Grades.Add(setGrade);
            context.SaveChanges();
            Console.WriteLine($"Ett nytt betyg har satts!");
        }

        public static int WhichStudentToGrade(labb2Context context)
        {
            Search.GetStudents(context, 1, 1);
            Console.WriteLine("Ange till vilken student du vill lägga till ett betyg");
            int.TryParse(Console.ReadLine(), out int studentChoice);
            return studentChoice;

        }
        
        public static void AddEmployee(labb2Context context)
        {
            Console.WriteLine("Anställdas Förnamn:");
            string eFirstname = Console.ReadLine();
            Console.WriteLine("Anställdas Efternamn:");
            string eLastname = Console.ReadLine();
            Console.WriteLine("Vilken avdelning ska den anställda jobba på?");
            string department = Console.ReadLine();
            int salary = 0;
            while (salary < 1)
            {
                Console.WriteLine("Hur mycket har den anställda i lön?");
                int.TryParse(Console.ReadLine(), out salary);
            }
            var newEmployee = new Employee()
            {
                FirstName = eFirstname,
                LastName = eLastname,
                Department = department,
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                Salary = salary
            };
            context.Employees.Add(newEmployee);
            context.SaveChanges();
        }
        public static void AddStudent(labb2Context context)
        {
            Console.WriteLine("Elevens förnamn:");
            string firstname = Console.ReadLine();
            Console.WriteLine("Elevens Efternamn:");
            string lastName = Console.ReadLine();
            Console.WriteLine("Elevens personnummer?(ÅÅMMDD-XXXX)");
            string pNummer = Console.ReadLine();
            Console.WriteLine("I vilken klass ska eleven vara?");
            var sClass = context.Classes.Select(c => c.ClassName).ToList();
            int i = 1;
            foreach (var Eclass in sClass)
            {
                Console.WriteLine($"[{i}] {Eclass}");
                i++;
            }
            int.TryParse(Console.ReadLine(), out int answer);
            var newStudent = new Student()
            {
                FirstName = firstname,
                LastName = lastName,
                Personnummer = pNummer,
                FkClassId = answer
            };
            context.Students.Add(newStudent);
            context.SaveChanges();
        }

    }
}
