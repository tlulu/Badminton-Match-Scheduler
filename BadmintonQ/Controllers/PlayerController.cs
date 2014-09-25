using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BadmintonQ.DAL;
using BadmintonQ.Models;
using BadmintonQ.Helper;
using System.Web.Script.Serialization;
using System.Diagnostics;

namespace BadmintonQ.Controllers
{
    public class PlayerController : Controller
    {
        private BadmintonContext db = new BadmintonContext();

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PlayerModels model)
        {
            if (ModelState.IsValid)
            {
                db.Players.Add(model);
                db.SaveChanges();
                return RedirectToAction("Details", model.ID);
            }
            return RedirectToAction("List");
        }


        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult List()
        {
            List<ActivePlayer> activePlayerList = db.ActivePlayers.ToList();
            List<Player> players = new List<Player>();
            foreach (var p in activePlayerList)
            {
                players.Add(PlayerHelper.ActivePlayertoPlayer(p,db));
            }
            return View(players);
        }

        public ActionResult Details(int id)
        {
            PlayerModels model = db.Players.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            Player player = PlayerHelper.PlayerModeltoPlayer(model);
            return View(player);
        }

        public ActionResult DropIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DropIn(Player model)
        {
            if (db.ActivePlayers.SingleOrDefault(x => x.PlayerID == model.ID) == null)
            {
                ActivePlayer newPlayer = new ActivePlayer();
                newPlayer.OnCourt = false;
                newPlayer.PlayerID = model.ID;
                newPlayer.Waits = 0;
                newPlayer.GamesPlayed = 0;
                db.ActivePlayers.Add(newPlayer);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult DropInList(string comment)
        {
            var json = new JavaScriptSerializer().Serialize(db.Players.Where(x => x.FirstName.Contains(comment) || x.LastName.Contains(comment)));
            return Json(json);
        }

    }
}