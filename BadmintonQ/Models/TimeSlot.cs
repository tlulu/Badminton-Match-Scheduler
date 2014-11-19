using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadmintonQ.Models
{
    /* A TimeSlot contains court objects
     * Represents a 15 minute interval of games
     */
    public class TimeSlot
    {
        public List<Court> courts { get; set; }
	    private static int numOfCourts=4; // total number of courts in the gym
	
	    public TimeSlot(){
            courts = new List<Court>();
	    }
	
	    public void addCourt(Court c){
		    courts.Add(c);
	    }
	
	    public static int getNumOfCourts(){
		    return numOfCourts;
	    }
    }
}