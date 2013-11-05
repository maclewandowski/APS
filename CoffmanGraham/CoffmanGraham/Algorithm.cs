using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffmanGraham
{
    class Algorithm
    {
        public bool isPrime(int[,] conseq, int pretender, int size)
        {
            for (int i = 0; i < size; i++)
            {
                if (conseq[pretender,i] != 0) return false;
            }
            return true;
        }

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

        public bool gotLabel(int[] labels, int pretender)
        {
            if (labels[pretender] == 0) return false;
            else return true;
        }

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
                    if(flag) S.Add(i);
                }
            }

            return S;
        }

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
    }
}
