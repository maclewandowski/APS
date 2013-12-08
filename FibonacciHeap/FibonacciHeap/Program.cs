/*
* APS
* Fibonnacci Heap Couriers
*
* Maciej Lewandowski
* Encrypted.pl
* maciej@encrypted.pl
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FibonacciHeap;

namespace Couriers
{
    class Program
    {
        /*
         * connection list is needed to bind given Courier id with his real id on list
         */
        static List<int> connections = new List<int>();

        static void Main(string[] args)
        {
            // create objects
            FileReader fr = new FileReader();

            // read list of commands
            List<List<int>> commands = fr.getCommands("input.txt");

            // prepare list of results
            List<int[]> results = new List<int[]>();

            // prepare list of couriers
            List<FibHeap> couriers = new List<FibHeap>();

            /*
             * loop thru commands
             * 
             * commands are "documented" in FileReader.cs
             * 
             */
            foreach (List<int> command in commands)
            {
                if (command[0] == 1)
                {
                    Console.WriteLine("Adding courier " + command[1]);
                    connections.Add(command[1]);
                    couriers.Add(new FibHeap());
                }
                if (command[0] == 2)
                {
                    Console.WriteLine("Adding to courier " + command[1] + " package id: " + command[2] + " prio: " + command[3]);
                    couriers[findSlot(command[1])].Insert(command[3]);
                }
                if (command[0] == 3)
                {
                    Console.WriteLine("Showing courier " + command[1] + " first package");

                        int package = couriers[findSlot(command[1])].ExtractMax();

                        int[] result = new int[2];
                        result[0] = command[1];
                        result[1] = package;

                        results.Add(result);
                }
                if (command[0] == 4)
                {
                    Console.WriteLine("Move packages from couirer " + command[1] + " to courier " + command[2]);
                    couriers[findSlot(command[1])].Union(couriers[findSlot(command[2])]);
                    couriers[findSlot(command[2])] = new FibHeap();
                }
            }

            // print results
            fr.printResults("output.txt", results);

            //Console.ReadKey();
        }

        /*
         * returning real id on List from given one
         */
        static private int findSlot(int conenction)
        {
            for (int i = 0; i < connections.Count; i++)
            {
                if (connections[i] == conenction)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
