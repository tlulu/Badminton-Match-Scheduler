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
        private List<Player> new_players = new List<Player>();
        TimeSlot ts = new TimeSlot();


        public List<PlayerModels> getActivePlayers(List<ActivePlayer> activePlayers)
        {
            List<PlayerModels> temp = new List<PlayerModels>();
            foreach (var p in activePlayers)
            {
                temp.Add(db.Players.SingleOrDefault(x => x.ID == p.PlayerID));
            }

            return temp;
        }

        public ActionResult Clear()
        {
            foreach (var p in db.ActivePlayers)
            {
                p.Waits = 0;
                p.OnCourt = false;
                p.GamesPlayed = 0;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Rotation()
        {
            ScheduleHelper.Rotate(db, players, ts);
            return RedirectToAction("Index");
        }


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