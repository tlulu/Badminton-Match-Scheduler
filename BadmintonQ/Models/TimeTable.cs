using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadmintonQ.Models
{
    /* A TimeTable is the schedule of matches for the entire session
     * Contains a list of TimeSlot objects
     * OBSOLETE DUE TO DIFFERENT IMPLEMENTATION
     */
    public class TimeTable
    {
        public List<TimeSlot> times { get; set; }

        public TimeTable(){
            times=new List<TimeSlot>();
        }

        public void addTimeSlot(TimeSlot ts)
        {
            times.Add(ts);
        }
    }
}