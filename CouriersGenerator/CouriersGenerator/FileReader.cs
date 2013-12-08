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
using System.IO;
namespace CouriersGenerator
{
    class FileReader
    {
        /* 
         * print Commands into file
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
        public void printCommands(string file, List<List<int>> commands)
        {
            FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);

            foreach (List<int> commandline in commands)
            {
                string line = "";
                foreach (int command in commandline)
                {
                    line += Convert.ToString(command) + ",";    
                }
               sw.WriteLine(line);
            }

            sw.Close();
            fs.Close();
        }
    }
}
