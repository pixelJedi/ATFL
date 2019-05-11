using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ATFL
{
    /// <summary>
    /// Класс Grammar.
    /// Описывает формальную грамматику четверкой (N, T, P, S)
    /// </summary>
    [Serializable]
    class Grammar : IShowable
    {
        /// <summary>
        /// Имя грамматики. Необязательное поле
        /// </summary>
        public string Name { get; set; } = "ATFL";  
        /// <summary>
        /// Множество терминалов 
        /// </summary>
        public string T { get; set; }
        /// <summary>
        /// Множество нетерминалов
        /// </summary>
        public string N { get; set; }
        /// <summary>
        /// Множество продукций - правил замены вида Left -> Right
        /// </summary>
        public List<GramRule> P { get; }
        /// <summary>
        /// Стартовый символ
        /// </summary>
        public char S { get; set; } = 'S';
        public event ReportEventHandler Report;
        /// <summary>
        /// Инициализирует новый экземпляр класса Grammar по указанным множествам.
        /// </summary>
        /// <param name="T">Множество терминалов</param>
        /// <param name="N">Множество нетерминалов</param>
        /// <param name="P">Множество продукций - правил замены</param>
        /// <param name="S">Стартовое состояние</param>
        public Grammar(string T, string N, List<GramRule> P, char S)
        {
            this.T = T;
            this.N = N;
            this.P = P.GetRange(0,P.Count);
            this.S = S;
        }
        /// <summary>
        /// Инициализирует новый экземпляр класса Grammar по пользовательской строке ввода
        /// </summary>
        /// <param name="GrammarRules">Строка с перечислением правил грамматики через запятую</param>
        public Grammar(string GrammarRules)
        {
            P = new List<GramRule>();
            if (ParseOK(GrammarRules))
            {
                var t = P.Select(x => x.Left).Distinct();
                N = string.Join(null, t);
                T = string.Join(null,GetTFromString(GrammarRules).Except(t));
            }
            else
                Console.WriteLine("Не удалось заполнить таблицу переходов");
        }
        /// <summary>
        /// Инициализирует новый экземпляр класса Grammar из существующего конечного автомата
        /// </summary>
        /// <param name="SM">Конечный автомат</param>
        public Grammar(StateMachine SM)
        {
            Name = SM.Name;
            // 1. Заполняем терминальные символы
            T = SM.Alphabet;
            // 2. Сопоставляем исходные состояния и задаем им нетерминалы
            Dictionary<string, char> Renames = new Dictionary<string, char>();
            var Q = SM.SetOfStates;
            char curName = S;
            foreach (var t in Q)
            {
                curName = GetNewNTName();
                Renames.Add(t, curName);
            }
            S = Renames[SM.StartState];
            // 3. Заполняем правила переходов
            P = new List<GramRule>();
            bool eps = false;
            foreach (var A in Q)
                foreach (var a in T)
                    if (SM.FindNextStates(A, a, out string[] B))
                        foreach (string b in B)
                        {
                            P.Add(new GramRule(Renames[A], $"{a}{Renames[b]}"));
                            if (SM.FinalState.Contains(b))
                            {
                                if (Renames[A] == S) { eps = true; P.Add(new GramRule(Renames[A], "ε")); };
                                P.Add(new GramRule(Renames[A], $"{a}"));
                            }
                        }
            if (eps) T += 'ε';
            Console.WriteLine("Renames:");
            foreach (KeyValuePair <string, char> keyValue in Renames)
                Console.WriteLine(keyValue.Key + " -> " + keyValue.Value);
            // 5. Все итоговые нетерминальные состояния заносим в N
            N = string.Join(null,Renames.Values);
        }
        
        private bool ParseOK(string input)
        {
            // S -> a|b, V -> c
            Regex regex = new Regex(@"(\s*\w\s*->\s*\w+\s*(\|\s*\w+\s*)*){1,}");
            MatchCollection matches = regex.Matches(input);
            if (matches.Count == 0) return false;
            string[] rules = input.Split(',');
            if (rules.Count() == matches.Count && !rules.Contains(""))
            {
                foreach (string rule in rules)
                {
                    // S -> a|b
                    string[] parts = rule.Split(new char[] { '-', '>' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2)
                    {
                        // S
                        char left = parts[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0][0];
                        // a|b
                        string[] rights = parts[1].Split(new char[] { ' ', '|' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var right in rights)
                            // a
                            P.Add(new GramRule(left, right));
                    }
                    else
                    {
                        Console.WriteLine("Неверно составленая продукция " + rule);
                        return false;
                    }
                }

            }
            else
            {
                Console.WriteLine("Правила отсутствуют");
                return false;
            }
            return true;
        }
        private string GetTFromString(string expression)
        {
            string temp = "";
            string acceptable = "+*()->|, ";
            foreach (char c in expression)
            {
                if (char.IsLetterOrDigit(c))
                {
                    if (!temp.Contains(c)) temp += c;
                }
                else if (!acceptable.Contains(c)) throw new FormatException($"Встречен недопустимый символ {c}");
            }
            return temp;
        }
        public bool IncludesRule(char s, string v)
        {
            foreach (var p in P)
                if (p.Left == s && p.Right == v) return true;
            return false;
        }
        public bool HasPair(GramRule rule)
        {
            if (rule.Right.Length == 1)
            {
                foreach (var p in P)
                    if (p.Right.Length == 2 && p.Left == rule.Left && p.Right[0] == rule.Right[0] && p.Left != p.Right[1])
                        return true;
            }
            else
                foreach (var p in P)
                    if (p.Right.Length == 1 && p.Left == rule.Left && p.Right[0] == rule.Right[0]) 
                        return true;
            return false;
        }

        private string NTLeft = "ABCDEFGHIJKLMNOPQRSTUVXYZ";
        private char GetNewNTName()
        {
            char newNTName;
            if (0 == NTLeft.Length) throw new OverflowException("Использованы все свободные латинские нетерминалы");
            else
            {
                newNTName = NTLeft[0];
                NTLeft = NTLeft.Remove(0, 1);
            }
            return newNTName;
        }
        public char GetNextNT()
        {
            char newNTName;
            if (0 == NTLeft.Length) throw new OverflowException("Использованы все свободные латинские нетерминалы");
            else
                newNTName = NTLeft[0];
            return newNTName;
        }

        private void SortStateTable()
        {
            GRuleComparer rc = new GRuleComparer();
            P.Sort(rc);
        }
        public void Show(char mode = 'r')
        {
            if (this.Report != null)
            {
                Report(this, new ReportEventArgs("Грамматика\t" + Name));
                Report(this, new ReportEventArgs("Терминалы:  \t" + T));
                Report(this, new ReportEventArgs("Нетерминалы:\t" + N));
                Report(this, new ReportEventArgs("Старт:\t" + S));
                Report(this, new ReportEventArgs("Продукции:\t"));
                switch (mode)
                {
                    case 't':
                        foreach (char NT in N)
                            Report(this, new ReportEventArgs($"{NT} -> {string.Join(" | ", P.FindAll(x => x.Left == NT).Select(x => x.Right))}"));
                        break;
                    default:
                        foreach (var p in P)
                            Report(this, new ReportEventArgs(p.Display()));
                        break;
                }

            }
        }
    }

    internal class GRuleComparer : IComparer<GramRule>
    {
        public int Compare(GramRule o1, GramRule o2)
        {
            if (o1.Left > o2.Left) return 1;
            else if (o1.Left < o2.Left) return -1;
            else
            {
                int result = String.Compare(o1.Right, o2.Right);
                if (result > 0) return 1;
                else if (result < 0) return -1;
                else return 0;
            }
        }
    }

    class GramRule
    {
        public char Left { get; set; } 
        public string Right { get; set; }

        public GramRule(char Left, string Right)
        {
            this.Left = Left;
            this.Right = Right;
        }
    
        public string Display()
        {
            return $"{Left} = {Right}";
        }
    }
}

