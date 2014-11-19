using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BadmintonQ.Models;
using BadmintonQ.Helper;
using BadmintonQ.DAL;
using System.Diagnostics;


namespace BadmintonQ.Helper
{
    public class ScheduleHelper
    {
        /* Creates a TimeSlot using a list of activeplayers
         * Assigns players on Courts based on wait-time(primary factor) and level of player
         * Time of arrival is automatically taken care of
         * Algorithm:
         * 1.Get a players with the highest wait-time
         * 2.Get players with the same level as the player in 1.
         * 3.Assign the players to a court
         * 4.If not enough players in that player's level or the wait-time difference is greater than 2, 
         *   find players with the highest wait-time in other levels
         * 5.Repeat 1-4 until all courts are filled
         * 6.Update the players on the court to "on court" and reset their wait-time to zero
         * 7.Increment the wait-time of the off court players by 1
         * The ActivePlayersTable is updated afterwards
         */
        public static void MakeTimeSlot(TimeSlot ts, List<Player> players, BadmintonContext context)
        {
            Random random = new Random();
            int numOfCourts = TimeSlot.getNumOfCourts();
            List<List<Player>> playergrid = new List<List<Player>>();
            List<int> onCourtIDs = new List<int>();
            int numOfLevels = 5;
            int count;
            int lvl;

            for (int i = 0; i < numOfLevels; i++)
            {
                playergrid.Add(new List<Player>());
            }

            for (int j = 0; j < players.Count; j++)
            {
                playergrid[players.ElementAt(j).Level - 1].Add(players.ElementAt(j));
            }

            for (int i = 0; i < numOfCourts; i++)
            {
                Court court = new Court();
                count = 0;

                players = players.OrderByDescending(x => x.Waits).ToList();

                for (int j = 0; j < players.Count; j++)
                {
                    if (count >= Court.intGetNumOfPlayersOnCourt())
                    {
                        break;
                    }
                    lvl = players.ElementAt(j).Level - 1;
                    playergrid[lvl] = playergrid[lvl].OrderByDescending(x => x.Waits).ToList();
                    for (int k = 0; k < playergrid[lvl].Count; k++)
                    {
                        if (count >= Court.intGetNumOfPlayersOnCourt())
                        {
                            break;
                        }
                        if (playergrid[lvl][k].OnCourt == false)
                        {
                            PlayerHelper.UpdatePlayerOnCourt(playergrid[lvl][k], context);
                            court.addPlayer(playergrid[lvl][k]);
                            count++;
                        }
                    }

                    if (count < Court.intGetNumOfPlayersOnCourt())
                    {
                        lvl++;
                        if (lvl < 5)
                        {
                            playergrid[lvl] = playergrid[lvl].OrderByDescending(x => x.Waits).ToList();
                            for (int k = 0; k < playergrid[lvl].Count; k++)
                            {
                                if (count >= Court.intGetNumOfPlayersOnCourt())
                                {
                                    break;
                                }
                                if (playergrid[lvl][k].OnCourt == false)
                                {
                                    PlayerHelper.UpdatePlayerOnCourt(playergrid[lvl][k], context);
                                    court.addPlayer(playergrid[lvl][k]);
                                    count++;
                                }
                            }
                        }

                        if (count < Court.intGetNumOfPlayersOnCourt())
                        {
                            lvl = lvl - 2;
                            if (lvl >= 0)
                            {
                                playergrid[lvl] = playergrid[lvl].OrderByDescending(x => x.Waits).ToList();
                                for (int k = 0; k < playergrid[lvl].Count; k++)
                                {
                                    if (count >= Court.intGetNumOfPlayersOnCourt())
                                    {
                                        break;
                                    }
                                    if (playergrid[lvl][k].OnCourt == false)
                                    {
                                        PlayerHelper.UpdatePlayerOnCourt(playergrid[lvl][k], context);
                                        court.addPlayer(playergrid[lvl][k]);
                                        count++;
                                    }
                                }
                            }
                        }
                    }
                }
                ts.addCourt(court);
            }
            for (int q = 0; q < playergrid.Count; q++)
            {
                for (int p = 0; p < playergrid[q].Count; p++)
                {
                    if (playergrid[q][p].OnCourt == false)
                    {
                        playergrid[q][p].Waits++;           //update every player's wait times
                    }
                }
            }

            foreach (var p in players)
            {
                if (p.OnCourt == false)
                {
                    ActivePlayer updated = context.ActivePlayers.Single(x => x.PlayerID == p.ID);
                    updated.Waits++;
                    context.SaveChanges();
                }
            }

        }

        /* Assigns every player to be off court
         * Assigns new players to courts 
         */
        public static void Rotate(BadmintonContext db, List<Player> players, TimeSlot ts)
        {
            foreach (var p in db.ActivePlayers)
            {
                p.OnCourt = false;
                db.SaveChanges();
            }
            List<ActivePlayer> temp = db.ActivePlayers.ToList();
            foreach (var i in temp)
            {
                players.Add(PlayerHelper.ActivePlayertoPlayer(i, db));
            }
            MakeTimeSlot(ts, players, db);
        }

        /*Shuffles players in a list of players */
        public static void ShufflePlayers(ref List<Player> players)
        {
            int numOfShuffles = 50;
            Player temp;
            Random random = new Random();
            int ran;
            int ran2;

            for (int i = 0; i < numOfShuffles; i++)
            {
                ran = random.Next(0, players.Count);
                ran2 = random.Next(0, players.Count);
                temp = players.ElementAt(ran);
                players[ran] = players.ElementAt(ran2);
                players[ran2] = temp;
            }
        }
    }
}