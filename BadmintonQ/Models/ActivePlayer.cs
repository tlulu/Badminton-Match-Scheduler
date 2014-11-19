using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BadmintonQ.Models
{
    /*View Model for active players */
    public class ActivePlayer   
    {
        [Key]
        public int ID { get; set; }
        public int PlayerID { get; set; }
        public int Waits { get; set; }
        public int GamesPlayed { get; set; }
        public bool OnCourt { get; set; }
    }
}