using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    class Schmutzfabrik
    {
        private int[,] probability_matrix;

        private Random random = new Random();

        public Schmutzfabrik(int[,] probmatrix)
        {
            probability_matrix = probmatrix;
        }

        public void dirty(Floor floor)
        {
            int x = random.Next(floor.getRooms().Count);
            int y = random.Next(floor.getRooms()[0].Count);

            if ( random.Next(101) <= probability_matrix[x,y] )
            {
                floor.dirt(x, y);
            }
        }

    }
}
