using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    class Accountant
    {
        private int account;
        private int rewardtick;

        public Accountant()
        {
            account = 0;
            rewardtick = 25;
        }

        public Accountant(int initialamount)
        {
            account = initialamount;
            rewardtick = 25;
        }

        public void pay(int cost)
        {
            account -= cost;
        }

        public void credit(int pay)
        {
            account += pay;
        }

        public void increaseTick(int x)
        {
            rewardtick += x;
        }

        public void decreaseTick(int x)
        {
            rewardtick -= x;
        }

        public void tick()
        {
            account += rewardtick;
        }


        public int getCount()
        {
            return account;
        }

    }
}
