/*
 * APS 
 * CoffmanGrahamGenerator
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
        // printing result
        public void printResult(int[,] graph, int size, string file)
        {
            FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            
            // first place is size
            sw.Write("{0};", size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (graph[i, j] != 0)
                    {
                        sw.Write("{0},{1};", i + 1, j + 2);
                    }
                }
            }

            sw.Close();
            fs.Close();
        }
    }
}
