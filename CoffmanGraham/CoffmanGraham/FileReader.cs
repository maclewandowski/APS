/*
 * APS 
 * CoffmanGraham
 * 
 * Maciej Lewandowski
 * Encrypted.pl
 * maciej@encrypted.pl
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CoffmanGraham
{
    class FileReader
    {
        // print Cmax, labels and processors into file
        public void printResult(List<int[]> Result, int[] labels, int m, string file)
        {
            FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("{0};", Result.Count);

            // labels
            sw.Write("L;");
            int vertex = new int();
            foreach (int label in labels)
            {
                sw.Write(" {0} ({1});", ++vertex, label);
            }
            sw.WriteLine("");

            // foreach processor WriteLine
            for (int i = 0; i < m; i++)
            {
                sw.Write("P{0};", i+1);
                foreach (int[] result in Result)
                {
                    sw.Write(" {0};", result[i]+1);
                }
                sw.WriteLine("");
            }

            sw.Close();
            fs.Close();
        }

        // get Words (edges tbh)
        public string[] getWords(string file)
        {
            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs);

            while (sr.EndOfStream == false)
            {

                string linia = sr.ReadLine();

                string[] words = linia.Split(';');
                return words;

            }

            string[] errors = new string[1];
            errors[0] = "0";
            return errors;
        }

        // how many vertex we will process?
        public int getWordsSize(string file)
        {
            string[] words = getWords(file);

            int size = Convert.ToInt32(words[0]);

            return size;
        }

        // splik Word (edge) into two vertex
        public int[,] parseWords(string[] words, int size)
        {
            int[,] conseq = new int[size, size];

            bool flag = new bool();
            flag = false;

            foreach (string word in words)
            {
                if (word == "") flag = false;
                if (flag)
                {
                    string[] ints = word.Split(',');
                    int vertex = Convert.ToInt32(ints[0]);
                    int cons = Convert.ToInt32(ints[1]);
                    conseq[vertex - 1, cons - 1] = 1;
                } flag = true;
            }

            return conseq;
        }
    }
}
