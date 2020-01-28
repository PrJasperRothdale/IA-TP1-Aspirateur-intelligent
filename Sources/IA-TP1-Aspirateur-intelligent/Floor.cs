using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    class Floor
    {
        private List<List<Room>> rooms;

        public Floor(int size)
        {
            rooms = new List<List<Room>>();

            for (int i = 0; i < size; i++)
            {
                List<Room> column = new List<Room>();
                for (int j = 0; j < size; j++)
                {
                    column.Add(new Room());
                }

                rooms.Add(column);
            }
            
        }

        public int[,] getState()
        {
            int[,] state = new int[rooms.Count,rooms[0].Count];

            for (int i = 0; i < rooms.Count ; i++)
            {
                for (int j = 0; j < rooms[0].Count; j++)
                {
                    state[i,j] = rooms[i][j].getState();
                    Console.WriteLine("Floor getstate [" + i + "," + j + "] = " + state[i, j]);
                }
                
            }

            return state;
        }

        public List<List<Room>> getRooms()
        {
            return rooms;
        }

        public void dirt(int x, int y)
        {
            rooms[x][y].dirt();
        }

        public void clean(int x, int y)
        {
            rooms[x][y].clean();
        }

        public void jewels(int x, int y)
        {
            rooms[x][y].jewels();
        }

        public void pickup(int x, int y)
        {
            rooms[x][y].pickup();
        }

        public void vaccumin(int x, int y)
        {
            rooms[x][y].vaccumin();
        }
        
        public void vaccumout(int x, int y)
        {
            rooms[x][y].vaccumout();
        }
    }
}
