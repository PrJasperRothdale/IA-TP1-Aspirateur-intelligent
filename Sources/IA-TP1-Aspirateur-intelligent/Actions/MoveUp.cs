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
            int[] ncoo = vacXY;
            if (!(vacXY[1] == 0))
            {
                ncoo[1] -= 1;
            }
            floor.vaccumout(vacXY);
            floor.vaccumin(ncoo);
        }

        public void reverse(Floor floor, int[] vacXY)
        {
            int[] ncoo = vacXY;
            if (!(vacXY[1] == floor.getState().GetLength(1)))
            {
                ncoo[1] += 1;
            }
            floor.vaccumout(vacXY);
            floor.vaccumin(ncoo);
        }

        public int getCost()
        {
            return cost;
        }
    }
}
