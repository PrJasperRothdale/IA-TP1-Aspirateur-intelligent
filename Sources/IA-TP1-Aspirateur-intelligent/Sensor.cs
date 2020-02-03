using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    class Sensor
    {
        
        public Sensor()
        {
            
        }

        public int[,] getSurroundings()
        {
            return Manor.getInstance().getFloorState();
        }
    }
}
