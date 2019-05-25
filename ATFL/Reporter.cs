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
        public Step Steps;
        public ReportEventArgs(string text,char flag='u') { Steps = new Step(flag, text); }
        public ReportEventArgs(char flag) { Steps = new Step(flag); }
        public ReportEventArgs(Step step) { Steps = step; }
    }
    /// <summary>
    /// Интерфейс для обозначения выводимых в лог классов
    /// </summary>
    interface IShowable
    {
        event ReportEventHandler Report;
        string Show(char mode);
    }
    /// <summary>
    /// Класс Reporter. Управляет запуском и записью процесса решения задач
    /// </summary>
    public class Reporter
    {
        public string Source { get; set; }
        public string DestDir {get;set;} = @"D:\Test\";     /// Каталог для сохранения сгенерированных решений задачи
        public string Extension { get;set; } = ".txt";      /// Расширение для сохранения сгенерированных решений задачи
        public int SubQueueCount { get; private set; } = 1; /// Внутренний порядковый номер генерируемого файла
        private StreamWriter Log { get; set; }              /// Основной поток записи поступивших данных
        public Queue<Step> Q { get; set; }                  /// Очередь для пошагового сохранения поступивших данных

        /// <summary>
        /// Инициализация составитель отчётов с указанием корневого каталога.
        /// </summary>
        /// <param name="DestinationDirectory">Корневой каталог для сохранения отчетов.</param>
        /// <param name="TaskName">Корневой каталог для сохранения отчетов.</param>
        public Reporter(string Source, string DestinationDirectory, string Extension)
        {
            this.Source = Source;
            this.DestDir = DestinationDirectory;
            this.Extension = Extension;
            this.Q = new Queue<Step>();
        }
        /// <summary>
        ///  Инициализирует составитель отчётов по умолчанию: путь сохранения D:\Test\, расширение .txt 
        /// </summary>
        public Reporter() { }

        public string MakeReport(string name, Function operation, string userInput)
        {
            string CurrentFile = DestDir + name + SubQueueCount++ + Extension;
            if (userInput != null && userInput != "q")
            {
                using (Log = new StreamWriter(CurrentFile))
                {
                    CompleteLog(this, new ReportEventArgs(userInput,'i'));
                    if (!operation(userInput))
                    {
                        File.Delete(CurrentFile);
                        return "Операция " + name + " неприменима";
                    }
                    CompleteLog(this, new ReportEventArgs('e'));
                    SubQueueCount += 1;
                }
            }
            return "Успешно. Записанный файл хранится по адресу " + CurrentFile;
        }

        internal void Clear()
        {
            Q.Clear();
            SubQueueCount = 0;
        }

        public bool PassToNextInput()
        {
            Step temp;
            do temp = Q.Dequeue(); while (!(Q.Count == 0 || temp.flag == 'e'));
            return Q.Count != 0;
        }
        public void CompleteLog(object sender, ReportEventArgs e)
        {
            Log.WriteLine(e.Steps.message);
            CompleteStages(this, new ReportEventArgs(e.Steps));
        }
        public void CompleteStages(object sender, ReportEventArgs e)
        {
            Q.Enqueue(e.Steps);
        }
        public bool GetNextStep(out string Step)
        {
            Step = "";
            if (Q.Count > 0)
            {
                if (Q.Peek().flag == 'e') return false;
                var temp = Q.Dequeue();
                switch (temp.flag)
                {
                    case 'i': // input
                        Step += "-----------------Ввод данных---------------\n" + temp.message;
                        break;
                    case 's': // start
                        Step += "-----------------Стартовая конфигурация----\n" + temp.message;
                        break;
                    case 't': // title
                        Step += "-----------------" + temp.message + "----\n";
                        break;
                    case 'r': // result
                        Step += "-----------------Конечная конфигурация----\n" + temp.message;
                        break;
                    // Новые флаги добавляются здесь
                    default:  // usual
                        Step += temp.message + '\n';
                        break;
                }
            }
            return true;
        }
        public int StepsLeft { get { return Q.TakeWhile(s => s.flag != 'e').Count(); } }
        public string SourceFileName { get { return Source; } }
    }
    public class Step
    {
        public char flag;
        public string message;

        public Step(char f) { flag = f; }
        public Step(char f, string m) { flag = f; message = m; }
    }
}
