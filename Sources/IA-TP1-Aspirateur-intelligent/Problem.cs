using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    class Problem
    {
        private int[,] desire;

        private int[,] initialState;
        private Dictionary<string, Action> actions;
        
        public Problem()
        {
            actions.Add("clean", new Actions.Clean());
            actions.Add("movedown", new Actions.MoveDown());
            actions.Add("moveleft", new Actions.MoveLeft());
            actions.Add("moveright", new Actions.MoveRight());
            actions.Add("moveup", new Actions.MoveUp());
            actions.Add("nothing", new Actions.Nothing());
            actions.Add("pickup", new Actions.Pickup());
        }

        public Dictionary<string, Modelisation.Node> succession(Modelisation.Node currentNode)
        {
            Dictionary<string, Modelisation.Node> newStates = new Dictionary<string, Modelisation.Node>();
            
            foreach (KeyValuePair<string, Action> entry in actions)
            {

            }

            return newStates;
        }
    }
}
