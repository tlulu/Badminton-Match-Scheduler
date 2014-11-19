using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BadmintonQ.DAL;
using BadmintonQ.Models;
using BadmintonQ.Helper;

namespace BadmintonQ.Controllers
{
    public class ScheduleController : Controller
    {
        private BadmintonContext db = new BadmintonContext();
        private List<Player> players = new List<Player>();
        TimeSlot ts = new TimeSlot();

        /* Returns a list of players
         * that have signed in 
         */
        public List<PlayerModels> getActivePlayers(List<ActivePlayer> activePlayers)
        {
            List<PlayerModels> temp = new List<PlayerModels>();
            foreach (var p in activePlayers)
            {
                temp.Add(db.Players.SingleOrDefault(x => x.ID == p.PlayerID));
            }

            return temp;
        }

        /* Clears the session variable 
         * Allows for new players to be on the court
         */
        public ActionResult Rotation()
        {
            Session["PL"] = null;
            return RedirectToAction("Index");
        }

        /* Displays the schedule page
         * Stores current matches (TimeSlot) using a session variable
         * After a rotation, a new list of players is generated and stored in the session variable
         */
        public ActionResult Index()
        {

            if (Session["PL"] != null)
            {
                ts = (TimeSlot)Session["PL"];
            }
            else
            {
                ScheduleHelper.Rotate(db, players, ts);
                Session["PL"] = ts;
            }

            return View(ts);
        }

    }
}