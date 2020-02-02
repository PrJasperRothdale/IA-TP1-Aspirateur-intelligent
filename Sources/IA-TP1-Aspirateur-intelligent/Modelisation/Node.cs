using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent.Modelisation
{
    class Node
    {
        private int[,] state;
        private int[] vacXY;
        private int depth;
        private int pathcost;
        private Node parent;
        private List<Node> childs;

        public Node(int[,] s, int[] aspXY, int d, int pc, Node p)
        {
            state = s;
            vacXY = aspXY;
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

        public int[,] getState()
        {
            return state;
        }

        public int getDepth()
        {
            return depth;
        }

        public int getPathcost()
        {
            return pathcost;
        }

        public int[] getVacXY()
        {
            return vacXY;
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
