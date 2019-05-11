using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ATFL
{
    public delegate void ReportEventHandler(object sender, ReportEventArgs e);
    public class ReportEventArgs : EventArgs
    {
        public string message;
        public ReportEventArgs(string text)
        {
            message = text;
        }
    }
    interface IShowable
    {
        event ReportEventHandler Report;
        void Show(char mode);
    }

    public class Reporter
    {
        private readonly string DestDir = @"D:\Test\";
        private readonly string Extension = ".txt";
        private int Number = 1;
        private StreamWriter Log { get; set; }

        public delegate bool Function(string input);

        /// <summary>
        /// Инициализация составитель отчётов с указанием корневого каталога.
        /// </summary>
        /// <param name="DestinationDirectory">Корневой каталог для сохранения отчетов.</param>
        /// <param name="TaskName">Корневой каталог для сохранения отчетов.</param>
        public Reporter(string DestinationDirectory, string Extension)
        {
            this.DestDir = DestinationDirectory;
            this.Extension = Extension;
        }
        /// <summary>
        ///  Инициализирует составитель отчётов по умолчанию: путь сохранения D:\Test\, расширение .txt 
        /// </summary>
        public Reporter() { }

        public string MakeReport(string name, Function operation, string userInput)
        {
            string CurrentFile = DestDir + name + Number++ + Extension;
            if (userInput != null && userInput != "q")
            {
                using (Log = new StreamWriter(CurrentFile))
                {

                    if (!operation(userInput))
                    {
                        File.Delete(CurrentFile);
                        return "Операция " + name + " неприменима";
                    }
                }
            }
            return "Успешно. Записанный файл хранится по адресу " + CurrentFile;
        }
        public void CompleteLog(object sender, ReportEventArgs e)
        {
            Log.WriteLine(e.message);
        }
    }
}
