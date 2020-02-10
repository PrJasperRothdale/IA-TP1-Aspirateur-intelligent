using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    public class Floor
    {
        private int[,] state;
        private int[,] initialState;

        private int[] aspXY;

        private Accountant accountant;

        public Floor(int size)
        {
            state = new int[size, size];
            initialState = new int[size, size];
            aspXY = new int[2];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    state[i, j] = 0;
                }

            }
            state[2, 2] = 1;
            aspXY = new[] { 2, 2 };
            initialState = (int[,])state.Clone();
            accountant = new Accountant();
        }

        public Floor(int[,] s, int account)
        {
            state = (int[,])s.Clone();
            initialState = (int[,])s.Clone();
            aspXY = new[] { 0, 0 };
            accountant = new Accountant(account);
        }

        public int[,] getState()
        {
            return state;
        }

        public int[,] getInitialState()
        {
            return initialState;
        }

        public int[] getAspXY()
        {
            if (((state[aspXY[0], aspXY[1]] % 4) % 2) == 1)
            {
                return (int[])aspXY.Clone();
            }
            else
            {
                for (int i = 0; i < state.GetLength(0); i++)
                {
                    for (int j = 0; j < state.GetLength(1); j++)
                    {
                        if (((state[i, j] % 4) % 2) == 1)
                        {
                            aspXY[0] = i;
                            aspXY[1] = j;
                            return (int[])aspXY.Clone();
                        }
                    }
                }
            }
        

            throw new Exception("Did not find the vaccum");

        }

        public void dirt(int[] coo)
        {
            
            if ( (state[coo[0], coo[1]] % 4) == state[coo[0], coo[1]] )
            {
                accountant.decreaseTick(5);
                state[coo[0], coo[1]] += 4;
            }
            
        }

        public void clean(int[] coo)
        {
            accountant.pay(20);
            accountant.increaseTick(5);

            if (state[coo[0],coo[1]] != state[coo[0],coo[1]] % 4)
            {
                //accountant.increaseTick(5);
                if (state[coo[0], coo[1]] % 4 != state[coo[0], coo[1]] % 4 % 2)
                {
                    accountant.pay(10000);
                }
            }
            state[coo[0], coo[1]] %= 4;
            state[coo[0], coo[1]] %= 2;
            
        }

        public void jewels(int[] coo)
        {
            int pstate = state[coo[0], coo[1]];

            accountant.decreaseTick(5);

            if ( pstate % 4 == pstate)
            {
                if (pstate % 2 == pstate )
                {
                    //accountant.decreaseTick(5);
                    state[coo[0], coo[1]] += 2;
                }
            }
            else
            {
                pstate %= 4;
                if (pstate % 2 == pstate)
                {
                    //accountant.decreaseTick(5);
                    state[coo[0], coo[1]] += 2;
                }
            }

        }

        public void pickup(int[] coo)
        {
            int pstate = state[coo[0], coo[1]];
            accountant.pay(20);

            accountant.increaseTick(5);
            
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
            accountant.pay(5);

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
            //Console.WriteLine("Moved out of :" + coo[0] + ", " + coo[1]);
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

        public void reset()
        {
            this.state = (int[,])initialState.Clone();
        }

        public int tick()
        {
            accountant.tick();
            return accountant.getCount();
        }

        public int account()
        {
            return accountant.getCount();
        }

    }
}
