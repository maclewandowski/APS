using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffmanGraham
{
    class Program
    {
        static void Main(string[] args)
        {

            // configuration
            string file_name = "input.txt";

            // our reading object
            FileReader File = new FileReader();

            // algorith
            Algorithm Alg = new Algorithm();


            string[] words = File.getWords(file_name);
            int size = File.getWordsSize(file_name);


            // array of final labels
            int[] labels = new int[size];






            int[,] conseq = File.parseWords(words, size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size - 1; j++)
                {
                    if (conseq[i, j] == 1) Console.WriteLine("{0} ma nastepnika {1}", i+1, j+1);
                }
            }


            for (int i = 0; i < size; i++)
            {
                if (Alg.isPrime(conseq, i, size))
                {
                    labels = Alg.giveLabel(labels, i);
                    break;
                }                
            }
            /*
            for (int i = 0; i < size; i++)
            {
                Console.Write("{0} {1} ;", i, labels[i]);
            }
            */
            // lista takich co to ich nastepniki maja etykiety

            List<int> S = Alg.findS(conseq, labels, size);
            List<List<int>> TS = new List<List<int>>();

            foreach (int vertex in S)
            {
                List<int> T = Alg.findT(conseq, labels, size, vertex);

                TS.Add(T);
            }


            int listSize = S.Count;

            // wyswietlanie dla testu


            for (int i = 0; i < listSize; i++)
            {
                Console.WriteLine("S: {0}", S[i]);
                int subListSize = TS[i].Count;

                for(int j=0; j<subListSize; j++)
                {
                    Console.WriteLine("- T: {0}", TS[i][j]);
                }
            }
            
            // sortowanie + znalezienie max długości
            int maxListLenght = new int();

            for (int i = 0; i < listSize; i++)
            {
                TS[i].Sort();

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
            
            Console.WriteLine("== nieposortowane intTS ==");

            for (int i = 0; i < listSize; i++)
            {
                Console.WriteLine(intTS[i]);
            }

            Console.WriteLine("====");
            
            //====
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

            Console.WriteLine("== nieposortowane cloneTS ==");

            for (int i = 0; i < listSize; i++)
            {
                Console.WriteLine(cloneTS[i]);
            }

            Console.WriteLine("====");

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


            // tablica pretenderow (dopoki bedzie pusta skacza poziomy
            // jak juz nie bedzie pusta to lecimy porownaniem

           //Console.WriteLine("S: {0}", S[0]);

            string waiter = Console.ReadLine();
        }
    }
}
