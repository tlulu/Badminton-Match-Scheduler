using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BadmintonQ.Models
{
    public class PlayerModels   //An entity of the Players table
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Level { get; set; }
        public string Gender { get; set; }
        public Boolean Paid { get; set; }
        public string Preference { get; set; }
        public Boolean Active { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}