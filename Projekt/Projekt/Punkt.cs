using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projekt
{
    class Punkt
    {
        int size = 0;

        public Punkt()
        {
        }

        private int Obliczaj()
        {
            return size;
        }

        public void WypiszPole()
        {
            Console.WriteLine(Convert.ToString(Obliczaj()));
        }
    }
}
