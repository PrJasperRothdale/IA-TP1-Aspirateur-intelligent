using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    class Problem
    {
        private Dictionary<string, Action> actions;
        private int[,] desire;
        
        public Problem()
        {
            actions = new Dictionary<string, Action>();

            //In cost order for shortest path
            actions.Add("nothing", new Actions.Nothing());
            actions.Add("movedown", new Actions.MoveDown());
            actions.Add("moveleft", new Actions.MoveLeft());
            actions.Add("moveright", new Actions.MoveRight());
            actions.Add("moveup", new Actions.MoveUp());
            actions.Add("pickup", new Actions.Pickup());
            actions.Add("clean", new Actions.Clean());

            //desire = (int[,]) Manor.getInstance().getAspDesire().Clone();
            desire = new int[4, 4];
            desire[2, 2] = 1;
        }

        public Dictionary<string, Modelisation.Node> succession(Modelisation.Node currentNode)
        {
            Dictionary<string, Modelisation.Node> newStates = new Dictionary<string, Modelisation.Node>();

            Floor testingFloor = new Floor(currentNode.getState(), currentNode.getPathcost());

            if (currentNode.getLastAction() == "nothing")
            {
                Modelisation.Node newnode = new Modelisation.Node(
                    testingFloor.getState(),
                    testingFloor.getAspXY(),
                    currentNode.getDepth() + 1,
                    //currentNode.getPathcost() + entry.Value.getCost(),
                    testingFloor.account(),
                    false,
                    "nothing",
                    currentNode
                    );
                newStates.Add("nothing", newnode);
                return newStates;
            }


            foreach (KeyValuePair<string, Action> entry in actions)
            {
                
                entry.Value.enact(testingFloor, testingFloor.getAspXY());
                if (isArrayEqual(testingFloor.getState(), currentNode.getState()) && entry.Key != "nothing" )
                {
                    testingFloor.reset();
                    continue;
                }
                Modelisation.Node newnode = new Modelisation.Node(
                    testingFloor.getState(),
                    testingFloor.getAspXY(),
                    currentNode.getDepth() + 1,
                    //currentNode.getPathcost() + entry.Value.getCost(),
                    testingFloor.account(),
                    false,
                    entry.Key,
                    currentNode
                    ) ;

                newStates.Add(entry.Key, newnode);
                testingFloor.reset();


            }
            

            return newStates;
        }

        public Dictionary<string, Modelisation.Node> retrosuccession(Modelisation.Node currentNode)
        {
            Dictionary<string, Modelisation.Node> newStates = new Dictionary<string, Modelisation.Node>();

            Floor testingFloor = new Floor(currentNode.getState(), currentNode.getPathcost());

            if (currentNode.getLastAction() == "nothing")
            {
                Modelisation.Node newnode = new Modelisation.Node(
                    testingFloor.getState(),
                    testingFloor.getAspXY(),
                    currentNode.getDepth() + 1,
                    //currentNode.getPathcost() + entry.Value.getCost(),
                    testingFloor.account(),
                    false,
                    "nothing",
                    currentNode
                    );
                newStates.Add("nothing", newnode);
                return newStates;
            }


            foreach (KeyValuePair<string, Action> entry in actions)
            {
                entry.Value.reverse(testingFloor, testingFloor.getAspXY());
                if (isArrayEqual(testingFloor.getState(), currentNode.getState()) && entry.Key != "nothing")
                {
                    testingFloor.reset();
                    continue;
                }
                Modelisation.Node newnode = new Modelisation.Node(
                    testingFloor.getState(),
                    testingFloor.getAspXY(),
                    currentNode.getDepth() + 1,
                    //currentNode.getPathcost() + entry.Value.getCost(),
                    testingFloor.account(),
                    false,
                    entry.Key,
                    currentNode
                    );

                newStates.Add(entry.Key, newnode);
                testingFloor.reset();
            }


            return newStates;
        }


        public Modelisation.Node[] isPresent(Modelisation.Node node, List<Modelisation.Node> visited)
        {
            foreach (Modelisation.Node n in visited)
            {
                if (node.getSignature() == n.getSignature())
                {
                    return new[] { node, n };
                }
            }
            return null;
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
