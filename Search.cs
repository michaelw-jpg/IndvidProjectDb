using Lab3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Search
    {

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

        static void GetStudents(labb2Context context, int asc, int firstOrLast) //gets student from database
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
            else if (asc == 0 && firstOrLast == 1)
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
                                   .Select(s => new { s.FirstName, s.LastName, s.Personnummer, Classname = s.FkClass.ClassName })
                                   .ToList();
            Console.WriteLine("studenter:");

            foreach (var student in students)
                Console.WriteLine($"{student.FirstName}, {student.LastName}, {student.Personnummer},{student.Classname}");

            
        }
        public static void Employees(labb2Context context)
        {
            Console.WriteLine("Skriv ut alla anställda");
            Console.WriteLine("Skriv ut endast lärare");
            int.TryParse(Console.ReadLine(), out int ans2);
            if (ans2 == 1)
            {
                var employees = context.Employees
                    .Select(o => new { o.FirstName, o.LastName, o.Department })
                    .ToList();
                Console.WriteLine("All employees");
                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.FirstName}, {employee.LastName}, {employee.Department}");
                }
            }
            else if (ans2 == 2)
            {
                var employees = context.Employees
                    .Where(o => o.Department == "teacher")
                    .Select(o => new { o.FirstName, o.LastName })
                    .ToList();
                Console.WriteLine("All teachers: ");
                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.FirstName}, {employee.LastName}");
                    Console.WriteLine($"");
                }
            }
        }
    }
}
