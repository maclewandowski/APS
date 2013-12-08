/*
* APS
* Kruskal
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

namespace Kruskal
{
    class FileReader
    {
        // print Cmax, labels and processors into file
        public void printResults(string file, List<Edge> results)
        {
            FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);

            int total = 0;

            foreach (Edge result in results)
            {
                sw.WriteLine("(" + result.u + "," + result.v + ") cost: " + result.weight);
                total += result.weight;
            }

            sw.WriteLine("total cost: " + total);

            sw.Close();
            fs.Close();
        }

        /* 
         * get Commands from file
         * 
         * first line is size of graph (amount of vertex)
         * 
         * next lines are all edges
         * 
         * weight,vertex,vertex 
         */
        public List<List<int>> getCommands(string file)
        {
            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs);

            List<List<int>> commands = new List<List<int>>();

            while (sr.EndOfStream == false)
            {

                string line = sr.ReadLine();
                string[] numbers = line.Split(',');
                List<int> command = new List<int>();

                foreach (string number in numbers)
                {
                    command.Add(Convert.ToInt32(number));
                }

                commands.Add(command);
            }

            return commands;
        }
    }
}
