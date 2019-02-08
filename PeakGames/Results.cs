using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakGames
{
    class Results : IResults
    {
        //For easy deserialization, this class is used. This class will go through the changes if the desired output changes
        public int order { get; set; }
        public int player_id { get; set; }
        public int gained_experience { get; set; }
        public int total_experience { get; set; }

        public Results()
        {
            order = 0;
            player_id = 0;
            gained_experience = 0; 
            total_experience = 0;
        }
        public Results(int order, int id, int gained_experience, int total_experience)
        {
            this.order = order;
            this.player_id = id;
            this.gained_experience = gained_experience;
            this.total_experience = total_experience;
        }

        
    }
}
