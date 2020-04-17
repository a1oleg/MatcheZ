using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Matchez
{
    public class Repo
    {
        public static GraphClient db;

        public static void StartLocale()
        {
            if (db == null)
            {
                db = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "letmein"); // 11007 11009  7474 11002 11005
                db.Connect();
            }
        }

        public static void StartRemote()
        {
            if (db == null)
            {
                db = new GraphClient(new Uri("https://hobby-egbhojjgbjfigbkehbaahhdl.dbs.graphenedb.com:24780/db/data/"), "a1oleg", "b.gebcaf8x5p56.ocMIAYbNnVtEyWqd");
                db.Connect();
            }
        }
        
        internal static void MergeThuuzMatch(ThuuzMatch ev)
        {
            db.Cypher
             .Create("(e:Game{newUser})")
             .WithParam("newUser", ev)             
             .ExecuteWithoutResults();
        }       

        internal static List<ThuuzMatch> GetExist()
        {
            return db.Cypher
            .Match("(e:Game)")            
            .Return(e => e.CollectAs<ThuuzMatch>())
            .Results.First().ToList();
        }

        internal static void MergeBasRefMatch(BasRefMatch brm, int id)
        {
            db.Cypher
             .Create("(nm:BRGame{newUser})")
             .WithParam("newUser", brm)
             .With("nm")
             .Match("(om:ThuuzMatch{Id:$_id})")
             .WithParam("_id", id)
             .Merge("(om)-[:mm]->[nm]")
             .ExecuteWithoutResults();
        }
    }
}
