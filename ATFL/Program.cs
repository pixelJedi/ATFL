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
            string testring;
            do
            {
                Console.WriteLine("Введите новые правила");
                testring = Console.ReadLine();

                if (testring != "q") testmachine = new SM(testring);
            } while (testring != "q");
        }
    }
}
