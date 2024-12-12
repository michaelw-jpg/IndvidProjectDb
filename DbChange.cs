using Lab3.Data;
using Lab3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class DbChange
    {
        public static void AddEmployee(labb2Context context)
        {
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
