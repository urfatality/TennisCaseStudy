using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PeakGames
{
    class JsonUtilities : IJsonUtilities
    {

        //PlayerFromJson method serializes the JSON file and maps the given attributes to players and then a list of players is returned
        public List<Player> PlayerFromJson(String path)
        {

            string json;
            List<Player> playerList = new List<Player>();
            //Try catch blocks check if the file is in the right format and stop the process if there are any problems.
            try
            {
                using (StreamReader r = new StreamReader(path))
                {
                    json = r.ReadToEnd();
                }
            }
            catch
            {
                return null;
            }

            if (json == "")
            {
                return null;
            }
            

            JObject rss = JObject.Parse(json);
            try
            {
                var Id =
                from p in rss["players"]
                select (int)p["id"];
                var hand =
                    from p in rss["players"]
                    select (string)p["hand"];
                var Experience =
                    from p in rss["players"]
                    select (int)p["experience"];
                var skill =
                    from p in rss["players"]
                    select (object)p["skills"];
            }
            catch
            {
                return null;
            }
            //Each attribute is read from JSON file.
            var postId =
                from p in rss["players"]
                select (int)p["id"];
            var posthand =
                from p in rss["players"]
                select (string)p["hand"];
            var postExperience =
                from p in rss["players"]
                select (int)p["experience"];
            var postSkills =
                from p in rss["players"]
                select (object)p["skills"];
            int[] ids = postId.ToArray();
            string[] hands = posthand.ToArray();
            int[] exps = postExperience.ToArray();
            int[,] skills = new int[ids.Length, Enum.GetNames(typeof(Surface)).Length];

            for (int i = 0; i < ids.Length; i++)
            {
                int j = 0;
                foreach (String foo in Enum.GetNames(typeof(Surface)))
                {
                    var jsonSkills =
                        from p in rss["players"]
                        select (int)p["skills"][foo];
                    skills[i, j] = jsonSkills.ElementAt(i);
                    j++;
                }
            }

            for (int i = 0; i < ids.Length; i++)
            {
                int[] playerSkill = new int[Enum.GetNames(typeof(Surface)).Length];
                for (int j = 0; j < playerSkill.Length; j++)
                {
                    playerSkill[j] = skills[i, j];
                }
                Player player = new Player(ids[i], hands[i], exps[i], playerSkill);
                playerList.Add(player);
            }
            return playerList;
        }

        public List<Tournament> TournamentFromJson(String path)
        {
            //Similar to the PlayerFromJson method, TournamentFromJson serializes the JSON file and maps attributes parsed from the JSON file to Tournamnent objects
            string json;
            JObject rss;
            List<Tournament> tournamentList = new List<Tournament>();
            try
            {
                using (StreamReader r = new StreamReader(path))
                {
                    json = r.ReadToEnd();
                }
              rss = JObject.Parse(json);
            }
            catch
            {
                return null;
            }
            try
            {
                var jsonId =
                from p in rss["tournaments"]
                select (int)p["id"];
                var jsonIdSurfate =
                    from p in rss["tournaments"]
                    select (string)p["surface"];
                var jsonIdType =
                    from p in rss["tournaments"]
                    select (string)p["type"];
            }
            catch
            {
                return null;
            }
            var postId =
                from p in rss["tournaments"]
                select (int)p["id"];
            var postSurfate =
                from p in rss["tournaments"]
                select (string)p["surface"];
            var postType =
                from p in rss["tournaments"]
                select (string)p["type"];
            int[] ids = postId.ToArray();
            string[] surfaces = postSurfate.ToArray();
            string[] types = postType.ToArray();
            for(int i= 0; i < ids.Length; i++)
            {
                Tournament tournament = new Tournament(ids[i], surfaces[i], types[i]);
                tournamentList.Add(tournament);
            }
            return tournamentList;
        }


        public String WriteResults(List<Player> playerList, string path)
        {
            //WriteResults method writes the resulting rankings and respective experiences of players. A Results class is used for easy deserialization
            var resultList = new List<Results>();
            for (int i = 0; i < playerList.Count; i++)
            {
                int gained = playerList[i].FinalExperience - playerList[i].FirstExperience;
                Results result = new Results(i+ 1, playerList[i].Id, gained, playerList[i].FinalExperience);
                resultList.Add(result);
            }

            resultList = SortList(resultList);
            
            for (int i = 0; i < resultList.Count; i++)
            {
                resultList[i].order = i + 1; 
            }
            var collectionWrapper = new
            {

                results = resultList

            };
            string json = JsonConvert.SerializeObject(resultList, Formatting.Indented);
            File.WriteAllText(path, JsonConvert.SerializeObject(collectionWrapper, Formatting.Indented));
            return json;
        }

        private List<Results> SortList(List<Results> resultList)
        {
            int max;
            for(int i = 0; i < resultList.Count; i++)
            {
                max = i;
                for(int j = i; j < resultList.Count - 1; j++)
                {
                    if (resultList.ElementAt(max).gained_experience < resultList.ElementAt(j + 1).gained_experience)
                    {
                        max = j + 1;
                    }
                    else if(resultList.ElementAt(max).gained_experience == resultList.ElementAt(j + 1).gained_experience)
                    {
                        int beginningExperiencei = resultList.ElementAt(max).total_experience - resultList.ElementAt(max).gained_experience;
                        int beginningExperience2 = resultList.ElementAt(j + 1).total_experience - resultList.ElementAt(j + 1).gained_experience;
                        if (beginningExperiencei < beginningExperience2)
                        {
                            max = j + 1;
                        }
                    }
                }
                Results temp = resultList[i];
                resultList[i] = resultList[max];
                resultList[max]= temp;
            }
            return resultList;
        }
    }
}
