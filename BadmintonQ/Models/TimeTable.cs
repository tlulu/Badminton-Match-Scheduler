using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadmintonQ.Models
{
    public class TimeTable
    {
        public List<TimeSlot> times { get; set; }
        public int numOfDoubleCourts = 4;

        public TimeTable(){
            times=new List<TimeSlot>();
        }

        public void addTimeSlot(TimeSlot ts)
        {
            times.Add(ts);
        }
    }
}