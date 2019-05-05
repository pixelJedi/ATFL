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

        public string[] FinalState { get; set; }    /// Множество конечных состояний
        public string Alphabet { get; set; }
        public string CurrentState { get; set; }    /// Текущее состояние
        public string StartState { get; set; }      /// Начальное состояние

        public SM(string TransitionRules)
        {
            StateTable = new List<TransRule>();
            Alphabet = "";
            if (ParseOK(TransitionRules))
            {
                foreach (TransRule TR in StateTable) TR.DisplayRule();
                SortStateTable();
                Console.WriteLine("Sorted:");
                foreach (TransRule TR in StateTable) TR.DisplayRule();
                CurrentState = StartState;
                Alphabet = GetRealAlphabet();
            }
            else
                Console.WriteLine("Не удалось заполнить таблицу переходов");
        }
        public SM(string Alphabet, string TransitionRules)
        {
            StateTable = new List<TransRule>();
            this.Alphabet = Alphabet;
            if (ParseOK(TransitionRules))
            {
                foreach (TransRule TR in StateTable) TR.DisplayRule();
                CurrentState = StartState;
            }
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
                if (!str.Contains(rule.symbol)) str += rule.symbol;
            }
            return str;
        }
        public string[] GetSetOfStates()    // Возвращает массив наименований состояний, задействованных в таблице переходов
        {
            List<string> str = new List<string>();
            foreach (TransRule rule in StateTable)
            {
                if (!str.Contains(rule.next)) str.Add(rule.next);
                if (!str.Contains(rule.current)) str.Add(rule.current);
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
                if (existsInTable(groups[0])) StartState = groups[0];
                else
                {
                    Console.WriteLine("Заданное начальное состояние не существует");
                    return false;
                }
                FinalState = new string[groups.Length - 1];
                for (int i = 0; i < FinalState.Length; i++)
                {
                    if (existsInTable(groups[i+1])) FinalState[i] = groups[i + 1];
                    else
                    {
                        Console.WriteLine($"Заданное состояние {groups[i+1]} не существует");
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
        private bool existsInTable(string state)
        {
            bool exists = false;
            foreach (TransRule rule in StateTable)
                if (rule.current == state || rule.next == state)
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


        public SM DSMFromNSM()
        {
            throw new Exception();
        }
    }
    
    class RuleComparer : IComparer<TransRule>
    {
        public int Compare(TransRule o1, TransRule o2)
        {
            int result = String.Compare(o1.current, o2.current);
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
                if (o1.symbol > o2.symbol) return 1;
                else if (o1.symbol < o2.symbol) return -1;
                else
                {
                    result = String.Compare(o1.next, o2.next);
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
        public string current { get; set; }
        public char   symbol  { get; set; }
        public string next    { get; set; }
        
        public TransRule(string current, char symbol, string next)
        {
            this.current = current;
            this.symbol = symbol;
            this.next = next;
        }
        public void DisplayRule()
        {
            Console.WriteLine($"{current}: {symbol} -> {next}");
        }
    }

