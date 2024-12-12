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
        public static void MainMenu(labb2Context context)
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
                Console.WriteLine("[7] Avsluta programmet");
                ConsoleKeyInfo keyinfo = Console.ReadKey();
                ConsoleKey key = keyinfo.Key;
                switch(key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        Search.Employees(context);
                        break;

                    case ConsoleKey.D2:
                        Console.Clear();
                        Search.StudentSearch(context);
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
                        Console.Clear();
                        DbChange.AddStudent(context);
                        Console.WriteLine("Studenten har lagts till! Tryck valfri knapp för att återgå");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D6:
                        Console.Clear();
                        DbChange.AddEmployee(context);
                        Console.WriteLine("Ny anställd har lagts till! Tryck valfri knapp för att återgå");
                        Console.ReadKey();
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
