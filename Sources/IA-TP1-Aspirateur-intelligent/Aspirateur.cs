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
            desire = calculateDesire();

        }

        public void wake()
        {
            state = sensor.getSurroundings();

            string line;

            /*
            Console.WriteLine("In aspirateur");
            Console.WriteLine("* -  -  -  -  -  *");

            for (int i = 0; i < state.GetLength(0); i++)
            {
                line = "|";
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    line += ' ' + state[i, j].ToString() + ' ';
                }

                line += '|';

                Console.WriteLine(line);
            }

            Console.WriteLine("* -  -  -  -  -  *");
            */

            tasklist = brain.search(state, desire);
            actors.execute(tasklist.Dequeue());
        }

        private int[,] calculateDesire()
        {
            desire = new int[5, 5];

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    desire[i, j] = 0;
                }

            }
            desire[0, 0] = 1;
            return desire;
            
        }

    }
}
