/*
* APS
* Kruskal
*
* Maciej Lewandowski
* Encrypted.pl
* maciej@encrypted.pl
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kruskal
{
    class Algorithm
    {
        List<Edge> edges;
        List<Edge> results;
        Tree tree;

        public Algorithm(List<List<int>> commands)
        {
            // Create size of Trees
            tree = new Tree(commands[0][0]);
            edges = new List<Edge>();

            // foreach Vertex do MakeSet
            for (int i = 0; i < commands[0][0]; i++)
            {
                tree.MakeSet(i);
            }

            // Create List of Edges
            for (int i = 1; i < commands.Count; i++)
            {
                Edge e = new Edge();

                e.weight = commands[i][0];
                e.u = commands[i][1];
                e.v = commands[i][2];

                edges.Add(e);
            }
        }

        public List<Edge> Process()
        {
            // sort Edges
            List<Edge> sorted = edges.OrderBy(o => o.weight).ToList();
            results = new List<Edge>();

            // foreach Edge in sorted List
            foreach (Edge e in sorted)
            {
                if (tree.FindSet(tree.vertexes[e.u]) != tree.FindSet(tree.vertexes[e.v]))
                {
                    results.Add(e);
                    tree.Union(tree.vertexes[e.u], tree.vertexes[e.v]);
                }
            }

            return results;
        }


    }

    /*
     * Tree is not Tree in mathematical mean. It can be woods as well.
     */ 
    class Tree
    {
        public Vertex[] vertexes { get; set; }

        public Tree(int size)
        {
            vertexes = new Vertex[size];
        }

        public void MakeSet(int id)
        {
            Vertex x = new Vertex();
            x.id = id;
            x.p = id;
            x.rank = 0;

            vertexes[x.id]=x;
        }

        public Vertex FindSet(Vertex x)
        {
            if (x.id != x.p)
            {
                x.p = FindSet(vertexes[x.p]).id;
            }
            return vertexes[x.p];
        }

        public void Union(Vertex x, Vertex y)
        {
            Link(FindSet(x), FindSet(y));
        }

        public void Link(Vertex x, Vertex y)
        {
            if (x.rank > y.rank)
            {
                y.p = x.id;
            }
            else
            {
                x.p = y.id;
                if (x.rank == y.rank)
                {
                    y.rank++;
                }
            }
        }
        
    }

    /*
     * Vertex contains id, parent id (p) and rank 
     */ 
    class Vertex
    {
        public int id { get; set; }
        public int p { get; set; }
        public int rank { get; set; }
    }

    /*
     * Edge contains u,v as Vertexes and weight as cost
     */
    class Edge
    {
        public int u { get; set; }
        public int v { get; set; }
        public int weight { get; set; }

        // debug print to console
        public void Write()
        {
            Console.WriteLine("Edge (" + u + "," + v + ") cost: " + weight);
        }
    }
}
