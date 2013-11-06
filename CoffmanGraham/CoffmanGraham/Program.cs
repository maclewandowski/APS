/* APS 
 * CoffmanGraham 
 * 
 * Encrypted.pl
 * maciej@encrypted.pl
 */

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

            // algorith object
            Algorithm Alg = new Algorithm();

            // grab data from text file
            string[] words = File.getWords(file_name);
            int size = File.getWordsSize(file_name);
            
            // array of final labels
            int[] labels = new int[size];

            // array of conenctions
            int[,] conseq = File.parseWords(words, size);

            // seed
            for (int i = 0; i < size; i++)
            {
                if (Alg.isPrime(conseq, i, size))
                {
                    labels = Alg.giveLabel(labels, i);
                    break;
                }                
            }

            // finding other labels
            for (int i = 0; i < size-1; i++)
            {
                labels = Alg.processNextLabel(conseq, labels, size);
            }
                        
            // Hu
            List<int[]> Result = Alg.algorithmHu(conseq, labels, size, 2);

            Console.WriteLine("-- FINAL RESULT --");

            foreach (int[] attime in Result)
            {
                Console.WriteLine("{0} {1}", attime[0], attime[1]);
            }
            
            string waiter = Console.ReadLine();
        }
    }
}
