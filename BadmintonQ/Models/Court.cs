using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadmintonQ.Models
{
    public class Court
    {
        public List<Player> players { get; set; }
        private int maxNum = 4;

        public Court()
        {
            players = new List<Player>();
        }
        public int getPlayerNum()
        {
            int c = 0;
            for (int i = 0; i < maxNum; i++)
            {
                if (players.ElementAt(i)!= null)
                {
                    c++;
                }
            }
            return c;
        }

        public void addPlayer(Player p)
        {
            if (players.Count< maxNum)
            {
                players.Add(p);
            }
                
        }

       
    }
}