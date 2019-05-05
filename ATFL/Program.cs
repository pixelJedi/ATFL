using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATFL
{
    class Program
    {
        static void Main(string[] args)
        {
            SM testmachine;
            string testring, testalpha;
            do
            {
                Console.WriteLine("Введите алфавит");
                testalpha = Console.ReadLine();
                Console.WriteLine("Введите новые правила");
                testring = Console.ReadLine();

                testmachine = new SM(testalpha, testring);
            } while (testring != "q"|| testalpha != "q");
        }
    }
}
