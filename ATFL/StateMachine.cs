using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ATFL
{
    class StateMachine
    {
        public string Name { get; set; } = "ATFL";
        private List<TransRule> StateTable;         /// Таблица переходов

        public List<string> FinalState;             /// Множество конечных состояний
        public string Alphabet { get; set; }
        public string CurrentState { get; set; }    /// Текущее состояние
        public string StartState { get; set; }      /// Начальное состояние

        public StateMachine(string TransitionRules)
        {
            StateTable = new List<TransRule>();
            FinalState = new List<string>();
            Alphabet = "";
            if (ParseOK(TransitionRules))
            {
                SortStateTable();
                CurrentState = StartState;
                Alphabet = GetRealAlphabet();
            }
            else
                Console.WriteLine("Не удалось заполнить таблицу переходов");
        }
        public StateMachine(string Alphabet, List<TransRule> StateTable, string StartState, List<string> FinalState)
        {
            this.StartState = StartState;
            this.FinalState = FinalState;
            this.Alphabet = Alphabet;
            this.StateTable = StateTable;
        }
        public StateMachine(string Alphabet, string TransitionRules)
        {
            StateTable = new List<TransRule>();
            this.Alphabet = Alphabet;
            if (ParseOK(TransitionRules))
                CurrentState = StartState;
            else
                Console.WriteLine("Не удалось заполнить таблицу переходов");
        }
        public StateMachine(Grammar G)
        {
            int count = 0;                                // Нумерация этапов
            StateTable = new List<TransRule>();
            FinalState = new List<string>();
            Alphabet = G.T;
            Console.WriteLine($"Шаг {++count}. В алфавит войдут все терминалы грамматики: ∑ = {Alphabet}");
            StartState = G.S.ToString();
            Console.WriteLine($"Шаг {++count}. Стартовое состояние соответствует стартовому состоянию грамматики: S = {StartState}");
            string newNT = G.GetNextNT().ToString();  // Специальное новое допускающее состояние
            Console.WriteLine($"\nШаг {++count}. Добавим специальное новое допускающее состояние: {newNT}");

            Console.WriteLine("Начнем заполнять таблицу переходов.");
            int subcount = 0;
            foreach (var p in G.P)
            {
                Console.WriteLine($"\nШаг {count}.{++subcount}. Рассмотрим продукцию {p.Display()}");
                if (p.Right.Count() == 2)
                {
                    StateTable.Add(new TransRule(p.Left.ToString(), p.Right[0], p.Right[1].ToString()));
                    Console.WriteLine($"Продукция вида A -> aB. Добавим правило δ({p.Left},{p.Right[0]}) = {p.Right[1]}.");
                    if (G.HasPair(p) && !FinalState.Contains($"{p.Right[1]}"))
                    {
                        Console.WriteLine($"{p.Right[1]} - завершающая вершина, т.к. существует парный переход из {p.Left} по тому же символу {p.Right[0]}.");
                        FinalState.Add(p.Right[1].ToString());
                    }
                }
                else
                {
                    Console.Write("Продукция вида A -> a. ");
                    if (!G.HasPair(p))
                    {
                        Console.WriteLine($"Не существует правила вида A -> aB. Поэтому добавим переход в допускающее состояние: δ({p.Left},{p.Right[0]}) = {newNT}.");
                        StateTable.Add(new TransRule(p.Left.ToString(), p.Right[0], newNT.ToString()));
                    }
                    else Console.WriteLine($"Существует парное правило вида A -> aB. Поэтому добавлять переход в таблицу не нужно.");
                }
            }
            if (!FinalState.Contains(StartState) && G.HasRule(G.S, "ε"))
            {
                Console.Write($"Шаг {++count}. Существует переход из стартовой вершины {StartState} в ε. {StartState} является заключительным состоянием");
                FinalState.Add(StartState);
            }
            if (!FinalState.Contains(newNT) && ExistsInTable(newNT))
                FinalState.Add(newNT);
            SortStateTable();
        }

        private bool ParseOK(string input)
        {
            bool isOK = true;

            string[] rules = input.Split(new char[] { '|', ',' });
            if (!rules.Contains(""))
            {
                for (int i = 0; i < rules.Length - 1; i++)
                    isOK = isOK && FillStates(rules[i]);
                isOK = isOK && FillStartFin(rules[rules.Length - 1]);
            }
            else
            {
                Console.WriteLine("Неверный синтаксис (лишняя запятая)");
                isOK = false;
            }
            return isOK;
        }

        public string GetRealAlphabet()     // Возваращает перечень взодных символов, задействованных в текущей таблице переходов
        {
            string str = "";
            foreach (TransRule rule in StateTable)
            {
                if (!str.Contains(rule.Symbol)) str += rule.Symbol;
            }
            return str;
        }
        public string[] GetSetOfStates()    // Возвращает массив наименований состояний, задействованных в таблице переходов
        {
            List<string> str = new List<string>();
            foreach (TransRule rule in StateTable)
            {
                if (!str.Contains(rule.Next)) str.Add(rule.Next);
                if (!str.Contains(rule.Current)) str.Add(rule.Current);
            }
            return str.ToArray();
        }

        private bool FillStartFin(string input)
        {

            Regex regex = new Regex(@"\s*\w+\s*(\s+\w+\s*)+");  // Ввод по образцу (s1 s2, ...)
            MatchCollection matches = regex.Matches(input);
            if (matches.Count > 0)
            {
                string[] groups = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (ExistsInTable(groups[0])) StartState = groups[0];
                else
                {
                    Console.WriteLine("Заданное начальное состояние не существует");
                    return false;
                }
                for (int i = 1; i < groups.Length; i++)
                {
                    if (ExistsInTable(groups[i])) FinalState.Add(groups[i]);
                    else
                    {
                        Console.WriteLine($"Заданное состояние {groups[i]} не существует");
                        return false;
                    }
                }
                return true;
            }
            else
            {
                Console.WriteLine("Список состояний не соответствует установленному шаблону");
                return false;
            }
        }
        private bool ExistsInTable(string state)
        {
            bool exists = false;
            foreach (TransRule rule in StateTable)
                if (rule.Current == state || rule.Next == state)
                {
                    exists = true;
                    break;
                }
            return exists;
        }
        private bool ExistsInTable(List<TransRule> stateTable, string state, char c = 'c')
        {
            bool exists = false;
            foreach (TransRule rule in stateTable)
                if (c == 'c' && rule.Current == state || c == 'n' && rule.Next == state)
                {
                    exists = true;
                    break;
                }
            return exists;
        }
        private bool FillStates(string input)
        {   
            Regex regex = new Regex(@"\s*\w+\s*:\s*(\w|\?)\s*->\s*\w+\s*");  // Ввод по образцу [current]: [symbol] -> [next]
            MatchCollection matches = regex.Matches(input);
            if (matches.Count == 1)
            {
                string[] groups = input.Split(new char[] { ' ', ':', '-', '>' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < groups.Length; i = i + 3)
                {
                    if (Alphabet == "" || Alphabet.Contains(groups[i + 1][0]))
                        StateTable.Add(new TransRule(groups[i], groups[i + 1][0], groups[i + 2]));
                    else
                    {
                        Console.WriteLine($"Символ {groups[i + 1][0]} не включен во входной алфавит");
                        return false;
                    }
                }
                return true;
            }
            else
            {
                Console.WriteLine("Правило не соответствует установленному шаблону");
                return false;
            }
        }

        private void SortStateTable()
        {
            RuleComparer rc = new RuleComparer();
            StateTable.Sort(rc);
        }

        public StateMachine DFMFromNFM()
        {
            List<TransRule> newStateTable = new List<TransRule>(); /// Новая (пустая) таблица состояний
            List<string> newFinalState = new List<string>();       /// Новый (пустой) список конечных состояний
            Queue<List<string>> Q = new Queue<List<string>>();     /// Очередь для последовательного разбора множеств состояний

            int count = 0;                                         /// Нумерация этапов
            Console.WriteLine("Q - очередь разбора связей.");
            Console.WriteLine($"Изначально в Q находится стартовое состояние {{{StartState}}}.");
            Q.Enqueue(new List<string> { StartState });            // Стартовая вершина включена в список
            while (Q.Count != 0)                                   // Пока не будут разобраны все связи
            {
                List<string> set = Q.Dequeue();                    // 1. Вытаскиваем множество состояний из очереди
                Console.WriteLine($"\nШаг {++count}. Вынимаем из Q множество {{{string.Join(",", set)}}}.");
                foreach (char c in Alphabet)                       // 2. Для каждой буквы находим next states этого множества
                {
                    bool isFinal = FindNextStatesForSet(set, c, out List<string> nextStates);
                    string newStateFromSet = string.Join(null, nextStates);
                    if (nextStates.Count != 0)
                    {
                        Console.Write($"\nШаг {count}.{c} ");
                        if (nextStates.Count > 1) Console.WriteLine($"Недетерминированность по символу {c}. Объединяем результаты перехода в множество:");
                        Console.WriteLine($"{string.Join(null, set)} : {c} -> {newStateFromSet}");
                        Console.WriteLine("Новое правило перехода вносится в таблицу.");
                        if (isFinal && !ExistsInTable(newStateTable, newStateFromSet, 'n'))
                        {
                            Console.WriteLine($"Состояние {newStateFromSet} - завершающее, т.к. среди исходных было завершающее.");
                            newFinalState.Add(newStateFromSet);
                        }
                        newStateTable.Add(new TransRule(string.Join(null, set), c, newStateFromSet));
                        if (!(ExistsInTable(newStateTable, newStateFromSet) || newStateFromSet == StartState || WasInQ(Q, nextStates)))
                        {
                            
                            Console.WriteLine($"Cостояние {newStateFromSet} еще не рассмотрено. Помещаем его в Q.");
                            Q.Enqueue(nextStates);
                        }
                        else
                            Console.WriteLine($"Cостояние {newStateFromSet} уже рассмотрено. Переходим дальше.");
                    }
                }
            }
            Console.WriteLine("Q пуста => все пути из начальной вершины рассмотрены.");
            StateMachine newSM = new StateMachine(Alphabet, newStateTable, StartState, newFinalState);
            newSM.SortStateTable();
            return newSM;
        }

        private bool WasInQ(Queue<List<string>> Q, List<string> nextStates)
        {
            bool wasInQ = false;
            foreach (var q in Q)
                if (q.Except(nextStates).Count() == 0 && nextStates.Except(q).Count() == 0) { wasInQ = true; break; }
            return wasInQ;
        }

        public bool FindNextStatesForSet(List<string> set, char sym, out List<string> nextStates)
        {
            nextStates = new List<string>();
            foreach (string state in set)
                nextStates = nextStates.Union(StateTable.FindAll(x => x.Current == state && x.Symbol == sym).Select(x => x.Next)).ToList();
            bool isFin = false;
            foreach (string state in nextStates)
                if (FinalState.Contains(state)) isFin = true;
            return isFin;
        }
        public bool FindNextStates(string curr, char sym, out string[] result)
        {
            result = StateTable.FindAll(x => x.Current == curr && x.Symbol == sym).Select(x => x.Next).ToArray();
            return result.Count() > 0;
        }
        public void Show(char mode = 'r')
        {
            const int F = 16;
            Console.WriteLine($"Начальное состояние: {StartState}");
            if (FinalState.Count == 1) Console.Write("Конечное состояние: ");
            else Console.Write("Конечные состояния: ");
            foreach (var t in FinalState) Console.Write($"{t} ");
            Console.WriteLine();
            Console.WriteLine("Таблица переходов: ");
            switch (mode)
            {
                case 't':
                    // Особая первая строка
                    Console.Write($"{' ',-F}");
                    foreach (char c in Alphabet) Console.Write($"{c,-F}");
                    Console.WriteLine();
                    // Остальные строки
                    string [] currNames = GetSetOfStates().ToArray();
                    foreach (string state in currNames)
                    {
                        Console.Write($"{state,-F}");
                        foreach (char c in Alphabet)
                        {
                            var nexts = StateTable.FindAll(x => x.Current == state && x.Symbol == c).Select(x => x.Next);
                            if (nexts.Count() > 0) Console.Write($"{string.Join(",", nexts),-F}");
                            else Console.Write($"{'-',-F}");
                        }
                        Console.WriteLine();
                    }
                    break;
                default:
                    foreach (TransRule rule in StateTable)
                        rule.DisplayRule();
                    break;
            }
        }
    }

    internal class RuleComparer : IComparer<TransRule>
    {
        public int Compare(TransRule o1, TransRule o2)
        {
            int result = String.Compare(o1.Current, o2.Current);
            if (result > 0)
            {
                return 1;
            }
            else if (result < 0)
            {
                return -1;
            }
            else
            {
                if (o1.Symbol > o2.Symbol) return 1;
                else if (o1.Symbol < o2.Symbol) return -1;
                else
                {
                    result = String.Compare(o1.Next, o2.Next);
                    if (result > 0) return 1;
                    else if (result < 0) return -1;
                    else return 0;
                    }
                }
            }
        }
    }

    internal class TransRule
    { 
        public string Current { get; set; }
        public char   Symbol  { get; set; }
        public string Next    { get; set; }
        
        public TransRule(string current, char symbol, string next)
        {
            this.Current = current;
            this.Symbol = symbol;
            this.Next = next;
        }
        public void DisplayRule()
        {
            Console.WriteLine($"{Current}: {Symbol} -> {Next}");
        }
    }

