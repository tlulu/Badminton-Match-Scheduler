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

        public static void shufflePlayers(ref List<Player> players)
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
            ScheduleHelper.makeTimeSlot(ts, players, db);
        }

        public static void makeTimeSlot(TimeSlot ts, List<Player> players, BadmintonContext context)
        {
            Random random = new Random();
            int numOfCourts = TimeSlot.getNumOfCourts();
            List<List<Player>> playergrid = new List<List<Player>>();
            List<int> onCourtIDs = new List<int>();
            int numOfLevels = 5;
            int numOfPlayersOnCourt = 4;
            int count;
            int lvl;

            //initialize
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
                    if (count >= numOfPlayersOnCourt)
                    {
                        break;
                    }
                    lvl = players.ElementAt(j).Level - 1;
                    playergrid[lvl] = playergrid[lvl].OrderByDescending(x => x.Waits).ToList();
                    for (int k = 0; k < playergrid[lvl].Count; k++)
                    {
                        if (count >= numOfPlayersOnCourt)
                        {
                            break;
                        }
                        if (playergrid[lvl][k].OnCourt == false)
                        {
                            PlayerHelper.updatePlayerOnCourt(playergrid[lvl][k], context);
                            court.addPlayer(playergrid[lvl][k]);
                            count++;
                        }
                    }

                    if (count < numOfPlayersOnCourt)
                    {
                        lvl++;
                        if (lvl < 5)
                        {
                            playergrid[lvl] = playergrid[lvl].OrderByDescending(x => x.Waits).ToList();
                            for (int k = 0; k < playergrid[lvl].Count; k++)
                            {
                                if (count >= numOfPlayersOnCourt)
                                {
                                    break;
                                }
                                if (playergrid[lvl][k].OnCourt == false)
                                {
                                    PlayerHelper.updatePlayerOnCourt(playergrid[lvl][k], context);
                                    court.addPlayer(playergrid[lvl][k]);
                                    count++;
                                }
                            }
                        }

                        if (count < numOfPlayersOnCourt)
                        {
                            lvl = lvl - 2;
                            if (lvl >= 0)
                            {
                                playergrid[lvl] = playergrid[lvl].OrderByDescending(x => x.Waits).ToList();
                                for (int k = 0; k < playergrid[lvl].Count; k++)
                                {
                                    if (count >= numOfPlayersOnCourt)
                                    {
                                        break;
                                    }
                                    if (playergrid[lvl][k].OnCourt == false)
                                    {
                                        PlayerHelper.updatePlayerOnCourt(playergrid[lvl][k], context);
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
                        playergrid[q][p].Waits++;
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

    }

}