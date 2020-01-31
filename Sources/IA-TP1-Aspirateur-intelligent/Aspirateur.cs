using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    class Aspirateur
    {
        private Sensor sensor;
        private Actors actors;
        private List<string> tasklist;
        private int[,] state;

        public Aspirateur()
        {
            sensor = new Sensor();
            actors = new Actors();
        }

        public void wake()
        {
            state = sensor.getSurroundings();
            
        }

    }
}
