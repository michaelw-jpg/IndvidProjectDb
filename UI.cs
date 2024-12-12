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
        public void MainMenu(labb2Context context)
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
                ConsoleKeyInfo keyinfo = Console.ReadKey();
                ConsoleKey key = keyinfo.Key;
                switch(key)
                {
                    case ConsoleKey.D1:
                        Search.Employees(context);
                        break;

                    case ConsoleKey.D2:
                        Search.StudentSearch(context);
                        break;
                    case ConsoleKey.D3:
                        GradeSearch.GradesLastMonth(context);
                        break;
                    case ConsoleKey.D4:
                        GradeSearch.AverageAll(context);
                        break;
                    case ConsoleKey.D5:
                        DbChange.AddStudent(context);
                        break;
                    case ConsoleKey.D6:
                        DbChange.AddEmployee(context);
                        break;
                }
            }
        }
    }
}
