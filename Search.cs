using Lab3.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab3
{
    public class Search
    {

        

        public static void StudentGrades(labb2Context context, int studentID)
        {
            var gradesList = context.Grades
                .Where(g => g.FkStudentId == studentID)
                .Select(temp => new
                {
                    temp.Grade1,
                    temp.GradeNr,
                    temp.GradeDate,
                    temp.FkCourse.Subjects,
                    TeacherName = temp.FkTeacher.FirstName + " " + temp.FkTeacher.LastName
                }).ToList();
            foreach(var item in gradesList)
            {
                Console.WriteLine($"Betyg: {item.Grade1}, Poäng: {item.GradeNr} , Datum: {item.GradeDate}, Ämne: {item.Subjects} , Lärare: {item.TeacherName}");
            }
        }
        public static void AverageSalaryDepartment(labb2Context context)
        {
            var avgSalary = context.Employees
                .GroupBy(e => e.Department)
                .Select(g => new
                {
                    Department = g.Key,
                    avgsalary = g.Average(e => e.Salary)
                })
                .ToList();
            foreach(var item in avgSalary)
            {
                Console.WriteLine($"Department: {item.Department} har en medellön på {item.avgsalary:F0}");
            }
        }

        public static void SalaryPerDepartment(labb2Context context)
        {
            var employeeSalary = context.Employees
                .GroupBy(e => e.Department)
                .Select(g => new
                {
                    Department = g.Key,
                    TotalSalary = g.Sum(e => e.Salary)
                })
                .ToList();
            foreach(var Department in employeeSalary)
            {
                Console.WriteLine($"Department: {Department.Department} Har en totallön på {Department.TotalSalary}");
            }
        }
        public static int CourseChoice(labb2Context context)
        {
            var courses = context.Courses
                .Select(c => new{ c.CourseId, c.Subjects,c.FkTeacherId }).ToList();
            foreach(var course in courses)
            {
                Console.WriteLine($"{course.CourseId} {course.Subjects}");
            }
            Console.WriteLine("Vilken kurs har studenten läst?");
            int.TryParse(Console.ReadLine(), out int courseToGrade);
            return courseToGrade;
        }
        
        public static string chooseClass(labb2Context context)
        {

            Console.WriteLine("Vilken Klass vill du välja?");
            Console.WriteLine("Tryck enter för att återgå till menyn");
            var classes = context.Classes.Select(c => c.ClassName).ToArray();

            foreach (var class1 in classes)
            {
                Console.WriteLine($"{class1}");

            }
            string answer = Console.ReadLine();
            return answer;
        }

       public static void GetStudents(labb2Context context, int asc, int firstOrLast) //gets student from database
        {

            if (asc == 1 && firstOrLast == 1)
            {
                var students = context.Students
                           .OrderBy(s => s.FirstName)
                           .Join(context.Classes, student => student.FkClassId,
                           cls => cls.ClassId, (student, cls) => new //SELECTING is done inside the join statement with new
                           {
                               student.StudentId,
                               student.FirstName,
                               student.LastName,
                               student.Personnummer,
                               cls.ClassName
                           }).ToList();
                foreach (var student in students)
                {
                    Console.WriteLine($"ID:[{student.StudentId}] {student.FirstName}, {student.LastName}, {student.Personnummer},{student.ClassName}");
                }
            }
            else if (asc == 0 && firstOrLast == 1)
            {
                var students = context.Students
                           .OrderByDescending(s => s.FirstName)
                           .Join(context.Classes, student => student.FkClassId,
                           cls => cls.ClassId, (student, cls) => new //SELECTING is done inside the join statement with new
                           {
                               student.StudentId,
                               student.FirstName,
                               student.LastName,
                               student.Personnummer,
                               cls.ClassName
                           }).ToList();
                foreach (var student in students)
                {
                    Console.WriteLine($"ID:[{student.StudentId}] {student.FirstName}, {student.LastName}, {student.Personnummer},{student.ClassName}");
                }
            }
            else if (asc == 0 && firstOrLast == 0)
            {
                var students = context.Students
                           .OrderByDescending(s => s.LastName)
                           .Join(context.Classes, student => student.FkClassId,
                           cls => cls.ClassId, (student, cls) => new //SELECTING is done inside the join statement with new
                           {
                               student.StudentId,
                               student.FirstName,
                               student.LastName,
                               student.Personnummer,
                               cls.ClassName
                           }).ToList();
                foreach (var student in students)
                {
                    Console.WriteLine($"ID:[{student.StudentId}]{student.FirstName}, {student.LastName}, {student.Personnummer},{student.ClassName}");
                }
            }
            else if (asc == 1 && firstOrLast == 0)
            {
                var students = context.Students
                           .OrderBy(s => s.LastName)
                           .Join(context.Classes, student => student.FkClassId,
                           cls => cls.ClassId, (student, cls) => new //SELECTING is done inside the join statement with new
                           {
                               student.StudentId,
                               student.FirstName,
                               student.LastName,
                               student.Personnummer,
                               cls.ClassName
                           }).ToList();
                foreach (var student in students)
                {
                    Console.WriteLine($"ID:[{student.StudentId}] {student.FirstName}, {student.LastName}, {student.Personnummer},{student.ClassName}");
                }
            }
        }


        public static void StudentSearch(labb2Context context)//main student search method
        {
            Console.WriteLine("vill du sortera efter 1: Förnamn, 2: Efternamn");
            int.TryParse(Console.ReadLine(), out int sName);
            Console.WriteLine("Vill du sortera Acending/descending, 1/2");
            int.TryParse(Console.ReadLine(), out int sSort);
            GetStudents(context, sSort - 1, sName - 1);
            string whichClass = chooseClass(context);
            if (whichClass == "")
            {
                return;
            }
            var students = context.Students
                                   .Where(s => s.FkClass.ClassName == whichClass)
                                   .OrderBy(s => s.FirstName)
                                   .Select(s => new {s.StudentId, s.FirstName, s.LastName, s.Personnummer, Classname = s.FkClass.ClassName }) //instead of using join we can acces class through Student.FkClass property
                                   .ToList();
            Console.WriteLine("studenter:");

            foreach (var student in students)
                Console.WriteLine($"ID:[{student.StudentId}] {student.FirstName}, {student.LastName}, {student.Personnummer},{student.Classname}");

            Console.WriteLine("Vill du ha specifik info kring en student? Ange ID nummer annars tryck enter");
            int.TryParse(Console.ReadLine(), out int studentinfo);
            if (studentinfo == 0)
                return;
            StudentInfo(context, studentinfo);
            StudentGrades(context, studentinfo);
            

            
        }
        public static void StudentInfo(labb2Context context,int studentinfo) //searches by studentID
        {
            var detailedInfo = context.Students
                .Where(s => s.StudentId == studentinfo)
                .GroupJoin(context.ActiveEnrollments,
                 student => student.StudentId,
                 ae => ae.FkStudentId,
                 (student, activeEnrollments) => new { student, activeEnrollments })
                .Select(temp => new
                {
                    Name = temp.student.FirstName + " " + temp.student.LastName,
                    temp.student.Personnummer,
                    Classname = temp.student.FkClass.ClassName,
                    Average = temp.student.Grades.Any() ? temp.student.Grades.Average(g => g.GradeNr) : 0, //If no grades set 0
                    TotalCourses = temp.activeEnrollments.Count(),
                    ActiveEnrollments = temp.activeEnrollments //creates activeenrollement data + joining course subject
                    .Join(context.Courses,
                    ae => ae.FkCourseId,
                    course => course.CourseId,
                    (ae, course) => new
                        {
                            ae.ActiveCourseId,
                            ae.CourseStartDate,
                            ae.CourseEndDate,
                            ae.FkCourseId,
                            CourseSubjects = course.Subjects 
                        })
                        .ToList()
                })
                .ToList();
           
            foreach (var item in detailedInfo)
            {
                Console.WriteLine($"{item.Name}, {item.Personnummer}, {item.Classname}," +
                $"GPA:{item.Average}, Är nuvarande inskriven i  : {item.TotalCourses} Antal kurser");
                for (int i = 0; i < item.TotalCourses; i++)
                {
                    Console.WriteLine("Aktiv Kurs: " + item.ActiveEnrollments[i].CourseSubjects); 
                }
            }
            
        }

        public static void EmployeePerDepartment(labb2Context context)
        {
            var employelist = context.Employees
                .GroupBy(o => o.Department)
                .Select(o => new
                {
                    Department = o.Key,
                    EmployeeperDepartment = o.Count()
                })
                .ToList();
            foreach(var employe in employelist)
            {
                Console.WriteLine($"PÅ Avdelning: {employe.Department} jobbar : {employe.EmployeeperDepartment} personer");
            }
        }
        public static void Employees(labb2Context context)
        {
            Console.WriteLine("\n[1]Skriv ut alla anställda");
            Console.WriteLine("[2]Skriv ut endast lärare");
            int.TryParse(Console.ReadLine(), out int ans2);
            if (ans2 == 1)
            {
                var employees = context.Employees
                    .Select(o => new { o.FirstName, o.LastName, o.Department, o.StartDate })
                    .ToList();
                Console.WriteLine("All employees");
                foreach (var employee in employees)
                {                  
                    if(employee.StartDate.Year == DateTime.Now.Year)
                    {
                        Console.WriteLine($"Namn: {employee.FirstName} {employee.LastName} Avdelning: {employee.Department} Antal jobbade år: 0");
                    }
                    else
                        Console.WriteLine($"Namn: {employee.FirstName} {employee.LastName} Avdelning: {employee.Department} Antal jobbade år: {DateTime.Now.AddYears(-employee.StartDate.Year).Year}");
                }
                Console.WriteLine();
                EmployeePerDepartment(context);
                Console.WriteLine();
            }
            else if (ans2 == 2)
            {
                var employees = context.Employees
                    .Where(o => o.Department == "teacher")
                    .Select(o => new { o.FirstName, o.LastName, o.StartDate })
                    .ToList();
                Console.WriteLine("All teachers: ");
                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.FirstName}, {employee.LastName}, Antal jobbade år: {DateTime.Now.AddYears(-employee.StartDate.Year).Year}");
                    Console.WriteLine($"");
                }
            }
           
        }
    }
}
