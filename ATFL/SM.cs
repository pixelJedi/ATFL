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
        private List <TransRule> StateTable;       /// Таблица переходов
        private string      CurrentState;     /// Текущее состояние
        private string      StartState;       /// Начальное состояние
        private string[]    FinalState;       /// Множество конечных состояний

        public SM(string TransitionRules)
        {
            /* Строка передается в парсер, тот возвращает успех/неуспех и структурированный результат
             * Если успех, записываем
             * Если неуспех, вывод: неверно задана таблица
             */
            StateTable = new List<TransRule>();
            if (!ParseOK(TransitionRules))
            {
                Console.WriteLine("Не удалось заполнить таблицу переходов");
                StartState = "";
                CurrentState = StartState;
                FinalState = new string[] { StartState };
            }
            else foreach (TransRule TR in StateTable)
                {
                    TR.DisplayRule();
                    StartState = StateTable[0].current;
                    CurrentState = StartState;
                    FinalState = new string[] { StartState };

                }
        }

        private bool ParseOK(string input)
        {
            bool isOK = true;

            string[] rules = input.Split(new char[] {'|',','});
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
            Regex regex = new Regex(@"\s*\w+\s*:\s*\w\s*->\s*\w+\s*");  // Ввод по образцу [current]: [symbol] -> [next]
            MatchCollection matches = regex.Matches(input);
            if (matches.Count == 1)
            {
                string[] groups = input.Split(new char[] { ' ', ':', '-', '>' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < groups.Length; i = i + 3)
                    StateTable.Add(new TransRule(groups[i], groups[i + 1][0], groups[i + 2]));
                return true;
            }
            else
            {
                Console.WriteLine("Правило не соответствует установленному шаблону");
                return false;
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
}
