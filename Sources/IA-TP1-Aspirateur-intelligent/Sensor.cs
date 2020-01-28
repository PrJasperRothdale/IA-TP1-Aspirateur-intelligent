using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    class Sensor
    {
        private Manor manor;
        public Sensor()
        {
            manor = Manor.getInstance();
        }

        public int[,] getSurroundings()
        {
            return manor.getFloorState();
        }
    }
}
