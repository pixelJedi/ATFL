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
                TextWriter tmp = Console.Out;
                while (true)
                {
                    testring = fs.ReadLine();
                    if (testring == null || testring == "q")
                    {
                        Console.SetOut(tmp);
                        Console.WriteLine("Успешно. Записанные файлы хранятся по адресу "+ DestDir);
                        break;
                    }
                    else
                    {
                        using (StreamWriter log = new StreamWriter(DestDir+"ConvertToDKA" + i++ + ".txt"))
                        {
                            //StreamWriter sw = new StreamWriter(log);
                            Console.SetOut(log);
                            //using (LogWriter log = new LogWriter(@"D:\Test\Automata" + (i++) + ".txt"))
                            {
                                //Console.SetOut(log);
                                Console.WriteLine("----Исходная конфигурация--------------------------------");
                                SM NFM = new SM(testring);
                                NFM.Show();
                                Console.WriteLine("----Преобразование в ДКА---------------------------------");
                                SM DFM = NFM.DFMFromNFM();
                                Console.WriteLine("----Итоговая конфигурация--------------------------------");
                                DFM.Show();
                            }
                        }
                    }
                }
            }
        }
    }
}