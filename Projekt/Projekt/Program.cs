using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projekt
{
    class Program
    {
        static void Main(string[] args)
        {
            Kwadrat k = new Kwadrat(5);
            k.WypiszPole();

            Prostokat r = new Prostokat(3, 4);
            r.WypiszPole();

            Punkt p = new Punkt();
            p.WypiszPole();

            Console.ReadKey();
        }
    }
}
