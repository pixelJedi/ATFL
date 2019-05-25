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
            R = new Reporter("testmachine1.txt",@"D:\Test\",".txt");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}