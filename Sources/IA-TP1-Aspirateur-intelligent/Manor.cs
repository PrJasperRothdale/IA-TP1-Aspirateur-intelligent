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

        private List<string> events;

        private Random random = new Random();

        private Manor()
        {
            floor = new Floor(GRID_SIZE);
            schmutzfabrik = new Schmutzfabrik(probmatrixGenerator());
            juwelfabrik = new Juwelfabrik(probmatrixGenerator());
            aspirateur = new Aspirateur();
            events = new List<string>();
            events.Add("nothing");
            events.Add("nothing");
            events.Add("nothing");
            events.Add("nothing");

        }

        public static Manor getInstance()
        {
            if (instance == null)
            {
                instance = new Manor();
            }
            return instance;
        }

        //Boucle principale
        public void setAlive(string strategy)
        {
            schmutzThread = new Thread(new ThreadStart(schmutzfabrikThread));
            juwelThread = new Thread(new ThreadStart(juwelfabrikThread));

            if (strategy != "n")
            {
                vacThread = new Thread(new ThreadStart(vaccumThreadInforme));
            }
            else
            {
                vacThread = new Thread(new ThreadStart(vaccumThread));
            }

            schmutzThread.Start();
            Thread.Sleep(2000);
            juwelThread.Start();
            vacThread.Start();

            while (true)
            {
                Console.Clear();
                
                if (strategy != "n")
                {
                    Console.WriteLine("Performance : " + aspirateur.getPerformance());
                }
                else
                {
                    Console.WriteLine("Performance : " + floor.tick());
                }
                printFloorState();
                printActions();
                Thread.Sleep(100);

            }
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

        public void pushEvent(string _event)
        {
            events.Insert(0, _event);
            if (events.Count > 4)
            {
                events = events.GetRange(0, 4);
            }
        }

        // Thread pour la poussiere
        private void schmutzfabrikThread()
        {
            while(true)
            {
                schmutzfabrik.dirty(floor);

                Thread.Sleep(4000);
            }
        }

        //Thread pour les bijoux
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
            }
        }

        //Thread agent
        private void vaccumThreadInforme()
        {
            while (true)
            {
                aspirateur.wakeInforme();
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

        private void printActions()
        {
            Console.WriteLine("Actors did : " + events[0]);
            Console.WriteLine("Actors did : " + events[1]);
            Console.WriteLine("Actors did : " + events[2]);
            Console.WriteLine("Actors did : " + events[3]);
        }

        public int[,] getFloorState()
        {
            return floor.getState();
        }


        public Floor getFloor()
        {
            return floor;
        }

    }
}
