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
using System.IO;

namespace CoffmanGraham
{
    class FileReader
    {
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

        public int getWordsSize(string file)
        {
            string[] words = getWords(file);

            int size = Convert.ToInt32(words[0]);

            return size;
        }

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
