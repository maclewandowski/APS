using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffmanGraham
{
    class Algorithm
    {
        public bool isPrime(int[,] conseq, int pretender, int size)
        {
            for (int i = 0; i < size; i++)
            {
                if (conseq[pretender,i] != 0) return false;
            }
            return true;
        }

        public int[] giveLabel(int[] labels, int id)
        {
            int count = new int();
            for (int i = 0; i < labels.Length; i++)
            {
                if(labels[i]!=0) count++;
            }
            labels[id] = count+1;
            return labels;
        }

        public bool gotLabel(int[] labels, int pretender)
        {
            if (labels[pretender] == 0) return false;
            else return true;
        }

        public List<int> findS(int[,] conseq, int[] labels, int size)
        {
            List<int> S = new List<int>();

            for (int i = 0; i < size; i++)
            {
                if(!gotLabel(labels,i))
                {
                    bool flag = new bool();
                    flag = true;
                    for (int j = 0; j < size; j++)
                    {
                        if((conseq[i,j]!=0)&&(!gotLabel(labels,j))) flag = false;
                    }
                    if(flag) S.Add(i);
                }
            }

            return S;
        }

        public List<int> findT(int[,] conseq, int[] labels, int size, int id)
        {
            List<int> T = new List<int>();

                    for (int j = 0; j < size; j++)
                    {
                        if (conseq[id, j] != 0)
                        {
                            T.Add(labels[j]);
                        }
                    }
            return T;
        }

        public int[] processNextLabel(int[,] conseq, int[] labels, int size)
        {
            // lista takich co to ich nastepniki maja etykiety

            List<int> S = findS(conseq, labels, size);
            List<List<int>> TS = new List<List<int>>();

            foreach (int vertex in S)
            {
                List<int> T = findT(conseq, labels, size, vertex);

                TS.Add(T);
            }


            int listSize = S.Count;

            /* wyswietlanie dla testu


            for (int i = 0; i < listSize; i++)
            {
                Console.WriteLine("S: {0}", S[i]);
                int subListSize = TS[i].Count;

                for (int j = 0; j < subListSize; j++)
                {
                    Console.WriteLine("- T: {0}", TS[i][j]);
                }
            }*/

            // sortowanie + znalezienie max długości
            int maxListLenght = new int();

            for (int i = 0; i < listSize; i++)
            {
                TS[i].Sort();
                TS[i].Reverse();

                if (TS[i].Count > maxListLenght) maxListLenght = TS[i].Count;
            }

            List<int> intTS = new List<int>();

            for (int i = 0; i < listSize; i++)
            {
                string pseudoInt = "";

                for (int j = 0; j < maxListLenght; j++)
                {
                    if (j < TS[i].Count) pseudoInt += TS[i][j];
                    else pseudoInt += "0";
                }

                intTS.Add(Convert.ToInt32(pseudoInt));
            }
            /*
            Console.WriteLine("== nieposortowane intTS ==");

            for (int i = 0; i < listSize; i++)
            {
                Console.WriteLine(intTS[i]);
            }

            Console.WriteLine("====");

            //====*/
            List<int> cloneTS = new List<int>();

            for (int i = 0; i < listSize; i++)
            {
                cloneTS.Add(intTS[i]);
            }

            intTS.Sort();
            
            Console.WriteLine("== posortowane intTS ==");

            for (int i = 0; i < listSize; i++)
            {
                Console.WriteLine(intTS[i]);
            }

            Console.WriteLine("====");
            /*
            Console.WriteLine("== nieposortowane cloneTS ==");

            for (int i = 0; i < listSize; i++)
            {
                Console.WriteLine(cloneTS[i]);
            }

            Console.WriteLine("====");
            */
            int pretender = new int();

            Console.WriteLine("takiego szukam: {0}", intTS[0]);

            for (int i = 0; i < listSize; i++)
            {
                if (intTS[0] == cloneTS[i])
                {
                    Console.WriteLine("znalazlem: {0} na pozycji {1}", cloneTS[i], i);
                    pretender = i;
                }
            }

            // przelec TS i poszukaj najpierw dlugosci 0 - pretenderzy
            // potem tacy co maja 1 na pierwszym slocie

            Console.WriteLine("nadać labelka teraz: {0}", S[pretender]);


            labels = giveLabel(labels, S[pretender]);

            return labels;

        }
    }
}
