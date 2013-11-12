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

namespace CoffmanGraham
{
    class Program
    {
        static void Main(string[] args)
        {

            // configuration
            string output = "output.txt";

            // maxsize can be UNLIMITED - this is max size of generated graph
            int maxsize = 15;

            // our reading object
            FileReader File = new FileReader();

            // algorith object
            Algorithm Alg = new Algorithm();

            // random max size of graph
            int size = Alg.generateGraphSize(maxsize);

            // generate
            int[,] graph = Alg.generateGraph(size);

            // save graph to file
            File.printResult(graph, size, output);
        }
    }
}
