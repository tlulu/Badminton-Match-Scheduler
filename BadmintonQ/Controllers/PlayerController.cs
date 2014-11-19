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

        #region CRUD

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

        #endregion

        public ActionResult DropIn()
        {
            return View();
        }

        /* Adds a new ActivePlayer entity
         * into the ActivePlayers table 
         */
        [HttpPost]
        public ActionResult DropIn(Player model)
        {
            PlayerHelper.AddActivePlayer(model, db);
            return RedirectToAction("List");
        }

        /* Returns a list of players in the Players table
         * Returns list in json format
         */
        public ActionResult DropInSearch(string input)
        {
            var json = PlayerHelper.GetSearchedPlayersJson(input, db);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

    }
}