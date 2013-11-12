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
    class Algorithm
    {
        public int[,] generateGraph(int size)
        {
            int[,] graph = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                Random rnd = new Random();
                int succ = rnd.Next(0, size-(i+1));

                for (int j = 0; j < succ; j++)
                {
                    int v = rnd.Next(i, size-1);

                    graph[i, v] = 1;
                }
            }

            return graph;
        }

        public int generateGraphSize(int max)
        {
            Random rnd = new Random();
            int size = rnd.Next(1, max+1);
            return size;
        }
    }
}
