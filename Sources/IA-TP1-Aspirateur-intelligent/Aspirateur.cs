using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    class Aspirateur
    {
        private Sensor sensor;
        private Actors actors;
        private Brain brain;
        private Queue<string> tasklist;
        private int[,] state;
        private int[,] desire;

        public Aspirateur()
        {
            
            sensor = new Sensor();
            actors = new Actors();
            brain  = new Brain();
            tasklist = new Queue<string>();
            desire = calculateDesire();

        }

        public void wake()
        {
            
            state = sensor.getSurroundings();
            tasklist = brain.search(state, (int[,])desire.Clone());

            //2 actions at a time to avoid do nothing loop
            actors.execute(tasklist.Dequeue());
            actors.execute(tasklist.Dequeue());
            

        }

        private int[,] calculateDesire()
        {
            int gridsize = 5;
            desire = new int[gridsize, gridsize];

            for (int i = 0; i < gridsize; i++)
            {
                for (int j = 0; j < gridsize; j++)
                {
                    desire[i, j] = 0;
                }

            }
            desire[2, 2] = 1;
            return desire;
            
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
