﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    public class Floor
    {
        private int[,] state;

        public Floor(int size)
        {
            state = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    state[i, j] = 0;
                }

            }
            
        }

        public Floor(int[,] s)
        {
            state = s;
        }

        public int[,] getState()
        {
            return state;
        }

        public int[] getAspXY()
        {


            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    if (((state[i, j] % 4) % 2) == 1)
                    {
                        int[] aspXY = new int[1];
                        aspXY[0] = i;
                        aspXY[1] = j;
                        return aspXY;
                    }
                }
            }
        

            throw new Exception("Did not find the vaccum");

        }

        public void dirt(int[] coo)
        {
            if ( (state[coo[0], coo[1]] % 4) == state[coo[0], coo[1]] )
            {
                state[coo[0], coo[1]] += 4;
            }
            
        }

        public void clean(int[] coo)
        {
            state[coo[0], coo[1]] %= 4;
        }

        public void jewels(int[] coo)
        {
            int pstate = state[coo[0], coo[1]];

            if ( pstate % 4 == pstate)
            {
                if (pstate % 2 == pstate )
                {
                    state[coo[0], coo[1]] += 2;
                }
            }
            else
            {
                pstate %= 4;
                if (pstate % 2 == pstate)
                {
                    state[coo[0], coo[1]] += 2;
                }
            }

        }

        public void pickup(int[] coo)
        {
            int pstate = state[coo[0], coo[1]];
            
            if ( pstate % 4 == pstate )
            {
                state[coo[0], coo[1]] %= 2;
            }
            else
            {
                pstate %= 4;
                pstate %= 2;
                pstate += 4;
                state[coo[0], coo[1]] = pstate;
            }

        }

        public void vaccumin(int[] coo)
        {
            int pstate = state[coo[0], coo[1]];

            if (pstate % 4 == pstate)
            {
                if (pstate % 2 == pstate)
                {
                    if ( pstate % 1 == pstate )
                    {
                        state[coo[0], coo[1]] += 1;
                    }
                }
                else
                {
                    pstate %= 2;
                    if (pstate % 1 == pstate)
                    {
                        state[coo[0], coo[1]] += 1;
                    }
                }
            }
            else
            {
                pstate %= 4;
                if (pstate % 2 == pstate)
                {
                    if (pstate % 1 == pstate)
                    {
                        state[coo[0], coo[1]] += 1;
                    }
                }
                else
                {
                    pstate %= 2;
                    if (pstate % 1 == pstate)
                    {
                        state[coo[0], coo[1]] += 1;
                    }
                }
            }

        }
        
        public void vaccumout(int[] coo)
        {
            int pstate = state[coo[0], coo[1]];

            if (pstate % 4 == pstate)
            {
                if (pstate % 2 == pstate)
                {
                    state[coo[0], coo[1]] %= 1;
                }
                else
                {
                    state[coo[0], coo[1]] %= 2;
                    state[coo[0], coo[1]] %= 1;
                    state[coo[0], coo[1]] += 2;
                }
            }
            else
            {
                pstate %= 4;
                state[coo[0], coo[1]] %= 4;
                if ( pstate % 2 == pstate)
                {
                    state[coo[0], coo[1]] %= 1;
                }
                else
                {
                    state[coo[0], coo[1]] %= 2;
                    state[coo[0], coo[1]] %= 1;
                    state[coo[0], coo[1]] += 2;
                }
                state[coo[0], coo[1]] += 4;

            }

        }


    }
}
