﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    class Actors
    {
        private Dictionary<string, Action> actions;

        public Actors()
        {
            actions.Add("clean",new Actions.Clean());
            actions.Add("movedown", new Actions.MoveDown());
            actions.Add("moveleft", new Actions.MoveLeft());
            actions.Add("moveright", new Actions.MoveRight());
            actions.Add("moveup", new Actions.MoveUp());
            actions.Add("nothing", new Actions.Nothing());
            actions.Add("pickup", new Actions.Pickup());
        }
        
        public void execute(string action)
        {
            actions[action].enact(Manor.getInstance().getFloor(), Manor.getInstance().getAspXY());
        }

    }
}
