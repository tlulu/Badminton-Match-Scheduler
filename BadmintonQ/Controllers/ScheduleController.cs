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
        private List<Player> players= new List<Player>();
        private List<Player> new_players = new List<Player>();


        public List<PlayerModels> getActivePlayers(List<ActivePlayer> activePlayers)
        {
            List<PlayerModels> temp = new List<PlayerModels>();
            foreach (var p in activePlayers)
            {
                temp.Add(db.Players.SingleOrDefault(x => x.ID == p.PlayerID));
            }

            return temp;
        }

        public ActionResult New()
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

        public ActionResult Index()
        {
            foreach (var p in db.ActivePlayers)
            {
                p.OnCourt = false;
                db.SaveChanges();
            }
            List<ActivePlayer> temp = db.ActivePlayers.ToList();
            foreach (var i in temp)
            {
                players.Add(PlayerHelper.ActivePlayertoPlayer(i,db));
            }
            
            //if (Session["PL"] != null)
            //{
            //    players = (List<Player>)Session["PL"];
            //    foreach (var i in players)
            //    {
            //        if (! temp.Any(x => x.ID == i.ID))
            //        {
            //            players.Remove(i);
            //        }
            //    }

            //    foreach (var i in temp)
            //    {
            //        if (!players.Any(x => x.ID == i.ID))
            //        {
            //            new_players.Add(PlayerHelper.PlayerModeltoPlayer(i));
            //        }
            //    }

            //}
            //else
            //{
            //    foreach (var i in temp)
            //    {
            //        players.Add(PlayerHelper.PlayerModeltoPlayer(i));
            //    }
            //}

            //ScheduleHelper.shufflePlayers(ref players);
            TimeSlot ts = new TimeSlot();
            ScheduleHelper.makeTimeSlot(ts, players, db);
            //ScheduleHelper.makeTimeSlot(ts,ref players,ref new_players);
            ViewBag.playerList = players.OrderBy(x=>x.FirstName);
            //Session["PL"] = players;
            //new_players.Clear();

            return View(ts);
        }

	}
}