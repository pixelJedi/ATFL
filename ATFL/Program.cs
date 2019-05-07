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
        static LogWriter log;
        static void Main(string[] args)
        {
            SM testmachine;
            string testring = "";
            using (StreamReader fs = new StreamReader(@"testmachine.txt"))
            {
                int i = 1;
                while (true)
                {
                    log = new LogWriter("D:\\Test\\File"+i+".txt");
                    Console.SetOut(log);
                    Console.SetOut(Console.Out);
                    Console.WriteLine("Вводятся тестовые правила: ");
                    testring = fs.ReadLine();
                    if (testring == null || testring == "q")
                    {
                        Console.WriteLine("Конец программы");
                        break;
                    }
                    Console.WriteLine($"КА {i++} >> {testring}");
                    Console.WriteLine("----Исходная конфигурация--------------------------------");
                    testmachine = new SM(testring);
                    testmachine.Show();
                    Console.WriteLine("----Преобразование в ДКА---------------------------------");
                    SM DFM = testmachine.DFMFromNFM();
                    Console.WriteLine("----Итоговая конфигурация--------------------------------");
                    DFM.Show();
                    log.Dispose();
                }
            }
        }
    }
}
