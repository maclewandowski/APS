using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projekt
{
    class Prostokat : Ksztalt
    {
        int a = 0;
        int b = 0;

        public Prostokat(int n, int m)
        {
            a = n;
            b = m;
        }

        public override int Obliczaj()
        {
            return a * b;
        }

        public override void WypiszPole()
        {
            Console.WriteLine(Convert.ToString(Obliczaj()));
        }
    }
}
