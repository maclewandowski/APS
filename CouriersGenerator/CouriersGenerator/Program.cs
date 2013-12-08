/*
* APS
* Couriers Data Generator
*
* Maciej Lewandowski
* Encrypted.pl
* maciej@encrypted.pl
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CouriersGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * Every 1 in size
             * gives around 100KB of file size
             * 
             * 10 = 1MB
             * 100 = 10MB
             * 1K = 100MB
             * 10K = 1G
             */ 
            int size = 1;

            FileReader fr = new FileReader();            
            List<List<int>> commands = new List<List<int>>();

            // couriers counter
            int couriers = new int();

            // packages counters for all couriers
            List<int> packages = new List<int>();

            // random
            Random rnd = new Random();

            //initialize the 0 package index
            packages.Add(0);

            for (int i = 0; i < size*10000; i++)
            {
                // random action
                int action = rnd.Next(1, 4);

                List<int> command = new List<int>();

                // add courier
                if (action == 1)
                {
                    // add courier counter
                    couriers++;

                    // add counter of packages for that courier
                    packages.Add(0);

                    // add command
                    command.Add(1);
                    command.Add(couriers);
                }

                // add package to courier
                if (action == 2)
                {
                    // are there any couriers yet?
                    if (couriers > 0)
                    {
                        // which courier
                        int cr = rnd.Next(1, couriers);
                        
                        // package counter
                        packages[cr]++; 

                        // which package
                        int pck = packages[cr];

                        // random priority
                        int prio = rnd.Next(1, 1000);

                        // add command
                        command.Add(2);
                        command.Add(cr);
                        command.Add(pck);
                        command.Add(prio);
                    }
                }

                // show courier package
                if (action == 3)
                {
                    // any courier exists
                    if (couriers > 0)
                    {
                        // which courier
                        int cr = rnd.Next(1, couriers);

                        // does courier got packages to show
                        if (packages[cr] > 0)
                        {
                            // remove package counter
                            packages[cr]--;

                            // add command
                            command.Add(3);
                            command.Add(cr);
                        }
                    }
                }

                // move packages from one courier to another
                if (action == 4)
                {
                    // which couriers
                    int cr1 = rnd.Next(1, couriers);
                    int cr2 = rnd.Next(1, couriers);

                    // not the same couriers?
                    if (cr1 != cr2)
                    {
                        command.Add(4);
                        command.Add(cr1);
                        command.Add(cr2);
                    }
                }


                if (command.Count > 0)
                {
                    commands.Add(command);
                }
            }

            fr.printCommands("test.txt", commands);
        }
    }
}
