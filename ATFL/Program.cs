using System;
using System.Windows.Forms;

namespace ATFL
{
    static class Program
    {
        public static Reporter R;
        [STAThread]
        static void Main()
        {
            R = new Reporter(@"D:\Test\",".txt");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
    /*
    class Program
    {
        static void Main(string[] args)
        {
        }
    }*/
}