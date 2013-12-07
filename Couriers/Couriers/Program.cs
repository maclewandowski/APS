/*
* APS
* Priority Queue Couriers
*
* Maciej Lewandowski
* Encrypted.pl
* maciej@encrypted.pl
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QueueSpace;

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
            PriorityQueue<int> pq = new PriorityQueue<int>();
            FileReader fr = new FileReader();

            List<List<int>> commands = fr.getCommands("input.txt");
            List<int[]> results = new List<int[]>();
            
            List<PriorityQueue<int>> couriers= new List<PriorityQueue<int>>();

            foreach (List<int> command in commands)
            {
                if (command[0] == 1)
                {
                    Console.WriteLine("Adding courier " + command[1]);
                    connections.Add(command[1]);
                    couriers.Add(new PriorityQueue<int>());
                }
                if (command[0] == 2)
                {
                    Console.WriteLine("Adding to courier " + command[1] + " package id: " + command[2] + " prio: " + command[3]);
                    couriers[findSlot(command[1])].Enqueue(command[2], command[3]);
                }
                if (command[0] == 3)
                {
                    Console.WriteLine("Showing courier " + command[1] + " first package");
                    if (!couriers[findSlot(command[1])].Empty)
                    {
                        int prio = couriers[findSlot(command[1])].LastPrio();
                        int package = couriers[findSlot(command[1])].Dequeue();
                        Console.WriteLine("id: " + Convert.ToString(package) + " prio: " + Convert.ToString(prio));

                        int[] result = new int[2];
                        result[0] = command[1];
                        result[1] = package;

                        results.Add(result);
                    }
                }
                if (command[0] == 4)
                {
                    Console.WriteLine("Move packages from couirer " + command[1] +" to courier " + command[2]);
                    while (!couriers[findSlot(command[1])].Empty)
                    {
                        int prio = couriers[findSlot(command[1])].LastPrio();
                        int package = couriers[findSlot(command[1])].Dequeue();
                        couriers[findSlot(command[2])].Enqueue(package,prio);
                    }
                }
            }

            fr.printResults("output.txt", results);

            Console.ReadKey();
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
