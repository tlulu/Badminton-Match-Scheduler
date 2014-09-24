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
    public class PlayerController: Controller
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
            List<PlayerModels> playerList = db.Players.ToList();
            List<Player> players=new List<Player>();
            foreach (var p in playerList){
                players.Add(PlayerHelper.PlayerModeltoPlayer(p));
            }
            return View(players);
            //var json = new JavaScriptSerializer().Serialize(db.Players);
            //return Json(json);
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

            return View(db.ActivePlayers.ToList());
        }

        [HttpPost]
        public ActionResult DropIn(int id)
        {
            db.Players.FirstOrDefault(x => x.ID == id).Active = true;
            db.SaveChanges();
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