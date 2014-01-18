using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dijkstra
{
    class Algorithm
    {
        List<Edge> ways;
        public Vertex[] cities { get; set; }
        List<int> linked;
        List<int> done;
        public List<List<int>> routes { get; set; }

        public Algorithm(List<List<int>> input)
        {
            // Create cities
            cities = new Vertex[input[0][0]];

            for (int i = 0; i < input[0][0]; i++)
            {
                cities[i] = new Vertex();
            }

            // Create ways
            ways = new List<Edge>();

            // and other stuff
            linked = new List<int>();
            done = new List<int>();
            routes = new List<List<int>>();

            for (int i = 1; i < input.Count; i++)
            {
                Edge way = new Edge();

                way.weight = input[i][0];
                way.u = input[i][1];
                way.v = input[i][2];

                ways.Add(way);
            }
        }

        public void Initialize()
        {
            int i = 0;
            foreach (Vertex city in cities)
            {
                city.id = i;
                city.d = int.MaxValue;
                city.pi = -1;

                i++;
            }

            // HQ d from HQ is 0
            cities[0].d = 0;
        }

        public void Process()
        {
            int id = 0;

            while (id != -1)
            {
                Console.WriteLine(id);
                findLinked(id);
                //printCities();
                done.Add(id);
                id = extractMin();
            }

            findRoutes();
        }

        public void findRoutes()
        {
            foreach (Vertex city in cities)
            {
                List<int> route = new List<int>();
                
                int pre = city.pi;

                while(pre != -1) 
                {
                    route.Add(pre);
                    pre = cities[pre].pi;
                }

                routes.Add(route);
            }

        }

        public int extractMin()
        {
            int min = int.MaxValue;
            int id = -1;

            foreach (int link in linked)
            {
                if (cities[link].d < min)
                {
                    id = link;
                }
            }

            if (id != -1)
            {
                linked.Remove(id);
            }

            return id;
        }

        /*
         * Just for easy debug
         */
        public void printCities()
        {
            Console.WriteLine("Cities list:");
            foreach(Vertex city in cities)
            {
                Console.WriteLine("id: " + Convert.ToString(city.id) + " pi: " + Convert.ToString(city.pi) + " d: " + Convert.ToString(city.d));
            }
        }

        public void findLinked(int id)
        {
            foreach (Edge way in ways)
            {
                if (way.u == id)
                {
                    Relax(way.u, way.v, way.weight);
                }
                if (way.v == id)
                {
                    Relax(way.v, way.u, way.weight);
                }
            }
        }

        public void Relax(int u, int v, int weight)
        {
            if (cities[v].d > (cities[u].d + weight))
            {
                cities[v].d = (cities[u].d + weight);
                cities[v].pi = u;
            }

            if ((!done.Contains(v))&&(!linked.Contains(v)))
            {
                linked.Add(v);
            }
        }
    }

    class Vertex
    {
        public int id { get; set; }
        public int pi { get; set; }
        public int d { get; set; }
    }

    class Edge
    {
        public int u { get; set; }
        public int v { get; set; }
        public int weight { get; set; }
    }
}
