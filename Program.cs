using Lab3.Data;
using Lab3.Models;
using Microsoft.EntityFrameworkCore;
namespace Lab3
{
    internal class Program
    {
        
        static void GetStudents(labb2Context context, int asc = 1, int firstOrLast = 0)
        {
            
            if (asc == 1 && firstOrLast == 1)
            { 
            var students = context.Students
                       .OrderBy(s => s.FirstName)
                       .Join(context.Classes, student => student.FkClassId,
                       cls => cls.ClassId, (student, cls) => new //SELECTING is done inside the join statement with new
                         {
                           student.FirstName,
                           student.LastName,
                           student.Personnummer,
                           cls.ClassName
                          }).ToList();
                foreach (var student in students)
                {
                    Console.WriteLine($"{student.FirstName}, {student.LastName}, {student.Personnummer},{student.ClassName}");
                }
            }
            else if(asc == 0 && firstOrLast == 1)
            {
                var students = context.Students
                           .OrderByDescending(s => s.FirstName)
                           .Join(context.Classes, student => student.FkClassId,
                           cls => cls.ClassId, (student, cls) => new //SELECTING is done inside the join statement with new
                           {
                               student.FirstName,
                               student.LastName,
                               student.Personnummer,
                               cls.ClassName
                           }).ToList();
                foreach (var student in students)
                {
                    Console.WriteLine($"{student.FirstName}, {student.LastName}, {student.Personnummer},{student.ClassName}");
                }
            }
            else if (asc == 0 && firstOrLast == 0)
            {
                var students = context.Students
                           .OrderByDescending(s => s.LastName)
                           .Join(context.Classes, student => student.FkClassId,
                           cls => cls.ClassId, (student, cls) => new //SELECTING is done inside the join statement with new
                           {
                               student.FirstName,
                               student.LastName,
                               student.Personnummer,
                               cls.ClassName
                           }).ToList();
                foreach (var student in students)
                {
                    Console.WriteLine($"{student.FirstName}, {student.LastName}, {student.Personnummer},{student.ClassName}");
                }
            }
            else if (asc == 1 && firstOrLast == 0)
            {
                var students = context.Students
                           .OrderBy(s => s.LastName)
                           .Join(context.Classes, student => student.FkClassId,
                           cls => cls.ClassId, (student, cls) => new //SELECTING is done inside the join statement with new
                           {
                               student.FirstName,
                               student.LastName,
                               student.Personnummer,
                               cls.ClassName
                           }).ToList();
                foreach (var student in students)
                {
                    Console.WriteLine($"{student.FirstName}, {student.LastName}, {student.Personnummer},{student.ClassName}");
                }
            }
        }
        static string chooseClass(labb2Context context)
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
        static void Main(string[] args)
        {
           
            using (var context = new labb2Context())
            {
                bool meny = true;
                while (meny)
                {
                    Console.WriteLine("[1] skriv ut anställda");
                    Console.WriteLine("[2] Skriv ut studenter");
                    Console.WriteLine("[3] Hämta alla betyg som satts senaste månaden");
                    Console.WriteLine("[4] Hämta snittbetyg på alla kurser");
                    Console.WriteLine("[5] Lägg till en Elev");
                    Console.WriteLine("[6] Lägg till en ny anställd");


                    int.TryParse(Console.ReadLine(), out int ans);
                    switch (ans)
                    { 
                        case 1:
                            Console.WriteLine("Skriv ut alla anställda");
                            Console.WriteLine("Skriv ut endast lärare");
                            int.TryParse(Console.ReadLine(), out int ans2);
                            if (ans2 == 1)
                            {
                                var employees = context.Employees
                                    .Select(o => new { o.FirstName, o.LastName, o.Department })
                                    .ToList();
                                Console.WriteLine("All employees");
                                foreach(var employee in employees)
                                {
                                    Console.WriteLine($"{employee.FirstName}, {employee.LastName}, {employee.Department}");
                                }
                            }
                            else if (ans2 == 2)
                            {
                                var employees = context.Employees
                                    .Where(o => o.Department == "teacher")
                                    .Select(o => new { o.FirstName, o.LastName})
                                    .ToList();
                                Console.WriteLine("All teachers: ");
                                foreach (var employee in employees)
                                {
                                    Console.WriteLine($"{employee.FirstName}, {employee.LastName}");
                                    Console.WriteLine($"");
                                }
                            }
                         
                            break;
                        case 2:
                            Console.WriteLine("vill du sortera efter 1: Förnamn, 2: Efternamn");
                            int.TryParse(Console.ReadLine(), out int sName);
                            Console.WriteLine("Vill du sortera Acending/descending, 1/2");
                            int.TryParse(Console.ReadLine(), out int sSort);
                            GetStudents(context,sSort-1, sName-1);
                            string whichClass = chooseClass(context);
                            if (whichClass == "")
                            {
                                break;
                            }
                                var students = context.Students
                                    .Where(s => s.FkClass.ClassName == whichClass)
                                    .OrderBy(s => s.FirstName)
                                    .Select(s => new { s.FirstName, s.LastName, s.Personnummer, Classname = s.FkClass.ClassName})
                                    .ToList();
                                    Console.WriteLine("studenter:");

                                foreach (var student in students)
                                    Console.WriteLine($"{student.FirstName}, {student.LastName}, {student.Personnummer},{student.Classname}");

                        break;

                        case 3:
                            DateTime today = DateTime.Now;
                            DateTime firstDayOfLastMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-1);
                            DateTime lastDayOfLastMonth = firstDayOfLastMonth.AddMonths(1).AddDays(-1);
                            //gets firstday by setting date to 01, and -1 month , then adding one month and removing a day to set last day
                            var gradesLastMonth = context.Grades
                                .Where(g => g.GradeDate >= DateOnly.FromDateTime(firstDayOfLastMonth) &&
                               g.GradeDate <= DateOnly.FromDateTime(lastDayOfLastMonth))
                                .Select(g => new { sFirstName = g.FkStudent.FirstName, sLastName = g.FkStudent.LastName, courseName = g.FkCourse.Subjects,
                                    g.Grade1 })
                                .ToList();
                            foreach(var grade in gradesLastMonth )
                            {
                                Console.WriteLine($"{grade.sFirstName} {grade.sLastName}, {grade.courseName}, {grade.Grade1}");
                            }
                            break;

                        case 4:
                            var grades = context.Grades.GroupBy(g => g.FkCourse)
                                .Select(group => new
                                {
                                    Subject = group.Key,
                                    GradeAvg = group.Average(g => g.GradeNr),
                                    Max =group.Max(g => g.GradeNr),
                                    Min = group.Min(g => g.GradeNr)
                                     
                                }).ToList();
                            foreach(var student in grades)
                            {
                                Console.WriteLine($"Max: {student.Max} Min: {student.Min} AVG:{student.GradeAvg:F2} Ämne: {student.Subject.Subjects}");
                            }
                            break;
                        case 5:
                            Console.WriteLine("Elevens förnamn:");
                            string firstname = Console.ReadLine();
                            Console.WriteLine("Elevens Efternamn:");
                            string lastName = Console.ReadLine();
                            Console.WriteLine("Elevens personnummer?(ÅÅMMDD-XXXX)");
                            string pNummer = Console.ReadLine();
                            Console.WriteLine("I vilken klass ska eleven vara?");
                            var sClass = context.Classes.Select(c => c.ClassName).ToList();
                            int i = 1;
                            foreach(var Eclass in sClass)
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
                            break;
                        case 6:
                            Console.WriteLine("Anställdas Förnamn:");
                            string eFirstname = Console.ReadLine();
                            Console.WriteLine("Anställdas Efternamn:");
                            string eLastname = Console.ReadLine();
                            Console.WriteLine("Vilken avdelning ska den anställda jobba på?");
                            string department = Console.ReadLine();
                            var newEmployee = new Employee()
                            {
                                FirstName = eFirstname,
                                LastName = eLastname,
                                Department = department
                            };
                            context.Employees.Add(newEmployee);
                            context.SaveChanges();
                            break;
                            
                            
                    }
                }
            }
        }
    }
}
