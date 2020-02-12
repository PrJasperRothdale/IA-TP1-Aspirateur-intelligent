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






        public int heuristique(List<(int, int, int)> cooList, int[] aspXY)
        {
            int h = 0;

            int[] vacXY = aspXY;
            int x = vacXY[0];
            int y = vacXY[1];


            for (int i = 0; i < cooList.Count; i++)
            {
                // Definition of heuristic 
                h += (Math.Abs(x - cooList[i].Item1) + Math.Abs(y - cooList[i].Item2)) * cooList[i].Item3;
            }
            return h;

        }

        // Test every actions given a node and return only a node with the lowest heuristic
        public List<Modelisation.Node> successionInforme(Modelisation.Node currentNode, List<Modelisation.Node> l)
        {
            // Create
            Floor testingFloor = new Floor(currentNode.getState(), currentNode.getPathcost());

            // Init
            int minH = 10000;
            Floor succFloor = new Floor(currentNode.getState(), currentNode.getPathcost());   // For now, the successor is the root itself
            string lastAct = "";

            // Copy currentNode, needed to store the best node
            Modelisation.Node node = new Modelisation.Node(
                currentNode.getState(),
                currentNode.getVacXY(),
                currentNode.getDepth(),
                currentNode.getPathcost(),
                currentNode.isVisited(),
                currentNode.getLastAction(),
                currentNode.getParent()
            );

            // Get coo
            List<(int, int, int)> cooList = testingFloor.getJewelDirt();

            // We determine the lowest heuristic successor
            foreach (KeyValuePair<string, Action> entry in actions)
            {
                entry.Value.enact(testingFloor, currentNode.getVacXY());

                int h = heuristique(cooList, testingFloor.getAspXY());   // Heuristic

                Modelisation.Node newState = new Modelisation.Node(
                    testingFloor.getState(),
                    testingFloor.getAspXY(),
                    currentNode.getDepth() + 1,
                    minH,
                    false,
                    entry.Key,
                    currentNode
                );

                // If the heuristic is below the actual min
                if (h < minH)
                {
                    minH = h;                                       // We store this heuristic
                    //succFloor = new Floor(testingFloor.getState()); // We store this floor
                    lastAct = entry.Key;                            // And the best action
                    l.Add(node);                                    // Don't forget to add the previous best node in the list
                    node = newState;
                }
                else
                {
                    l.Add(newState);
                }

                testingFloor.reset();                               // Return to the initial state
            }

            // Add the best node last
            l.Add(node);

            return l;

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
