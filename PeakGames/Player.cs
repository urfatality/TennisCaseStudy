using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace PeakGames
{   
    //A player class that includes the fields from JSON format: Id, Hand, Experience and Skills
    public enum Hand
    {
        right = 0,
        left = 1
    }
    [JsonObject("players")]
    class Player : IPlayer
    {
        
        [JsonProperty("id")]
        public int Id { get; private set; }
        [JsonProperty("hand")]
        public readonly Hand Hand;

        [JsonProperty("experience")]
        public int FirstExperience { get; private set; }
        public int FinalExperience { get; set; }

        [JsonProperty("skills")]
        public int[] Skills { get; private set; }

        public Player()
        {
            Id = 0;
            Hand = Hand.right;
            FirstExperience = 0;
            FinalExperience = FirstExperience;
            Skills =new int[] { 0, 0, 0};
        }

        public Player(int id,object hand, int experience, int[] skills)
        {
            this.Id = id;
            
            this.Hand = (Hand)Enum.Parse(typeof(Hand), hand.ToString());
            this.FirstExperience = experience;
            this.FinalExperience = experience;
            this.Skills = skills;
        }
        
        
    }
}
