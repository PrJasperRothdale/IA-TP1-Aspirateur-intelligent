using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

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
        private int[,] pstate;

        public Aspirateur()
        {
            
            sensor = new Sensor();
            actors = new Actors();
            brain  = new Brain();
            tasklist = new Queue<string>();
            desire = calculateDesire();
            pstate = null;

        }

        public void wake()
        {
            
            state = sensor.getSurroundings();
            if (pstate == null || pstate != state)
            {
                tasklist = brain.search(state, (int[,])desire.Clone());
            }
            pstate = (int[,])state.Clone();

            //2 actions at a time to avoid do nothing loop
            actors.execute(tasklist.Dequeue());
            Thread.Sleep(500);
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

        public int[,] getDesire()
        {
            return (int[,])desire.Clone();
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
