using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ATFL
{
    [Serializable]
    class StateMachine : IShowable
    {
        public string           Name { get; set; } = "ATFL";    /// Имя автомата
        private List<TransRule> StateTable;                     /// Таблица переходов
        public  List<string>    FinalState;                     /// Множество конечных состояний
        public  string          Alphabet { get; set; }          /// Множество входных символов
        public  string          CurrentState { get; set; }      /// Текущее состояние
        public  string          StartState { get; set; }        /// Начальное состояние
        public event ReportEventHandler Report = Program.R.CompleteLog;  /// Отправка записи в лог

        /// <summary>
        /// Инициализирует новый экземпляр класса StateMachine перечнем правил
        /// </summary>
        /// <param name="TransitionRules">Строка с перечислением правил перехода и указанием начального и конечных состояний</param>
        public StateMachine(string TransitionRules) 
        {
            StateTable = new List<TransRule>();
            FinalState = new List<string>();
            Alphabet = "";
            if (!ParseOK(TransitionRules)) Console.WriteLine("Не удалось заполнить таблицу переходов");
            else
            {
                SortStateTable();
                CurrentState = StartState;
                Alphabet = GetRealAlphabet();
            }                
        }
        /// <summary>
        /// Инициализирует новый экземпляр класса StateMachine готовыми полями
        /// </summary>
        /// <param name="Alphabet">Строка с перечислением множества входных символов</param>
        /// <param name="StateTable">Ссылка на существующую таблицу переходов</param>
        /// <param name="StartState">Указание имени начального состояния</param>
        /// <param name="FinalState">Ссылка на список имен конечных состояний</param>
        public StateMachine(string Alphabet, List<TransRule> StateTable, string StartState, List<string> FinalState)
        {
            this.Alphabet = Alphabet;
            this.StartState = StartState;
            this.FinalState = FinalState.GetRange(0, FinalState.Count);
            this.StateTable = StateTable.GetRange(0, StateTable.Count);
        }
        /// <summary>
        /// Инициализирует новый экземпляр класса StateMachine по грамматике с правилами
        /// </summary>
        /// <param name="G">Регулярная грамматика, по которой должен быть построен конечный автомат</param>
        public StateMachine(Grammar G)
        {
            // ----------< Блок инициализации >---------------------------------
            StateTable = new List<TransRule>();
            FinalState = new List<string>();
            int numerator = 0;
            // ----------< Преподготовка к преобразованию грамматики >----------
            Alphabet = G.T;
            Report(this, new ReportEventArgs($"Шаг {++numerator}. В алфавит войдут все терминалы грамматики: ∑ = {Alphabet}"));
            StartState = G.S.ToString();
            Report(this, new ReportEventArgs($"Шаг {++numerator}. Стартовое состояние соответствует стартовому состоянию грамматики: S = {StartState}"));
            string newNT = G.GetNextNT().ToString(); 
            Report(this, new ReportEventArgs($"Шаг {++numerator}. Добавим специальное новое допускающее состояние: {newNT}"));
            // ----------< Преобразование правил грамматики >-------------------
            Report(this, new ReportEventArgs("Начнем заполнять таблицу переходов."));
            int subcount = 0;
            foreach (var p in G.P)
            {
                Report(this, new ReportEventArgs($"\nШаг {numerator}.{++subcount}. Рассмотрим продукцию {p.Display()}"));
                string temp = "";
                if (p.Right.Count() == 2)
                {
                    StateTable.Add(new TransRule(p.Left.ToString(), p.Right[0], p.Right[1].ToString()));
                    temp += $"Продукция вида A -> aB. Добавим правило δ({p.Left},{p.Right[0]}) = {p.Right[1]}.";
                    if (G.HasPair(p) && !FinalState.Contains($"{p.Right[1]}"))
                    {
                        temp += $"{p.Right[1]} - завершающая вершина, т.к. существует парный переход из {p.Left} по тому же символу {p.Right[0]}.";
                        FinalState.Add(p.Right[1].ToString());
                    }
                }
                else
                {
                    temp += "Продукция вида A -> a. ";
                    if (G.HasPair(p)) temp += $"Существует парное правило вида A -> aB. Поэтому добавлять переход в таблицу не нужно.";
                    else
                    {
                        temp += $"Не существует правила вида A -> aB. Поэтому добавим переход в допускающее состояние: δ({p.Left},{p.Right[0]}) = {newNT}.";
                        StateTable.Add(new TransRule(p.Left.ToString(), p.Right[0], newNT.ToString()));
                    }
                }
                Report(this, new ReportEventArgs(temp));
            }
            // ----------< Поиск финальных состояний и завершение >-------------
            if (!FinalState.Contains(StartState) && G.IncludesRule(G.S, "ε"))
            {
                Report(this, new ReportEventArgs($"Шаг {++numerator}. Существует переход из стартовой вершины {StartState} в ε. {StartState} является заключительным состоянием"));
                FinalState.Add(StartState);
            }
            if (!FinalState.Contains(newNT) && ExistsInTable(newNT)) FinalState.Add(newNT);
            SortStateTable();
        }
        /// <summary>
        /// Ищет уникальные входные символы, существующие в действующей таблице переходов
        /// </summary>
        /// <returns>Список уникальных входных символов, существующих в действующей таблице переходов</returns>
        public string GetRealAlphabet()
        {
            string str = "";
            foreach (TransRule rule in StateTable) if (!str.Contains(rule.Symbol)) str += rule.Symbol;
            return str;
        }
        /// <summary>
        /// Геттер множества существующих состояний
        /// </summary>
        public string[] SetOfStates         // Возвращает массив имен состояний, задействованных в таблице переходов
        {
            get
            {
                List<string> str = new List<string>();
                foreach (TransRule rule in StateTable)
                {
                    if (!str.Contains(rule.Next)) str.Add(rule.Next);
                    if (!str.Contains(rule.Current)) str.Add(rule.Current);
                }
                return str.ToArray();
            }
        }
        /// <summary>
        /// Основной метод для выделения правил перехода из входной строки для класса StateMachine
        /// </summary>
        /// <param name="input">Фходная строка в формате TransRule1, TransRule2, ... TransRuleN | Start Fin1 Fin2 ... </param>
        /// <returns>Сигнал об успешном выполнении парсинга</returns>
        private bool ParseOK(string input)  // 
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
        private bool FillStates(string input)
        {   
            Regex regex = new Regex(@"\s*\w+\s*:\s*(\w|ε)\s*->\s*\w+\s*");  // Ввод по образцу [current]: [symbol] -> [next]
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
        /// <summary>
        /// Группирует правила в таблице и сортирует по алфавиту
        /// </summary>
        private void SortStateTable()
        {
            TRuleComparer rc = new TRuleComparer();
            StateTable.Sort(rc);
        }
        /// <summary>
        /// Проверяет наличие состояния state в таблице
        /// </summary>
        /// <param name="state">Имя состояния</param>
        /// <returns>Возвращает true, если состояние существует</returns>
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
        /// <summary>
        /// Расширенный метод проверки наличия состояния state в таблице stateTable
        /// </summary>
        /// <param name="stateTable">Обрабатывемая таблица переходов</param>
        /// <param name="state">Искомое состояние</param>
        /// <param name="c">Флаг поиска: 'c', чтобы искать в current, 'n', чтобы искать в next </param>
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
        /// <summary>
        /// Возвращает новый автомат, приведенный к детермининированному виду
        /// </summary>
        public StateMachine DFMFromNFM()
        {
            // ----------< Блок инициализации >---------------------------------
            List<TransRule> newStateTable = new List<TransRule>(); /// Новая (пустая) таблица состояний
            List<string> newFinalState = new List<string>();       /// Новый (пустой) список конечных состояний
            Queue<List<string>> Q = new Queue<List<string>>();     /// Очередь для последовательного разбора множеств состояний
            int numerator = 0;                                     /// Нумерация этапов
            // ----------< Детерминизация правил КА >---------------------------
            Report(this, new ReportEventArgs($"Q - очередь разбора состояний. Изначально в Q находится стартовое состояние {{{StartState}}}."));
            Q.Enqueue(new List<string> { StartState });            // Стартовая вершина включена в список
            while (Q.Count != 0)                                   // Пока не будут разобраны все связи
            {
                List<string> set = Q.Dequeue();                    // 1. Вытаскиваем множество состояний из очереди
                Report(this, new ReportEventArgs($"\nШаг {++numerator}. Вынимаем из Q множество {{{string.Join(",", set)}}}."));
                foreach (char c in Alphabet)                       // 2. Для каждой буквы находим next states этого множества
                {
                    bool isFinal = FindNextStatesForSet(set, c, out List<string> nextStates);
                    string newStateFromSet = string.Join(null, nextStates);
                    if (nextStates.Count != 0)
                    {
                        string temp = $"\nШаг {numerator}.{c} ";
                        if (nextStates.Count > 1)
                            temp += $"\nНедетерминированность по символу {c}. Объединяем результаты перехода в множество:";
                        temp += $"\n{string.Join(null, set)} : {c} -> {newStateFromSet}. Новое правило перехода вносится в таблицу.";
                        if (isFinal && !ExistsInTable(newStateTable, newStateFromSet, 'n'))
                        {
                            temp += $"\nСостояние {newStateFromSet} - завершающее, т.к. среди исходных было завершающее.";
                            newFinalState.Add(newStateFromSet);
                        }
                        newStateTable.Add(new TransRule(string.Join(null, set), c, newStateFromSet));
                        if (!(ExistsInTable(newStateTable, newStateFromSet) || newStateFromSet == StartState || WasInQ(Q, nextStates)))
                        {

                            temp += $"\nCостояние {newStateFromSet} еще не рассмотрено. Помещаем его в Q.";
                            Q.Enqueue(nextStates);
                        }
                        else
                            temp += $"Cостояние {newStateFromSet} уже рассмотрено. Переходим дальше.";
                        Report(this, new ReportEventArgs(temp));
                    }
                }
            }
            // ----------< Завершение >-----------------------------------------
            Report(this, new ReportEventArgs("Q пуста => все пути из начальной вершины рассмотрены."));
            StateMachine newSM = new StateMachine(Alphabet, newStateTable, StartState, newFinalState);
            newSM.SortStateTable();
            return newSM;
        }
        /// <summary>
        /// Служебный метод для проверки, что состояние уже было рассмотрено
        /// </summary>
        /// <param name="Q">Очередь разбора</param>
        /// <param name="nextStates">Состояние (множество состояний)</param>
        /// <returns>Возвращает true, в случае, если состояние было рассмотрено</returns>
        private bool WasInQ(Queue<List<string>> Q, List<string> nextStates)
        {
            bool wasInQ = false;
            foreach (var q in Q)
                if (q.Except(nextStates).Count() == 0 && nextStates.Except(q).Count() == 0) { wasInQ = true; break; }
            return wasInQ;
        }
        /// <summary>
        /// Возвращает множество состояний nextStates, в которые переходят состояния из set по символу sym
        /// </summary>
        /// <returns>Возвращает true, если множество найденных состояний - заверщающее</returns>
        public bool FindNextStatesForSet(List<string> set, char sym, out List<string> nextStates)
        {
            nextStates = new List<string>();
            foreach (string state in set)
                nextStates = nextStates.Union(StateTable.FindAll(x => x.Current == state && x.Symbol == sym).Select(x => x.Next)).ToList();
            foreach (string state in nextStates)
                if (FinalState.Contains(state)) return true;
            return false;
        }
        /// <summary>
        /// Возвращает массив состояний nextStates, в которые переходят состояния из curr по символу sym
        /// </summary>
        /// <returns>Возвращает true, если найденное множество непустое</returns>
        public bool FindNextStates(string curr, char sym, out List<string> nextStates)
        {
            nextStates = StateTable.FindAll(x => x.Current == curr && x.Symbol == sym).Select(x => x.Next).ToList();
            return nextStates.Count() > 0;
        }
        /// <summary>
        /// Обеспечивает форматированный вывод конфигурации конечного автомата
        /// </summary>
        /// <param name="mode">Режим вывода: r для вывода в виде перечня правил, t для табличного вывода</param>
        public void Show(char mode = 'r')
        {
            const int F = 10;
            Report(this, new ReportEventArgs($"Начальное состояние: {StartState}"));
            string temp = FinalState.Count == 1 ? "Конечное состояние: " : "Конечные состояния: ";
            foreach (var t in FinalState)
                temp += $"{t} ";
            Report(this, new ReportEventArgs(temp));
            StringBuilder sb = new StringBuilder();
            sb.Append("Таблица переходов: \n");
            switch (mode)
            {
                case 't':
                    // Особая первая строка
                    sb.Append($"{' ',-F}");
                    foreach (char c in Alphabet) sb.Append($"{c,-F}");
                    sb.AppendLine();
                    // Остальные строки
                    string [] currNames = SetOfStates.ToArray();
                    foreach (string state in currNames)
                    {
                        sb.Append($"{state,-F}");
                        foreach (char c in Alphabet)
                        {
                            var nexts = StateTable.FindAll(x => x.Current == state && x.Symbol == c).Select(x => x.Next);
                            if (nexts.Count() > 0) sb.Append($"{string.Join(",", nexts),-F}");
                            else sb.Append($"{'-',-F}");
                        }
                        sb.AppendLine();
                    }
                    Report(this, new ReportEventArgs(sb.ToString()));
                    break;
                default:
                    temp = "";
                    foreach (TransRule rule in StateTable)
                        temp += rule.DisplayRule();
                    Report(this, new ReportEventArgs(temp));
                    break;
            }
        }
    }

    internal class TRuleComparer : IComparer<TransRule>
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
    internal sealed class TransRule
    { 
        /// <summary>
        /// Текущее состояние
        /// </summary>
        public string Current { get; set; }
        /// <summary>
        /// Условие перехода
        /// </summary>
        public char   Symbol  { get; set; }
        /// <summary>
        /// Следующее состояние
        /// </summary>
        public string Next    { get; set; }
        
        public TransRule(string current, char symbol, string next)
        {
            this.Current = current;
            this.Symbol = symbol;
            this.Next = next;
        }
        public string DisplayRule()
        {
            return $"{Current}: {Symbol} -> {Next}";
        }
    }
}



