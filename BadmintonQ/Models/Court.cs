using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadmintonQ.Models
{
    /*A court contains player objects
     */
    public class Court
    {
        public List<Player> players { get; set; }
        private static int maxNumPlayersOnCourt = 4;

        public Court()
        {
            players = new List<Player>();
        }
        public int getPlayerNum()
        {
            int c = 0;
            for (int i = 0; i < maxNumPlayersOnCourt; i++)
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
            if (players.Count < maxNumPlayersOnCourt)
            {
                players.Add(p);
            }
                
        }

        public static int intGetNumOfPlayersOnCourt(){
            return maxNumPlayersOnCourt;
        }

       
    }
}