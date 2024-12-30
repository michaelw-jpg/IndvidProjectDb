using Lab3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class UI
    {
        public static void StudentMenu(labb2Context context)
        {
            bool meny = true;
            while (meny)
            {
                Console.WriteLine("\n[1] skriv ut Elever");
                Console.WriteLine("[2] Sök på Elev");
                Console.WriteLine("[3] Återgå");
                ConsoleKeyInfo keyinfo = Console.ReadKey();
                ConsoleKey key = keyinfo.Key;
                switch (key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        Search.StudentSearch(context);
                        Console.WriteLine("tryck Enter för att återgå till huvudmenyn");
                        Console.ReadLine();
                        meny = false;
                        break;
                    case ConsoleKey.D2:
                        Console.Clear();
                        Console.WriteLine("Ange ID:");
                        int.TryParse(Console.ReadLine(), out int studentID);
                        Search.StudentInfo(context, studentID);
                        Console.WriteLine("Studentens betyg:");
                        Search.StudentGrades(context, studentID);
                        Console.WriteLine("tryck Enter för att återgå till huvudmenyn");
                        Console.ReadLine();
                        
                        meny = false;
                        break;
                    case ConsoleKey.D3:
                        Console.Clear();
                        meny = false;
                        break;

                }
            }
        }

        public static void EmployeeInfo(labb2Context context)
        {
            bool meny = true;
            while (meny)
            {
                Console.WriteLine("\n[1] skriv ut anställda");
                Console.WriteLine("[2] Skriv ut lön per avdelning");
                Console.WriteLine("[3] Medellönen på de olika avdelningarna");
                ConsoleKeyInfo keyinfo = Console.ReadKey();
                ConsoleKey key = keyinfo.Key;
                switch(key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        Search.Employees(context);
                        meny = false;
                        break;
                    case ConsoleKey.D2:
                        Console.Clear();
                        Search.SalaryPerDepartment(context);
                        meny = false;
                        break;
                    case ConsoleKey.D3:
                        Console.Clear();
                        Search.AverageSalaryDepartment(context);
                        meny = false;
                        break;

                }
            }
        }
        public static void AddNewMenu(labb2Context context)
        {
            bool meny = true;
            while (meny)
            {
                Console.WriteLine("[1] Lägg till en Elev");
                Console.WriteLine("[2] Lägg till en ny anställd");
                Console.WriteLine("[3] Lägg till ett nytt betyg");
                ConsoleKeyInfo keyinfo = Console.ReadKey();
                ConsoleKey key = keyinfo.Key;
                switch(key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        DbChange.AddStudent(context);
                        Console.WriteLine("Studenten har lagts till! Tryck valfri knapp för att återgå");
                        Console.ReadKey();
                        meny = false;
                        break;
                    case ConsoleKey.D2:
                        Console.Clear();
                        DbChange.AddEmployee(context);
                        Console.WriteLine("Ny anställd har lagts till! Tryck valfri knapp för att återgå");
                        Console.ReadKey();
                        meny = false;
                        break;
                    case ConsoleKey.D3:
                        int courseID = Search.CourseChoice(context);
                        int studentID = DbChange.WhichStudentToGrade(context);
                        DbChange.GetGradeSetter(context, studentID, courseID);
                        Console.ReadKey();
                        meny = false;
                        break;
                    case ConsoleKey.D4:
                        meny = false;
                        break;
                    default:
                        Console.WriteLine("Ange en siffra 1-4");
                        break;
                }
            }
        }
        public static void MainMenu(labb2Context context)
        {
            bool meny = true;
            while (meny)
            {
                Console.WriteLine("[1] Info om anställda");
                Console.WriteLine("[2] Info om Studenter");
                Console.WriteLine("[3] Hämta alla betyg som satts senaste månaden");
                Console.WriteLine("[4] Hämta snittbetyg på alla kurser");
                Console.WriteLine("[5] Lägg till något nytt");
                Console.WriteLine("[6] Hämta en lista på alla aktiva kurser");
                Console.WriteLine("[7] Avsluta programmet");
                ConsoleKeyInfo keyinfo = Console.ReadKey();
                ConsoleKey key = keyinfo.Key;
                switch(key)
                {
                    case ConsoleKey.D1:
                        UI.EmployeeInfo(context);
                        break;

                    case ConsoleKey.D2:
                        Console.Clear();
                        UI.StudentMenu(context);
                        break;
                    case ConsoleKey.D3:
                        Console.Clear();
                        GradeSearch.GradesLastMonth(context);
                        Console.WriteLine("Tryck enter för att återgå!");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D4:
                        Console.Clear();
                        GradeSearch.AverageAll(context);
                        Console.WriteLine("Tryck enter för att återgå!");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D5:
                        Console.WriteLine();
                        UI.AddNewMenu(context);
                        break;
                   
                    case ConsoleKey.D6:
                        GradeSearch.ActiveCourses(context);
                        break;
                    case ConsoleKey.D7:
                        meny = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("\nAnge en siffra mellan 1-7");
                        break;
                }
            }
        }
    }
}
