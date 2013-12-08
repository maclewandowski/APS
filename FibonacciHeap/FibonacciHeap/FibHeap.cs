/*
* APS
* Fibonnacci Heap Couriers
*
* Maciej Lewandowski
* Encrypted.pl
* maciej@encrypted.pl
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FibonacciHeap
{
    class FibHeap
    {
        // ids from keys List
        public List<int> roots { get; set; }

        // amount of vertexes
        static int n;

        // to be honest n-1
        public int last_id;
        public int max { get; set; }
        public int max_id { get; set; }

        public List<FibHeapVertex> vertexes { get; set; }

        // basic constructor
        public FibHeap()
        {
            n = 0;
            last_id = -1;
            max = -1;
            vertexes = new List<FibHeapVertex>();
            roots = new List<int>();
        }

        // Inserting new Vertex into our Fib heap
        public void Insert(int key)
        {
            FibHeapVertex x = new FibHeapVertex();
            vertexes.Add(x);

            x.key = key;
            
            n++;
            last_id++;

            x.id = last_id;

            x.degree = 0;
            x.parent = -1;
            x.child = -1;

            x.left = last_id;
            x.right = last_id;
                        

            if (key > max)
            {
                max = key;
                max_id = last_id;
            }
            
            AddRoot(x);
        }

        // Removing FibVertex from root list
        private void RemoveRoot(int x)
        {
            int root_id = new int();

            for (int i = 0; i < roots.Count; i++)
            {
                if (roots[i] == x) root_id = i;
            }

            FibHeapVertex l = vertexes[vertexes[roots[root_id]].left];
            FibHeapVertex r = vertexes[vertexes[roots[root_id]].right];

            l.right = r.id;
            r.left = l.id;

            roots.Remove(root_id);
        }

        // adding FibVertex to root list
        private void AddRoot(FibHeapVertex x)
        {
            roots.Add(x.id);

            if (roots.Count > 1)
            {
                vertexes[roots[0]].left = x.id;

                x.right = vertexes[roots[0]].id;

                vertexes[roots[roots.Count - 2]].right = x.id;

                x.left = vertexes[roots[roots.Count - 2]].id;
            }
        }

        // debug only - printing roots
        public void PrintRoots()
        {
            foreach (int id in roots)
            {
                Console.WriteLine("(" + vertexes[id].left + ") [" + id + ":" + vertexes[id].key + "] (" + vertexes[id].right + ")");
                
            }
        }

        // debug only - printing max and max_id
        public void PrintHeapData()
        {
            Console.WriteLine("max: " + max + ", max_id: " + max_id);
        }

        // Union (actually adding H2 to H1(this))
        public void Union(FibHeap heap)
        {
            int frozen = n;

            // copy all vertex with changing all id
            foreach (FibHeapVertex x in heap.vertexes)
            {
                vertexes.Add(x);
                x.id += frozen;
                n++;
                last_id++;

                // parent
                if (x.parent != -1) x.parent += frozen;

                // child
                if (x.child != -1) x.child += frozen;

                // left
                x.left += frozen;
                
                // right
                x.right += frozen;
            }


            // copy roots
            foreach (int id in heap.roots)
            {
                AddRoot(vertexes[id+frozen]);
            }

            // max
            if (heap.max > max)
            {
                max = heap.max;
                max_id = heap.max_id+frozen;
            }
        }
 
        public void MakeSon(int wannabe_parent_id, int wannabe_son_id)
        {
            FibHeapVertex x = vertexes[wannabe_parent_id];
            FibHeapVertex y = vertexes[wannabe_son_id];

            if (x.child == -1)
            {
                x.child = y.id;
                y.parent = x.id;
                x.degree = 1;
            }
            else
            {
                FibHeapVertex son = vertexes[x.child];
                FibHeapVertex leftson = vertexes[son.left];

                son.left = y.id;
                y.left = leftson.id;
                leftson.right = y.id;
                y.right = son.id;

                x.degree++;
                y.parent = x.id;
            }
        }

        public void Link(int x_id, int y_id)
        {
            RemoveRoot(y_id);
            MakeSon(x_id, y_id);

            FibHeapVertex y = vertexes[y_id];
            y.mark = false;
        }


        public void MoveSonsToRoots(int x_id)
        {
            FibHeapVertex x = vertexes[x_id];
            if (x.child >= 0)
            {
                FibHeapVertex firstson = vertexes[x.child];

                List<int> sons = new List<int>();

                sons.Add(firstson.id);

                FibHeapVertex nextson = vertexes[firstson.left];

                while (!sons.Contains(nextson.id))
                {
                    sons.Add(nextson.id);
                    nextson = vertexes[nextson.left];
                }

                foreach (int id in sons)
                {
                    FibHeapVertex v = vertexes[id];
                    v.parent = -1;
                    roots.Add(v.id);
                }
            }
            x.child = -1;
        }

        public void Consolidate()
        {
            if (roots.Count > 2)
            {
                double count = roots.Count / 4;
                int loops = Convert.ToInt32(Math.Floor(count));

                int e = 0;

                for (int i = 0; i < loops; i++)
                {
                    FibHeapVertex x = vertexes[roots[i * 2 + e]];
                    FibHeapVertex y = vertexes[roots[i * 2 + 1 + e]];

                    if (x.key > y.key)
                    {
                        Link(x.id, y.id);

                        if (x.key > max)
                        {
                            max = x.key;
                            max_id = x.id;
                        }
                    }
                    else
                    {
                        Link(y.id, x.id);

                        if (y.key > max)
                        {
                            max = y.key;
                            max_id = y.id;
                        }
                    }
                    e++;
                }

                for (int i = loops; i < roots.Count; i++)
                {
                    FibHeapVertex x = vertexes[roots[i]];
                    if (x.key > max)
                    {
                        max = x.key;
                        max_id = x.id;
                    }
                }
            }
        }

        public int ExtractMax()
        {
            int retmax = max;
            if (max != -1)
            {
                MoveSonsToRoots(max_id);
                RemoveRoot(max_id);

                FibHeapVertex x = vertexes[max_id];

                if (x.left == x.id)
                {
                    max = -1;
                }
                else
                {
                    max = x.left;
                }

                Consolidate();
                n--;
            }

            return retmax;
        }
    }

    class FibHeapVertex
    {
        public int key { get; set; }
        public int parent { get; set; }
        public int child { get; set; }
        public int left { get; set; }
        public int right { get; set; }
        public bool mark { get; set; }
        public int degree { get; set; }
        public int id { get; set; }
    }
}
