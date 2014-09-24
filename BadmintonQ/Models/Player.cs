using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadmintonQ.Models
{
    public class Player         //View model for each player
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Level { get; set; }
        public string Gender { get; set; }
        public Boolean Paid { get; set; }
        public string Preference { get; set; }
        public Boolean Active { get; set; }
        public int Waits { get; set; }

        public Boolean OnCourt { get; set; }
        public int GamesPlayed { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public Player()
        {
            GamesPlayed = 0;
            OnCourt = false;
        }
    }
}