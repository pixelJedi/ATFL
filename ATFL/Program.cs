using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ATFL
{
    class Program
    {
        static void Main(string[] args)
        {
            SM testmachine;
            string testring = "";
            //Console.WriteLine("Введите алфавит");
            //testalpha = Console.ReadLine();
            Console.WriteLine("Введите новые правила");
            using (StreamReader fs = new StreamReader(@"testmachine.txt"))
            {
                while (true)
                {
                    testring = fs.ReadLine();
                    if (testring == null || testring == "q") break;
                    testmachine = new SM(testring);
                }
            }
        }
    }
}
