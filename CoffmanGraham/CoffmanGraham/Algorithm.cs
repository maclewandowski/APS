﻿/*
 * APS 
 * CoffmanGraham
 * 
 * Maciej Lewandowski
 * Encrypted.pl
 * maciej@encrypted.pl

 * blabla

 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffmanGraham
{
    class Algorithm
    {
        // is vertex able to be the first one?
        public bool isPrime(int[,] conseq, int pretender, int size)
        {
            for (int i = 0; i < size; i++)
            {
                if (conseq[pretender,i] != 0) return false;
            }
            return true;
        }

        // add label to specific vertex
        public int[] giveLabel(int[] labels, int id)
        {
            int count = new int();
            for (int i = 0; i < labels.Length; i++)
            {
                if(labels[i]!=0) count++;
            }
            labels[id] = count+1;
            return labels;
        }

        // checking if vertex already have any label
        public bool gotLabel(int[] labels, int pretender)
        {
            if (labels[pretender] == 0) return false;
            else return true;
        }

        // List (collection) of all vertex, which DONT HAVE labels and their conseq DO HAVE label
        public List<int> findS(int[,] conseq, int[] labels, int size)
        {
            List<int> S = new List<int>();

            for (int i = 0; i < size; i++)
            {
                if(!gotLabel(labels,i))
                {
                    bool flag = new bool();
                    flag = true;
                    for (int j = 0; j < size; j++)
                    {
                        if((conseq[i,j]!=0)&&(!gotLabel(labels,j))) flag = false;
                    }

                    // flag is now true only if all conseq of vertex HAVE labels
                    if(flag) S.Add(i);
                }
            }

            return S;
        }

        // List labels of all conseq of given vertex (id as vertex)
        public List<int> findT(int[,] conseq, int[] labels, int size, int id)
        {
            List<int> T = new List<int>();

                    for (int j = 0; j < size; j++)
                    {
                        if (conseq[id, j] != 0)
                        {
                            T.Add(labels[j]);
                        }
                    }
            return T;
        }

        // one loop of finding S, than T for all and collecting as ST, 
        // spliting, searching, merge and finaly adding label for one vertex
        public int[] processNextLabel(int[,] conseq, int[] labels, int size)
        {  
            List<int> S = findS(conseq, labels, size);

            // TS if List of T Lists for every S
            List<List<int>> TS = new List<List<int>>();

            foreach (int vertex in S)
            {
                List<int> T = findT(conseq, labels, size, vertex);
                TS.Add(T);
            }


            int listSize = S.Count;
            
            // Reverse sort of T
            int maxListLenght = new int();

            for (int i = 0; i < listSize; i++)
            {
                TS[i].Sort();
                TS[i].Reverse();

                // btw we are finding the max amount of labels in T
                if (TS[i].Count > maxListLenght) maxListLenght = TS[i].Count;
            }

            // OK - this one could be tricky or not
            // comparing strings sux bad
            // so why dont we convert our numeric string to same format ints?
            List<int> intTS = new List<int>();

            for (int i = 0; i < listSize; i++)
            {
                string pseudoInt = "";

                for (int j = 0; j < maxListLenght; j++)
                {
                    if (j < TS[i].Count) pseudoInt += TS[i][j];

                    // adding 0, becouse we need same lenght to be able to compare those "strings" like numbers
                    else pseudoInt += "0";
                }

                intTS.Add(Convert.ToInt32(pseudoInt));
            }
            
            // we need clone, becouse if we sort without it we will loose index perm.
            // WE CAN NOT USE:
            // List<int> cloneTS = intTT;
            // becouse this will bind clone, and we will sort it as well as intTS
            List<int> cloneTS = new List<int>();

            for (int i = 0; i < listSize; i++)
            {
                cloneTS.Add(intTS[i]);
            }

            // now we can "compare alphabeticly" in simple Sort.
            intTS.Sort();

            // pretender is vertex that possibly will have label now
            int pretender = new int();

            for (int i = 0; i < listSize; i++)
            {
                // intTS[0] is the first vertex - we need to give it label
                // but there is not option to find out it index, so lets do this Turbo Pascal way
                if (intTS[0] == cloneTS[i]) pretender = i;
            }
            
            labels = giveLabel(labels, S[pretender]);

            return labels;
        }

        public List<int[]> algorithmHu(int[,] conseq, int[] labels, int size, int m)
        {
            List<int>[] P = new List<int>[size];
            List<int[]> Result = new List<int[]>();
            List<int> Done = new List<int>();

            // lets make array of list
            // lists will contains all pre's
            for (int i = 0; i < size; i++)
            {
                List<int> Q = new List<int>();

                for (int j = 0; j < size; j++)
                {
                    // care [j,i] not [i,j] its pre not conseq
                    if (conseq[j, i] != 0) Q.Add(j);
                }

                P[i] = Q;
            }

            // while there are still some vertex to process
            while (Done.Count < size)
            {
                // List of vertex in this time quantum, where pre's amount = 0
                List<int> Free = new List<int>();

                for (int i = 0; i < size; i++)
                {
                    if ((P[i].Count == 0)&&(Done.IndexOf(i) == -1)) Free.Add(i);
                }
                
                // get m first from Free and "process" them
                int[] tmp = new int[m];
                for (int i = 0; i < m; i++)
                {
                    if (i < Free.Count)
                    {
                        tmp[i] = Free[i];
                        Done.Add(Free[i]);
                    }
                    else
                    {
                        tmp[i] = -1;
                    }
                }

                // add array to Result;
                Result.Add(tmp);
                
                // delete all in Done from P[i]
                for (int i = 0; i < size; i++)
                {
                    foreach (int done in Done)
                        P[i].Remove(done);
                }
            }
            
            
            return Result;
        }
    }
}
