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
        private int performance = 0;

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




        // Awake the vaccum, which analyze the environment and add tasks to do in its tasklist to achieve desire in desire matrix
        public void wakeInforme()
        {
            // Get the state of the room
            int[,] state = sensor.getSurroundings();
            Floor fl = new Floor(state, 0);
            int[] vacXY = fl.getAspXY();
            List<int[,]> desireStates = calculateDesireState();
            

            // Get the path : tasklist
            tasklist = brain.searchInforme(state, desireStates);

            // Execute the queue
            int t = tasklist.Count;
            for (int i = 0; i < t; i++)
            {
                string dq = tasklist.Dequeue();

                // Performance update
                switch (dq)
                {
                    case "clean":
                        if (brain.isJewelDirt(state, vacXY) == 3 || brain.isJewelDirt(state, vacXY) == 7)
                        {
                            Console.WriteLine("Aspirate a jewel");
                            performance = -1000;
                        }
                        else
                        {
                            performance += 10;
                        }
                        break;

                    case "pickup":
                        if (brain.isJewelDirt(state, vacXY) == 5)
                        {
                            Console.WriteLine("Pickup dirt");
                            performance = -100;
                        }
                        else
                        {
                            performance += 10;
                        }
                        break;

                    default:
                        break;
                }

                actors.execute(dq);
            }
        }

        public int isJewelDirt(int[,] state)
        {

            Floor fl = new Floor(state, 0);

            int[] aspXY = fl.getAspXY();

            switch (state[aspXY[0], aspXY[1]])
            {
                // Vaccum + jewel
                case 3:
                    return 3;

                // Vaccum + dirt
                case 5:
                    return 5;

                // Vaccum + dirt + jewel
                case 7:
                    return 7;

                default:
                    return 0;
            }
        }


        // Create 9 differents desire states : 9 positions of vaccum in a clean room
        private List<int[,]> calculateDesireState()
        {
            List<int[,]> desireStates = new List<int[,]>();
            int gridsize = 5;

            int[,] desire = new int[gridsize, gridsize];

            // Copy desire matrix
            int[,] copyDesire = (int[,])desire.Clone();

            // Add 1 at every different cell and add it to the list 
            for (int i = 0; i < gridsize; i++)
            {
                for (int j = 0; j < gridsize; j++)
                {
                    desire[i, j] = 1;                          // Add 1
                    desireStates.Add(desire);                  // Add the new desire state in the list
                    desire = (int[,])copyDesire.Clone();      // Reset desire
                }

            }

            return desireStates;
        }

        public int getPerformance()
        {
            return performance;
        }
    }
}
