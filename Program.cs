using Lab3.Data;
using Lab3.Models;
using Microsoft.EntityFrameworkCore;
namespace Lab3
{
    internal class Program
    {
        
        
        static void Main(string[] args)
        {
           
            using (var context = new labb2Context())
            {
                UI.MainMenu(context); 
                
            }
        }
    }
}
