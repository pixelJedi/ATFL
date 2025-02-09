﻿using System.Drawing;
using System.Collections.Generic;

namespace ATFL
{
    public static class Tasks
    {
        public static Task [] Operation;
        static Tasks()
        {
            Operation = new Task[]
            {
                new Task(
                "Детерминизация КА",
                "s1: a -> s1, s1: b -> s1, s1: a -> s2, ... |  s1 s2",
                "Последовательно рассматриваются все переходы и, при необходимости, образуются эквивалентные состояния с единственным переходом по символу",
                MakeDFAFromNDFA
                ),
                new Task(
                "Построение КА по грамматике",
                "s1: a -> s1, s1: b -> s1, s1: a -> s2, ... ",
                "Построение КА по грамматике",
                MakeAutomataFromGrammar
                ),
                new Task(
                "Построение грамматики по КА",
                "s1: a -> s1, s1: b -> s1, s1: a -> s2, ... ",
                "Построение грамматики по КА",
                MakeGrammarFromAutomata
                )
                // Новые задачи записывать здесь
            };
        }
        public static bool MakeDFAFromNDFA(string input)
        {
            StateMachine SM = new StateMachine(input);
            StateMachine DFM;
            Program.R.CompleteLog(Program.R, new ReportEventArgs(SM.Show('t'), 's'));
            DFM = SM.DFMFromNFM();
            Program.R.CompleteLog(Program.R, new ReportEventArgs(DFM.Show('t'), 'r'));
            return true;
        }
        public static bool MakeAutomataFromGrammar(string input)
        {
            Grammar G = new Grammar(input);
            StateMachine SM;
            Program.R.CompleteLog(Program.R, new ReportEventArgs("------------------Ввод данных---------------\n" + input));
            Program.R.CompleteLog(Program.R, new ReportEventArgs("------------------Распознана конфигурация---"));
            G.Show('t');
            Program.R.CompleteLog(Program.R, new ReportEventArgs("------------------Делаем КА-----------------"));
            SM = new StateMachine(G);
            Program.R.CompleteLog(Program.R, new ReportEventArgs("------------------После преобразований------"));
            SM.Show('t');
            return true;
        }
        public static bool MakeGrammarFromAutomata(string input)
        {
            StateMachine SM = new StateMachine(input);
            Grammar G;
            Program.R.CompleteLog(Program.R, new ReportEventArgs("------------------Ввод данных---------------\n" + input));
            Program.R.CompleteLog(Program.R, new ReportEventArgs("------------------Распознана конфигурация---"));
            SM.Show('t');
            Program.R.CompleteLog(Program.R, new ReportEventArgs("------------------Делаем грамматику---------"));
            G = new Grammar(SM);
            Program.R.CompleteLog(Program.R, new ReportEventArgs("------------------После преобразований------"));
            G.Show('t');
            return true;
        }
    }
    public delegate bool Function(string input);
    public class Task
    {
        public string Name { get; }
        public string Format { get; set; }
        public string Description { get; }
        public Function Operation { get; }
        public Task(string N, string F, string D, Function O)
        {
            Name  = N;
            Format = F;
            Description = D;
            Operation   = O;
        }
    }
}
