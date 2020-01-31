using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent.Modelisation
{
    class Node
    {
        private int[,] state;
        private int depth;
        private int pathcost;
        private Node parent;
        private List<Node> childs;

        public Node(int[,] s, int d, int pc, Node p)
        {
            state = s;
            depth = d;
            pathcost = pc;
            parent = p;
            childs = new List<Node>();
        }
        public Node(int[,] s, int d, int pc)
        {
            state = s;
            depth = d;
            pathcost = pc;
            childs = new List<Node>();
        }

        public void addChild(Node child)
        {
            childs.Add(child);
        }

        public void removeChild(Node child)
        {
            childs.Remove(child);
        }
    }
}
