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

namespace Kruskal
{
    class Program
    {
        static void Main(string[] args)
        {
            // Import all Edges from file
            FileReader fr = new FileReader();
            List<List<int>> commands =  fr.getCommands("input.txt");
            
            // new Kruskal Algorithm Object
            Algorithm k = new Algorithm(commands);
            List<Edge> results = k.Process();

            // debug Console Write
            foreach (Edge e in results)
            {
                e.Write();
            }

            fr.printResults("output.txt",results);

            //Console.ReadKey();
        }
    }
}
