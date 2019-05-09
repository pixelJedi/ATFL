using System;
using System.IO;

namespace ATFL
{
    class Program
    {
        static void Main(string[] args)
        {
            string testring = "";
            using (StreamReader fs = new StreamReader(@"testmachine1.txt"))
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
                            Console.WriteLine("--------Ввод данных----------------------------------------");
                            Console.WriteLine(testring);
                            Console.WriteLine("--------Распознана конфигурация----------------------------");
                            StateMachine NFM = new StateMachine(testring);
                            NFM.Show('t');
                            Console.WriteLine("--------Грамматика------------------------------");
                            Grammar gr = new Grammar(NFM);
                            gr.Show();
                            Console.WriteLine("--------Преобразование в ДКА-------------------------------");
                            StateMachine DFM = NFM.DFMFromNFM();
                            Console.WriteLine("--------Итоговая конфигурация------------------------------");
                            DFM.Show('t');
                            Console.WriteLine("--------Грамматика------------------------------");
                            Grammar gr1 = new Grammar(DFM);
                            gr.Show();
                        }
                    }
                }
            }
        }
    }
}