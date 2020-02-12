using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    class Brain
    {
        private Problem problem;
        private List<Modelisation.Node> tree_fs;
        private List<Modelisation.Node> tree_fg;
        private List<Modelisation.Node> tree;



        public Brain()
        {
            problem = new Problem();
            tree_fs = new List<Modelisation.Node>();
            tree_fg = new List<Modelisation.Node>();
            tree = new List<Modelisation.Node>();

        }


        private Queue<string> generateTasklist(Modelisation.Node ns, Modelisation.Node ng)
        {
            List<string> actions = new List<string>();

            while(ns.getLastAction() != "root" )
            {
                actions.Insert(0, ns.getLastAction());
                ns = ns.getParent();
            }

            while(ng.getLastAction() != "goal")
            {
                actions.Add(ng.getLastAction());
                ng = ng.getParent();
            }


            Queue<string> queue = new Queue<string>(actions);

            return queue;
        }

        public Queue<string> search(int[,] initstate, int[,] desiredstate)
        {
            Modelisation.Node rootn = new Modelisation.Node(
                    initstate,
                    new[] { 0, 0 },
                    0,
                    0,
                    false,
                    "root"
                );
            Modelisation.Node goaln = new Modelisation.Node(
                    desiredstate,
                    new[] { 0, 0 },
                    0,
                    0,
                    false,
                    "goal"
                );
            tree_fs.Clear();
            tree_fg.Clear();


            tree_fs.Add(rootn);
            tree_fg.Add(goaln);


            int borderindex = 0;

            while (true)
            {
                

                Dictionary<string, Modelisation.Node> s_successors = problem.succession(tree_fs[borderindex]);
                
                Dictionary<string, Modelisation.Node> g_successors = problem.retrosuccession(tree_fg[borderindex]);



                foreach (KeyValuePair<string,Modelisation.Node> entry in s_successors)
                {
                    Modelisation.Node[] x = problem.isPresent(entry.Value, tree_fg.GetRange(0, borderindex));
                    if ( x != null )
                    {
                        return generateTasklist(x[0], x[1]);
                    }
                    tree_fs.Add(entry.Value);
                    
                }


                foreach(KeyValuePair<string, Modelisation.Node> entry in g_successors)
                {
                    Modelisation.Node[] x = problem.isPresent(entry.Value, tree_fs.GetRange(0, borderindex));
                    if ( x != null)
                    {
                        return generateTasklist(x[1], x[0]);
                    }
                    tree_fg.Add(entry.Value);
                }

                borderindex++;
            }

        }

        public Queue<string> searchInforme(int[,] initstate, List<int[,]> desireStates)
        {

            Queue<string> tasklist = new Queue<string>();

            // First check if initstate is one of our desireStates
            //*
            foreach (int[,] d in desireStates)
            {
                // If one of them is equal, then no need to search a tasklist
                if (isArrayEqual(initstate, d))
                {
                    // Stop method
                    Queue<string> q = new Queue<string>();
                    return q;
                }
            }
            //*/

            // Clear tree
            tree.Clear();
            tasklist.Clear();

            // Create a root node with initial state
            Modelisation.Node root = new Modelisation.Node(
                    initstate,      // initial state
                    new[] { 0, 0 },          // vacXY
                    0,              // depth
                    100,            // heuristic
                    false,          // visited
                    "nothing"          // lastaction
                );

            tree.Add(root);

            // Represent index of exploration : here it's Breadth first search
            //int borderindex = 0;

            List<Modelisation.Node> succ = new List<Modelisation.Node>();

            bool b = true;

            while (b)
            {
                // We check if the last node state of tree is on dirt or jewel
                //Floor f = new Floor(tree[borderindex].getState());
                int d = isJewelDirt(tree[tree.Count - 1].getState(), tree[tree.Count - 1].getVacXY());

                // If the vaccum is on jewel or dirt
                if (d != 0)
                {
                    switch (d)
                    {
                        case 3:
                            tasklist.Enqueue("pickup");
                            return tasklist;
                        case 5:
                            tasklist.Enqueue("clean");
                            return tasklist;

                        case 7:
                            tasklist.Enqueue("pickup");
                            tasklist.Enqueue("clean");
                            return tasklist;
                        default:
                            b = false;
                            break;
                    }

                }

                // If not
                else
                {
                    tree = problem.successionInforme(tree[tree.Count - 1], tree); // Get successor
                }

                tasklist = generatetasklist(tree);

            }

            return tasklist;

        }

        // Allow to know if the vaccum is on a cell with dirt or/and jewel
        public int isJewelDirt(int[,] state, int[] aspXY)
        {
            int[] vacXY = aspXY;
            int x = vacXY[0];
            int y = vacXY[1];

            switch (state[x, y])
            {
                // Vaccum + jewel
                case 3:
                    return 3;

                // Vaccum + dirt
                case 5:
                    return 5;

                // Vaccum + dirt + jewel
                case 7:
                    return 7;

                default:
                    return 0;
            }
        }

        // Generate tasklist from a node list
        public Queue<string> generatetasklist(List<Modelisation.Node> l)
        {
            Queue<string> tasklist = new Queue<string>();

            if (l.Count != 0)
            {
                Modelisation.Node node = l[l.Count - 1];

                // While the node is not the root node, we add its lastaction to the tasklist
                while (node.getLastAction() != "nothing")
                {
                    tasklist.Enqueue(node.getLastAction());
                    node = node.getParent();
                }

                //tasklist.Enqueue(node.getLastAction());

            }

            return tasklist;
        }


        private bool isArrayEqual(int[,] a, int[,] b)
        {
            if ((a.GetLength(0) != b.GetLength(0)) || (a.GetLength(1) != b.GetLength(1)))
            {
                return false;
            }
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (a[i, j] != b[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
