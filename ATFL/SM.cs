using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ATFL
{
    class SM
    {
        private List<TransRule> StateTable;         /// Таблица переходов

        public List<string> FinalState;             /// Множество конечных состояний
        public string Alphabet { get; set; }
        public string CurrentState { get; set; }    /// Текущее состояние
        public string StartState { get; set; }      /// Начальное состояние

        public SM(string TransitionRules)
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
        public SM(string Alphabet, List<TransRule> StateTable, string StartState, List<string> FinalState)
        {
            this.StartState = StartState;
            this.FinalState = FinalState;
            this.Alphabet = Alphabet;
            this.StateTable = StateTable;
        }
        public SM(string Alphabet, string TransitionRules)
        {
            StateTable = new List<TransRule>();
            this.Alphabet = Alphabet;
            if (ParseOK(TransitionRules))
                CurrentState = StartState;
            else
                Console.WriteLine("Не удалось заполнить таблицу переходов");
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

        public SM DFMFromNFM()
        {
            List<TransRule> newStateTable = new List<TransRule>(); /// Новая (пустая) таблица состояний
            List<string> newFinalState = new List<string>();       /// Новый (пустой) список конечных состояний
            Queue<List<string>> P = new Queue<List<string>>();     /// Очередь для последовательного разбора множеств состояний

            int count = 1;                                         /// Для нумерации этапов
            Console.WriteLine($"Шаг {count}: Поместим стартовую вершину {StartState} в очередь");
            P.Enqueue(new List<string> { StartState });            // Стартовая вершина включена в список
            while (P.Count != 0)                                   // Пока не будут разобраны все связи
            {
                List<string> set = P.Dequeue();                    // 1. Вытаскиваем множество состояний из очереди
                Console.WriteLine($"Шаг {++count}. Рассматриваем множество вершин {{{string.Join(",", set)}}}");
                foreach (char c in Alphabet)                       // 2. Для каждой буквы находим next states этого множества
                {
                    bool isFinal = FindNextStatesInSet(set, c, out List<string> nextStates);
                    string newStateFromSet = string.Join(null, nextStates);
                    if (nextStates.Count != 0)
                    {
                        Console.WriteLine($"{string.Join(null, set)} : {c} -> {newStateFromSet}");
                        if (isFinal && !ExistsInTable(newStateTable, newStateFromSet, 'n'))
                        {
                            Console.WriteLine($"Одна из вершин была терминальной -> {newStateFromSet} также терминальна.");
                            newFinalState.Add(newStateFromSet);
                        }
                        newStateTable.Add(new TransRule(string.Join(null, set), c, newStateFromSet));
                        if (!ExistsInTable(newStateTable, newStateFromSet) && newStateFromSet != StartState)
                        {
                            Console.WriteLine($"Вершина {newStateFromSet} еще не рассматривалась. Помещаем ее в очередь.");
                            P.Enqueue(nextStates);
                        }
                        else
                            Console.WriteLine($"Вершина {newStateFromSet} уже была в очереди. Переходим дальше.");
                    }
                }
            }
            Console.WriteLine("Все пути из начальной вершины рассмотрены. Конец алгоритма.");
            SM newSM = new SM(Alphabet, newStateTable, StartState, newFinalState);
            return newSM;
        }

        private bool FindNextStatesInSet(List<string> set, char sym, out List<string> nextStates)
        {
            nextStates = new List<string>();
            foreach (string state in set)
                nextStates = nextStates.Union(StateTable.FindAll(x => x.Current == state && x.Symbol == sym).Select(x => x.Next)).ToList();
            bool isFin = false;
            foreach (string state in nextStates)
                if (FinalState.Contains(state)) isFin = true;
            return isFin;
        }

        public void Show()
        {
            Console.WriteLine($"Начальное состояние: {StartState}");
            if (FinalState.Count == 1) Console.Write("Конечное состояние: ");
            else Console.Write("Конечные состояния: ");
            foreach (var t in FinalState) Console.Write($"{t} ");
            Console.WriteLine();
            Console.WriteLine("Таблица переходов: ");
            foreach (TransRule rule in StateTable)
                rule.DisplayRule();
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
    class TransRule
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

