﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent.Actions
{
    class MoveUp : Action
    {
        private int cost;

        public MoveUp()
        {
            cost = 5;
        }

        public void enact(Floor floor, int[] vacXY)
        {
            floor.vaccumout(vacXY);
            int[] ncoo = vacXY;
            if (!(vacXY[0] == 0))
            {
                ncoo[0] -= 1;
            }
            
            floor.vaccumin(ncoo);
        }

        public void reverse(Floor floor, int[] vacXY)
        {
            floor.vaccumout(vacXY);
            int[] ncoo = vacXY;
            if (!(vacXY[0] == floor.getState().GetLength(0)))
            {
                ncoo[0] += 1;
            }
            
            floor.vaccumin(ncoo);
        }

        public int getCost()
        {
            return cost;
        }
    }
}
