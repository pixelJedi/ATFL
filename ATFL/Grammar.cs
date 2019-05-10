using System;
using System.Collections.Generic;
using System.Linq;

namespace ATFL
{
        /// <summary>
        /// Класс Grammar.
        /// Описывает формальную грамматику четверкой (N, T, P, S)
        /// </summary>
    class Grammar
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
        public List<GramRule> P;
        /// <summary>
        /// Стартовый символ
        /// </summary>
        public char S { get; set; } = 'S';
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
            T = GetT(GrammarRules);
            ParseOK(S, GrammarRules);
            N = string.Join(null, P.Distinct().ToList());
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
            var Q = SM.GetSetOfStates();
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

        private List<string> ParseOK(char curr, string input)
        {
            Console.WriteLine($"Получено выражение {input}. Ищем внутренние ветви.");
            if (input.Length == 1)
            {
                if (T.Contains(input[0]))
                {
                    Console.WriteLine($"{input} - одиночный терминал. Ветвь завершена.");
                    return new List<string> { input };
                }
                else
                    throw new ArgumentException($"{input} - ошибочный символ. Разбор прекращен.");
            }
            else
            {
                string[] branches = input.Split('+');
                if (branches.Count() > 1)
                {
                    foreach (string branch in branches)
                    {
                        P.Add(new GramRule(GetNewNTName(), branch));
                        ParseOK(curr, branch);
                    }

                }
                else if (input.Length >= 2)
                {
                    //if (T.Contains(input[0]) && T.Contains(input[1])) P.Add();
                }
            }
            throw new Exception();
        }

        internal bool HasRule(char s, string v)
        {
            foreach (var p in P)
                if (p.Left == s && p.Right == v) return true;
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

        private string GetT(string expression)
        {
            string temp = "";
            string acceptable = "+*() ";
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

        public void Show()
        {
            Console.WriteLine("Грамматика\t" + Name);
            Console.WriteLine("Терминалы:  \t" + T);
            Console.WriteLine("Нетерминалы:\t" + N);
            Console.WriteLine("Старт:\t" + S);
            Console.WriteLine("Продукции:\t");
            foreach (char NT in N)
                Console.WriteLine($"{NT} -> {string.Join(" | ", P.FindAll(x => x.Left == NT).Select(x => x.Right))}");


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

