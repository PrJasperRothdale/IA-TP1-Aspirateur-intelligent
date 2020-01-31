using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace IA_TP1_Aspirateur_intelligent
{
    public sealed class Manor
    {
        private static int GRID_SIZE = 5;

        private static Manor instance;
        private static Floor floor;
        private static Schmutzfabrik schmutzfabrik;
        private static Juwelfabrik juwelfabrik;
        
        private int[] aspXY;

        private Random random = new Random();

        private Manor()
        {
            floor = new Floor(GRID_SIZE);
            schmutzfabrik = new Schmutzfabrik(probmatrixGenerator());
            juwelfabrik = new Juwelfabrik(probmatrixGenerator());
        }

        public static Manor getInstance()
        {
            if (instance == null)
            {
                instance = new Manor();
            }
            return instance;
        }

        private int[,] probmatrixGenerator()
        {
            int x = floor.getRooms().Count;
            int y = floor.getRooms()[0].Count;

            int[,] probmatrix = new int[x,y];

            for (int i = 0; i < probmatrix.GetLength(0); i++)
            {
                for (int j = 0; j < probmatrix.GetLength(1); j++)
                {
                    probmatrix[i, j] = random.Next(101);
                }
            }

            return probmatrix;
        }

        private void schmutzfabrikThread()
        {
            while(true)
            {
                schmutzfabrik.dirty(floor);

                Thread.Sleep(2000);
            }
        }

        private void juwelfabrikThread()
        {
            while (true)
            {
                juwelfabrik.drop(floor);

                Thread.Sleep(2000);
            }
        }

        public void setAlive()
        {
            Thread schmutzThread = new Thread(new ThreadStart(schmutzfabrikThread));
            Thread juwelThread = new Thread(new ThreadStart(juwelfabrikThread));

            schmutzThread.Start();
            juwelThread.Start();

            while (true)
            {
                printFloorState();
                Thread.Sleep(1000);
            }
        }

        private void printFloorState()
        {
            int[,] state = floor.getState();

            string line;

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
        }

        public int[,] getFloorState()
        {
            return floor.getState();
        }


        public Floor getFloor()
        {
            return floor;
        }

        public int[] getAspXY()
        {
            if (((floor.getRooms()[aspXY[0]][aspXY[1]].getState() % 4) % 2) == 1)
            {
                return aspXY;
            }

            else
            {
                for (int i = 0; i < floor.getRooms().Count; i++)
                {
                    for (int j = 0; j < floor.getRooms()[0].Count; j++)
                    {
                        if (((floor.getRooms()[i][j].getState() % 4) % 2) == 1)
                        {
                            aspXY[0] = i;
                            aspXY[1] = j;
                            return aspXY;
                        }
                    }
                }
            }

            throw new Exception("Did not find the vaccum");

        }
    }
}
