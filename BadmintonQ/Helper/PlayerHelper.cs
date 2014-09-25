using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BadmintonQ.Models;
using BadmintonQ.DAL;


namespace BadmintonQ.Helper
{
    public class PlayerHelper
    {
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

        

        public static void freePlayers(ref List<Player> players)
        {
            for (int i = 0; i < players.Count; i++)
            {
                players.ElementAt(i).OnCourt = false;
            }
        }

        public static void fillPlayerInfo(int index, ref Court court,ref List<Player> players)
        {
            players.ElementAt(index).GamesPlayed++;
            players.ElementAt(index).OnCourt = true;
            court.addPlayer(players.ElementAt(index));
        }

        public static void updatePlayerOnCourt(Player player, BadmintonContext context)
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