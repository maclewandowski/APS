/*
* APS
* Dijkstra Couriers
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

namespace Dijkstra
{
    class FileReader
    {
        /*
         * printing result to file
         * city: Vertex.id, length: Vertex.d, route: Vertex.id > ... > Vertex.id
         */ 
        public void printResults(string file, Vertex[] cities, List<List<int>> routes)
        {
            FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);

            for (int i = 0; i < cities.Count(); i++)
            {
                string route = "";
                foreach (int cid in routes[i])
                {
                    route += " > ";
                    route += cid;
                }

                sw.WriteLine("city: " + cities[i].id + ", length: " + cities[i].d + ", route: " + cities[i].id + route);

            }

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
