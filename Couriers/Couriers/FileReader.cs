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
using System.IO;

namespace Couriers
{
    class FileReader
    {
        // prints results into file
        public void printResults(string file, List<int[]> results)
        {
            FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);

            foreach (int[] result in results)
            {
                sw.WriteLine(result[0] + "," + result[1]);
            }
                        
            sw.Close();
            fs.Close();
        }

        /* 
         * get Commands from file
         * 
         * 1. Add Counrier K(id)
         * A,(int)id_K
         * example: 1,1
         * 
         * 2. Add package P(id) with Prio(id) to courier K(id)
         * AK,(int)id_K,(int)id_P,(int)Prio
         * example: 2,1,1,1
         * 
         * 3. Show courier K(id) first package P(id)
         * S,(int)id_K
         * example: 3,1
         *
         * 4. Move all packages from courier K1(id) to K2(id)
         * MV,(int)id_K1,(int)id_K2
         * example: 4,1,2
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
