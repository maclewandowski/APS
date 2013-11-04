using info.lundin.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string wzor = "x+2";

            ExpressionParser parser = new ExpressionParser();

            Hashtable h = new Hashtable();

            // Add variables and values to hashtable
            h.Add( "x", 1.ToString() );
            h.Add( "y", 2.ToString() );
            // Parse and write the result
            double result = parser.Parse( "xcos(y)", h );
            Console.WriteLine( result );

            /*
            FileStream fs = new FileStream("dane.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            while (sr.EndOfStream == false)  
            {

                string linia = sr.ReadLine();

                Console.WriteLine(linia);
            }

            string wait = Console.ReadLine();*/
        }
    }
}