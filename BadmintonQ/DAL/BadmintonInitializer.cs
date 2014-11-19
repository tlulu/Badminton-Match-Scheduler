using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BadmintonQ.Models;

namespace BadmintonQ.DAL
{
    public class BadmintonInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<BadmintonContext>
    {
        protected override void Seed(BadmintonContext context)
        {
            var players = new List<PlayerModels>
            {
            new PlayerModels{FirstName="Carson",LastName="Alexander",Level=3,Gender="M",Paid=true,Preference="D",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="Meredith",LastName="Alonso",Level=2,Gender="F",Paid=true,Preference="D",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="Arturo",LastName="Anand",Level=4,Gender="M",Paid=false,Preference="S",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="A",LastName="Alexander",Level=3,Gender="M",Paid=true,Preference="D",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="B",LastName="Alonso",Level=2,Gender="F",Paid=true,Preference="D",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="C",LastName="Anand",Level=4,Gender="M",Paid=false,Preference="S",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="D",LastName="Alexander",Level=1,Gender="M",Paid=true,Preference="D",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="E",LastName="Alonso",Level=2,Gender="F",Paid=true,Preference="D",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="F",LastName="Anand",Level=4,Gender="M",Paid=false,Preference="S",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="G",LastName="Alexander",Level=3,Gender="M",Paid=true,Preference="D",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="H",LastName="Alonso",Level=2,Gender="F",Paid=true,Preference="D",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="I",LastName="Anand",Level=4,Gender="M",Paid=false,Preference="S",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="J",LastName="Alexander",Level=3,Gender="M",Paid=true,Preference="D",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="K",LastName="Alonso",Level=5,Gender="F",Paid=true,Preference="D",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="L",LastName="Anand",Level=5,Gender="M",Paid=false,Preference="S",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="M",LastName="Alexander",Level=3,Gender="M",Paid=true,Preference="D",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="N",LastName="Alonso",Level=5,Gender="F",Paid=true,Preference="D",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="O",LastName="Anand",Level=4,Gender="M",Paid=false,Preference="S",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="P",LastName="Alexander",Level=3,Gender="M",Paid=true,Preference="D",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="Q",LastName="Alonso",Level=5,Gender="F",Paid=true,Preference="D",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="R",LastName="Anand",Level=4,Gender="M",Paid=false,Preference="S",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="S",LastName="Alexander",Level=3,Gender="M",Paid=true,Preference="D",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="T",LastName="Alonso",Level=5,Gender="F",Paid=true,Preference="D",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="U",LastName="Anand",Level=4,Gender="M",Paid=false,Preference="S",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="V",LastName="Alexander",Level=1,Gender="M",Paid=true,Preference="D",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="W",LastName="Alonso",Level=2,Gender="F",Paid=true,Preference="D",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="X",LastName="Anand",Level=4,Gender="M",Paid=false,Preference="S",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="Y",LastName="Alexander",Level=4,Gender="M",Paid=true,Preference="D",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="Z",LastName="Alonso",Level=5,Gender="F",Paid=true,Preference="D",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")},
            new PlayerModels{FirstName="Tony",LastName="Anand",Level=4,Gender="M",Paid=false,Preference="S",Active=true,EnrollmentDate=DateTime.Parse("2013-09-01")}
            
            };

            players.ForEach(s => context.Players.Add(s));

            var activePlayers = new List<ActivePlayer>
            {
            new ActivePlayer{PlayerID=1,GamesPlayed=2,Waits=1,OnCourt=false},
            new ActivePlayer{PlayerID=2,GamesPlayed=2,Waits=0,OnCourt=false},
            new ActivePlayer{PlayerID=3,GamesPlayed=2,Waits=0,OnCourt=false},
            new ActivePlayer{PlayerID=4,GamesPlayed=2,Waits=1,OnCourt=false},
            new ActivePlayer{PlayerID=5,GamesPlayed=2,Waits=2,OnCourt=false},
            new ActivePlayer{PlayerID=6,GamesPlayed=2,Waits=3,OnCourt=false},
            new ActivePlayer{PlayerID=7,GamesPlayed=2,Waits=3,OnCourt=false},
            new ActivePlayer{PlayerID=8,GamesPlayed=2,Waits=2,OnCourt=false},
            new ActivePlayer{PlayerID=9,GamesPlayed=2,Waits=2,OnCourt=false},
            new ActivePlayer{PlayerID=10,GamesPlayed=2,Waits=2,OnCourt=false},
            new ActivePlayer{PlayerID=11,GamesPlayed=2,Waits=1,OnCourt=false},
            new ActivePlayer{PlayerID=12,GamesPlayed=2,Waits=1,OnCourt=false},
            new ActivePlayer{PlayerID=13,GamesPlayed=2,Waits=2,OnCourt=false},
            new ActivePlayer{PlayerID=14,GamesPlayed=2,Waits=3,OnCourt=false},
            new ActivePlayer{PlayerID=15,GamesPlayed=2,Waits=1,OnCourt=false},
            new ActivePlayer{PlayerID=16,GamesPlayed=2,Waits=3,OnCourt=false},
            new ActivePlayer{PlayerID=17,GamesPlayed=2,Waits=3,OnCourt=false},
            new ActivePlayer{PlayerID=18,GamesPlayed=2,Waits=3,OnCourt=false},
            new ActivePlayer{PlayerID=19,GamesPlayed=2,Waits=3,OnCourt=false},
            new ActivePlayer{PlayerID=20,GamesPlayed=2,Waits=1,OnCourt=false}
            };

            activePlayers.ForEach(s => context.ActivePlayers.Add(s));

            context.SaveChanges();
            
        }
    }
}