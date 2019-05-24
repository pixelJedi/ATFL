using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ATFL
{
    /// <summary>
    /// Стандартизированный делегат для управления обработкой события Report
    /// </summary>
    public delegate void ReportEventHandler(object sender, ReportEventArgs e);
    /// <summary>
    /// Служебный класс для броадкастинга сообщений в лог
    /// </summary>
    public class ReportEventArgs : EventArgs
    {
        public string message;
        public ReportEventArgs(string text)
        {
            message = text;
        }
    }
    /// <summary>
    /// Интерфейс для обозначения выводимых в лог классов
    /// </summary>
    interface IShowable
    {
        event ReportEventHandler Report;
        void Show(char mode);
    }
    /// <summary>
    /// Класс Reporter. Управляет запуском и записью процесса решения задач
    /// </summary>
    public class Reporter
    {
        public string DestDir { get;  } = @"D:\Test\";  /// Каталог для сохранения сгенерированных решений задачи
        public string Extension { get; } = ".txt";      /// Расширение для сохранения сгенерированных решений задачи
        private int Number = 1;                         /// Внутренний порядковый номер генерируемого файла
        private StreamWriter Log { get; set; }          /// Основной поток записи поступивших данных
        public Queue<string> Q { get; }                 /// Очередь для пошагового сохранения поступивших данных


        /// <summary>
        /// Инициализация составитель отчётов с указанием корневого каталога.
        /// </summary>
        /// <param name="DestinationDirectory">Корневой каталог для сохранения отчетов.</param>
        /// <param name="TaskName">Корневой каталог для сохранения отчетов.</param>
        public Reporter(string DestinationDirectory, string Extension)
        {
            this.DestDir = DestinationDirectory;
            this.Extension = Extension;
            this.Q = new Queue<string>();
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
            CompleteStages(this, new ReportEventArgs(e.message + "\n"));
        }
        public void CompleteStages(object sender, ReportEventArgs e)
        {
            Q.Enqueue(e.message);
        }
    }
}
