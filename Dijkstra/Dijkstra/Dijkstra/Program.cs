using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dijkstra
{
    class Program
    {
        static void Main(string[] args)
        {
            FileReader fr = new FileReader();

            Algorithm alg = new Algorithm(fr.getCommands("input.txt"));

            alg.Initialize();

            alg.Process();

            fr.printResults("output.txt", alg.cities, alg.routes);            
        }
    }
}
