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
        private Floor floor;
        private static Aspirateur aspirateur;
        private static Schmutzfabrik schmutzfabrik;
        private static Juwelfabrik juwelfabrik;

        private Thread schmutzThread;
        private Thread juwelThread;
        private Thread vacThread;

        private Random random = new Random();

        private Manor()
        {
            floor = new Floor(GRID_SIZE);
            schmutzfabrik = new Schmutzfabrik(probmatrixGenerator());
            juwelfabrik = new Juwelfabrik(probmatrixGenerator());
            aspirateur = new Aspirateur();
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
            int x = floor.getState().GetLength(0);
            int y = floor.getState().GetLength(1);

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

                Thread.Sleep(4000);
            }
        }

        private void juwelfabrikThread()
        {
            while (true)
            {
                juwelfabrik.drop(floor);

                Thread.Sleep(4000);
            }
        }

        private void vaccumThread()
        {
            while(true)
            {
                aspirateur.wake();

                ///Thread.Sleep(1000);
            }
        }

        public void setAlive()
        {
            schmutzThread = new Thread(new ThreadStart(schmutzfabrikThread));
            juwelThread = new Thread(new ThreadStart(juwelfabrikThread));
            vacThread = new Thread(new ThreadStart(vaccumThread));

            schmutzThread.Start();
            Thread.Sleep(2000);
            juwelThread.Start();
            vacThread.Start();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Performance : " + floor.tick());
                printFloorState();
                Thread.Sleep(500);
                
            }
        }

        private void printFloorState()
        {

            /*
               dirt | jewels | vaccum === > d|j|v

                djv
                --- = 0
                --v = 1
                -j- = 2
                -jv = 3
                d-- = 4
                d-v = 5
                dj- = 6 
                djv = 7

             */
            int[,] state = floor.getState();

            string line;

            Console.WriteLine("* -  -  -  -  - *");

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

            Console.WriteLine("* -  -  -  -  - *");
        }

        public int[,] getFloorState()
        {
            return floor.getState();
        }


        public Floor getFloor()
        {
            return floor;
        }

        public int[,] getAspDesire()
        {
            return aspirateur.getDesire();
        }

        public bool isArrayEqual(int[,] a, int[,] b)
        {
            if ( (a.GetLength(0) != b.GetLength(0)) || (a.GetLength(1) != b.GetLength(1)))
            {
                return false;
            }

            for (int i=0; i < a.GetLength(0); i++)
            {
                for (int j=0; j < a.GetLength(1); j++)
                {
                    if ( a[i,j] != b[i,j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
