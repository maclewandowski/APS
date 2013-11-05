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

            // wypisanie
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size - 1; j++)
                {
                    if (conseq[i, j] == 1) Console.WriteLine("{0} ma nastepnika {1}", i+1, j+1);
                }
            }

            // znalezienie zarodka

            for (int i = 0; i < size; i++)
            {
                if (Alg.isPrime(conseq, i, size))
                {
                    labels = Alg.giveLabel(labels, i);
                    break;
                }                
            }

            for (int i = 0; i < size-1; i++)
            {
                labels = Alg.processNextLabel(conseq, labels, size);
            }


            for (int i = 0; i < size; i++)
            {
                Console.WriteLine("{0} ({1})", i, labels[i]);
            }



            // tablica pretenderow (dopoki bedzie pusta skacza poziomy
            // jak juz nie bedzie pusta to lecimy porownaniem

           //Console.WriteLine("S: {0}", S[0]);

            string waiter = Console.ReadLine();
        }
    }
}
