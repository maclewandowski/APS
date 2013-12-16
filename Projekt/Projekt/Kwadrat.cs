using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projekt
{
    class Kwadrat : Ksztalt
    {
        int rozmiar = 0;

        public Kwadrat(int n)
        {
            rozmiar = n;
        }

        public override int Obliczaj()
        {
            return rozmiar * rozmiar;
        }

        public override void WypiszPole()
        {
            Console.WriteLine(Convert.ToString(Obliczaj()));
        }
    }
}
