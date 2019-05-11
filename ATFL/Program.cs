using System;
using System.IO;

namespace ATFL
{
    class Program
    {
        public static Reporter R;
        static void Main(string[] args)
        {
            string DestDir = @"D:\Test\";
            string ExtensionName = ".txt";
            string Source = "testmachine1";
            R = new Reporter(DestDir,ExtensionName);
            string testring = "s1: a -> s1, s1: a -> s2, s2: b -> s3, s3: a -> s1 | s1 s3";
            using (StreamReader fs = new StreamReader(Source + ExtensionName))
            {
                while (true)
                {
                    testring = fs.ReadLine();
                    if (testring == null || testring == "q") break;
                    string TaskName = "MakeDFA";
                    R.MakeReport(TaskName, Task.MakeDFAFromNDFA, testring);
                }
            }
        }
    }
}