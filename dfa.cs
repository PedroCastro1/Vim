using System;
using System.Collections.Generic;

using state = System.String;
using input = System.Char;

namespace DFA
{
    public class DFA
    {
        // Estado inicial.
        public state start;
        // Possíveis estados finais.
        public HashSet<state> final;

        public SortedList<KeyValuePair<state, input>, state> transitionTable;

        public DFA()
        {
            final = new HashSet<state>();
            transitionTable = new SortedList<KeyValuePair<state, input>, state>(new Comparer());
        }

        public string Simulate(string inp)
        {
            state currState = start;
            CharEnumerator i = inp.GetEnumerator();

            while (i.MoveNext())
            {
                KeyValuePair<state, input> transition = new KeyValuePair<state, input>(currState, i.Current);

                if (!transitionTable.ContainsKey(transition))
                {
                    return "Rejected";

                }
                currState = transitionTable[transition];
            }
            if (final.Contains(currState))
            {
                return "Accepted";
            }
            else
            {
                return "Rejected";
            }
        }

        public void Show()
        {
            Console.Write("DFA start state: {0}\n", start);
            Console.Write("DFA final state(s): ");

            IEnumerator<state> iE = final.GetEnumerator();

            while (iE.MoveNext())
                Console.Write(iE.Current + " ");

            Console.Write("\n\n");

            foreach (KeyValuePair<KeyValuePair<state, input>, state> kvp in transitionTable)
                Console.Write("Trans[{0}, {1}] = {2}\n", kvp.Key.Key, kvp.Key.Value, kvp.Value);
        }
    }

    public class Comparer : IComparer<KeyValuePair<state, input>>
    {
        public int Compare(KeyValuePair<state, input> transition1, KeyValuePair<state, input> transition2)
        {
            if (transition1.Key == transition2.Key)
                return transition1.Value.CompareTo(transition2.Value);
            else
                return transition1.Key.CompareTo(transition2.Key);
        }
    }

    // ------------------------------------------------------------------------------------------------------------------------ //

    public class NFA
    {
        public state start;
        public HashSet<state> final;

        public SortedList<KeyValuePair<state, input>, List<state>> transitionTable;

        public NFA(state start, HashSet<state> final)
        {
            this.start = start;
            this.final = final;
            transitionTable = new SortedList<KeyValuePair<state, input>, List<state>>(new Comparer());
        }
              
        public void Show()
        {
            Console.Write("NFA start state: {0}\n", start);
            Console.Write("NFA final state(s): ");

            IEnumerator<state> iE = final.GetEnumerator();

            while (iE.MoveNext())
                Console.Write(iE.Current + " ");

            Console.Write("\n\n");

            foreach (KeyValuePair<KeyValuePair<state, input>, List<state>> kvp in transitionTable)
                Console.Write("Transition [{0}, {1}] = {2}\n", kvp.Key.Key, kvp.Key.Value, ImprimirEstados(kvp.Value));
        }

        private string ImprimirEstados(List<state> estados)
        {
            string listaEstados = "";
            foreach (string l in estados)
            {
                listaEstados += " " + l;
            }
            return "{" + listaEstados + " }";
        }

        public class Comparer : IComparer<KeyValuePair<state, input>>
        {
            public int Compare(KeyValuePair<state, input> transition1, KeyValuePair<state, input> transition2)
            {
                if (transition1.Key == transition2.Key)
                    return transition1.Value.CompareTo(transition2.Value);
                else
                    return transition1.Key.CompareTo(transition2.Key);
            }
        }
    }

    public class App
    {
        private static int num = 0;

        public static DFA NFAtoDFA(NFA nfa)
        {
            DFA dfa = new DFA();

            HashSet<HashSet<state>> markedStates = new HashSet<HashSet<state>>();
            HashSet<HashSet<state>> unMarkedStates = new HashSet<HashSet<state>>();

            Dictionary<HashSet<state>, state> dfaStateNum = new Dictionary<HashSet<state>, state>();

            HashSet<state> nfaInitial = new HashSet<state>();
            nfaInitial.Add(nfa.start);

            return dfa;
        }

        private static int GenNewState()
        {
            return num++;
        }

        public static void Main(string[] args)
        {
            HashSet<state> ab = new HashSet<state>();
            ab.Add("q0");
            NFA a = new NFA("q0", ab);

            List<state> aa = new List<state>
            {
                "q0",
                "q1",
                "q2"
            };

            List<state> a2 = new List<state>
            {
                "q2"
            };

            a.transitionTable.Add(new KeyValuePair<state, input>("q0", '0'), aa);
            a.transitionTable.Add(new KeyValuePair<state, input>("q1", '1'), a2);

            a.Show();

            //DFA df = new DFA
            //{
            //    start = "q0",
                
            //};

            //df.final.Add("q1");

            //df.transitionTable.Add(new KeyValuePair<state, input>("q0", '0'), "q1");
            //df.transitionTable.Add(new KeyValuePair<state, input>("q1", '1'), "q0");
            //Console.WriteLine("The String {0} is {1}: " , "0101", df.Simulate("0101"));
            //df.Show();

            Console.ReadKey();
        }
    }
}
