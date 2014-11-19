using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BadmintonQ.Models;
using BadmintonQ.DAL;
using System.Web.Script.Serialization;


namespace BadmintonQ.Helper
{
    public class PlayerHelper
    {
        public static Player ActivePlayertoPlayer(ActivePlayer activePlayer,BadmintonContext db)
        {
            Player player = new Player();
            PlayerModels model = db.Players.SingleOrDefault(x => x.ID == activePlayer.PlayerID);
            player.ID = model.ID;
            player.FirstName = model.FirstName;
            player.LastName = model.LastName;
            player.Paid = model.Paid;
            player.Level = model.Level;
            player.Preference = model.Preference;
            player.EnrollmentDate = model.EnrollmentDate;
            player.OnCourt = activePlayer.OnCourt;
            player.Active = model.Active;
            player.Waits = activePlayer.Waits;
            player.GamesPlayed = activePlayer.GamesPlayed;
            return player;
        }

        /* Adds player to ActivePlayers table */
        public static void AddActivePlayer(Player model,BadmintonContext db){
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
        }

        /* Sets all players as "off court"
         */
        public static void FreePlayers(ref List<Player> players)
        {
            for (int i = 0; i < players.Count; i++)
            {
                players.ElementAt(i).OnCourt = false;
            }
        }

        /* Returns a json of players that have 
         * first name or last name that contains input
         */
        public static string GetSearchedPlayersJson(string input, BadmintonContext db)
        {
            var json = new JavaScriptSerializer().Serialize(db.Players.Where(x => x.FirstName.Contains(input) || x.LastName.Contains(input)));
            return json;
        }

        public static Player PlayerModeltoPlayer(PlayerModels model)
        {
            Player player = new Player();
            player.ID = model.ID;
            player.FirstName = model.FirstName;
            player.LastName = model.LastName;
            player.Paid = model.Paid;
            player.Level = model.Level;
            player.Preference = model.Preference;
            player.EnrollmentDate = model.EnrollmentDate;
            player.Active = model.Active;
            return player;
        }

        /* Updates player view model
         * when a player is assigned to a court 
         */
        public static void UpdatePlayerOnCourt(Player player, BadmintonContext context)
        {
            var id = player.ID;
            player.Waits = 0;
            player.OnCourt = true;
            player.GamesPlayed++;
            ActivePlayer updated = context.ActivePlayers.Single(x => x.PlayerID == id);
            updated.Waits = 0;
            updated.OnCourt = true;
            updated.GamesPlayed++;
            context.SaveChanges();
        }
    }
}