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



        internal static void MergeUsers(IEnumerable<int> ids)
        {
            db.Cypher
            .Unwind(ids, "id")
            .Create("(u:User{user_id:id})")
            //.Where("u.id = id")            
            .ExecuteWithoutResults();
        }

        internal static void MergeThuuzMatch(ThuuzMatch ev)
        {
            db.Cypher
             .Create("(e:Game{newUser})")
             .WithParam("newUser", ev)             
             .ExecuteWithoutResults();

        }




        internal static void MergeUser(int _id)
        {
            db.Cypher
            .Merge($"(u:User{{Id:{_id}}})")
            .ExecuteWithoutResults();
        }



        internal static List<int> GetExist()
        {
            return db.Cypher
            .Match($"(e:Event)")
            .Where("not EXISTS( (:Event)-[]->(e) )")
            .With("e.Id AS iid")
            .Return(iid => iid.CollectAs<int>())
            .Results.First().Distinct().ToList();
        }




        internal static void CreateQuery(string query)
        {

            try
            {
                db.Cypher
                .Create(query)
                .ExecuteWithoutResults();
            }
            catch
            {
                db.Cypher
               .Merge("(:Query{Value:{v}})")
               .WithParam("v", query)
               .ExecuteWithoutResults();
            }

        }
        internal static void MergePages()
        {
            var names = new[] {
            "workbook",
            "rooms.lesson.no-rev",
            "showcase",
            "rooms.view.step.grammar",
            "rooms.lesson.rev.step.content",//-
            "download-apps",
            "rooms.lesson.rev.step.grammar",
            "rooms.homework-showcase",
            "rooms.view.step.attachments",
            "notes",
            "rooms.lesson.rev.step.attachments",
            "words.vocabulary",
            "rooms.student-showcase",
            "rooms.view.step.content",//-
            "rooms.test.step",
            "video.test",
            "rooms.test-showcase",
            };

            db.Cypher
            .Unwind(names, "name")
              .Merge("(p:Page{Name:name})")
              .ExecuteWithoutResults();
        }
    }
}
