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
            string testring = "";
            using (StreamReader fs = new StreamReader(@"testmachine.txt"))
            {
                int i = 1;
                string DestDir = @"D:\Test\";
                string TaskName = "ConvertToDKA";
                string ExtensionName = ".txt";
                TextWriter tmp = Console.Out;
                while (true)
                {
                    testring = fs.ReadLine();
                    if (testring == null || testring == "q")
                    {
                        Console.SetOut(tmp);
                        Console.WriteLine($"Успешно. Записанные файлы ({i-1}) хранятся по адресу {DestDir}");
                        break;
                    }
                    else
                    {
                        using (StreamWriter log = new StreamWriter(DestDir + TaskName + i++ + ExtensionName))
                        {
                            Console.SetOut(log);
                            Console.WriteLine("--------Исходная конфигурация------------------------------");
                            SM NFM = new SM(testring);
                            NFM.Show();
                            Console.WriteLine("--------Преобразование в ДКА-------------------------------");
                            SM DFM = NFM.DFMFromNFM();
                            Console.WriteLine("--------Итоговая конфигурация------------------------------");
                            DFM.Show();
                        }
                    }
                }
            }
        }
    }
}