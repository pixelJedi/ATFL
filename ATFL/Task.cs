namespace ATFL
{
    static class Task
    {
        public static bool MakeDFAFromNDFA(string input)
        {
            StateMachine SM = new StateMachine(input);
            StateMachine DFM;
            SM.Report += Program.R.CompleteLog;
            Program.R.CompleteLog(Program.R, new ReportEventArgs("------------------Ввод данных-------------------------------------\n" + input));
            Program.R.CompleteLog(Program.R, new ReportEventArgs("------------------Распознана конфигурация-------------------------"));
            SM.Show('t');
            Program.R.CompleteLog(Program.R, new ReportEventArgs("------------------Делаем ДКА--------------------------------------"));
            DFM = SM.DFMFromNFM();
            DFM.Report += Program.R.CompleteLog;
            Program.R.CompleteLog(Program.R, new ReportEventArgs("------------------После преобразований----------------------------"));
            DFM.Show('t');
            return true;
        }
    }
}
