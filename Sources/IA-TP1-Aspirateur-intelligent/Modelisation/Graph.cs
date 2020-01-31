using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent.Modelisation
{
    class Graph
    {
        private int popnumber;
        private List<int[,]>[] adjacents;

        public Graph(int n)
        {
            popnumber = n;
            adjacents = new List<int[,]>[popnumber];
        }

        public void addEdge(int n1, int n2)
        {
            adjacents[n1].Add(adjacents[n2]);
        }
    }
}
