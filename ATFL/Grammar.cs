using System;
using System.Collections.Generic;
using System.Linq;

namespace ATFL
{
    class Grammar
    {
        public string Name { get; set; } = "ATFL";
        public string T { get; set; }
        public string N { get; set; }
        public List<GramRule> P;
        public char S { get; set; } = 'S';

        public Grammar(string T, string N, List<GramRule> P, char S)
        {
            this.T = T;
            this.N = N;
            this.P = P.GetRange(0,P.Count);
            this.S = S;
        }
        public Grammar(string Expression)
        {
            T = GetT(Expression);
            ParseOK(S, Expression);
            N = string.Join(",", P.Distinct().ToList());
        }

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
            foreach (var A in Q)
            {
                foreach (var a in T)
                {
                    string [] B;
                    if (SM.FindNextStates(A,a, out B))
                    {
                        foreach (string b in B)
                        {
                            P.Add(new GramRule(Renames[A], $"{a}{Renames[b]}"));
                            if (SM.FinalState.Contains(b))
                            {
                                if (Renames[A] == S) P.Add(new GramRule(Renames[A], "ε"));
                                P.Add(new GramRule(Renames[A], $"{a}"));
                            }
                        }                            
                    }
                }
            }
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
                        if (ExistsInTable(branch))
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

        private bool ExistsInTable(string branch)
        {
            throw new NotImplementedException();
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
                Console.WriteLine($"{NT} = {string.Join(" | ", P.FindAll(x => x.Left == NT).Select(x => x.Right))}");


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
    
        public void Display()
        {
            Console.WriteLine($"{Left} = {Right}");
        }
    }
}

