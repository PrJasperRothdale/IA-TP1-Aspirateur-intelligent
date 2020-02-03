using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    class Brain
    {
        private Problem problem;
        private List<Modelisation.Node> frontiere_fs;
        private List<Modelisation.Node> frontiere_fg;
        private List<Modelisation.Node> visited_fs;
        private List<Modelisation.Node> visited_fg;


        public Brain()
        {
            problem = new Problem();
            frontiere_fs = new List<Modelisation.Node>();
            frontiere_fg = new List<Modelisation.Node>();
            visited_fs = new List<Modelisation.Node>();
            visited_fg = new List<Modelisation.Node>();

        }


        private Queue<string> generateTasklist(Modelisation.Node ns, Modelisation.Node ng)
        {
            //Modelisation.Node intersection = search(state);
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
                    "root"
                );
            Modelisation.Node goaln = new Modelisation.Node(
                desiredstate,
                new[] { 0, 0 },
                0,
                0,
                "goal"
                );

            frontiere_fs.Clear();
            frontiere_fg.Clear();
            visited_fs.Clear();
            visited_fg.Clear();

            frontiere_fs.Add(rootn);
            frontiere_fg.Add(goaln);

            int caca = 0;

            while (true)
            {
                //Console.WriteLine("Tour " + caca++);
                Dictionary<string, Modelisation.Node> s_successors = problem.succession(frontiere_fs[0]);
                
                Dictionary<string, Modelisation.Node> g_successors = problem.retrosuccession(frontiere_fg[0]);

                visited_fs.Add(frontiere_fs[0]);
                visited_fg.Add(frontiere_fg[0]);

                frontiere_fs.RemoveAt(0);
                frontiere_fg.RemoveAt(0);


                foreach(KeyValuePair<string,Modelisation.Node> entry in s_successors)
                {

                    if (isPresent(entry.Value, visited_fg) != null)
                    {
                        Console.WriteLine("Trouve un truc en A ");
                        Modelisation.Node[] x = isPresent(entry.Value, visited_fg);
                        return generateTasklist(x[0], x[1]);
                    }
                    frontiere_fs.Add(entry.Value);
                    
                }

                Console.ReadLine();

                foreach(KeyValuePair<string, Modelisation.Node> entry in g_successors)
                {
                    if (isPresent(entry.Value, visited_fs) != null)
                    {
                        Console.WriteLine("Trouve un truc en B ");
                        Modelisation.Node[] x = isPresent(entry.Value, visited_fs);
                        return generateTasklist(x[1], x[0]);
                    }
                    frontiere_fg.Add(entry.Value);
                }

            }

        }

        private Modelisation.Node[] isPresent(Modelisation.Node node, List<Modelisation.Node> visited)
        {
            foreach( Modelisation.Node n in visited)
            {

                if (node.getState() == n.getState())
                {
                    Console.WriteLine("TROUVE UNE SOLUTION");
                    return new[] { node, n };
                }
            }
            return null;
        }

    }
}
