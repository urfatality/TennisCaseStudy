using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakGames
{
    
    public enum Surface
    {
        clay = 0,
        grass = 1,
        hard = 2
    }
    public enum TournamentType
    {
        elimination = 0,
        league = 1
    }
    
    class Tournament : ITournament
    {
        public int Id { get; private set; }
        public Surface Surface { get; private set; }
        public TournamentType Type { get; private set; }

        public Tournament()
        {
            Id = 0;
            Surface = Surface.clay;
            Type = TournamentType.elimination;
        }
        public Tournament(int id, object surface, object type)
        {
            this.Id = id;
            this.Surface = (Surface)Enum.Parse(typeof(Surface), surface.ToString());
            this.Type = (TournamentType)Enum.Parse(typeof(TournamentType), type.ToString());
        }

        public void SetMatches(List<Player> players)
        {
            Console.WriteLine($"Match Type: {this.Type}");
            if (this.Type == TournamentType.elimination)             
                EliminationTree(players.ToArray());
            
                
            else if (this.Type == TournamentType.league)
                LeagueMatch(players);
            
                
        }


        //The main match class. The match is played between two input players.The rules of matches are implemented here.
        public Player Match(Player player1, Player player2)
        {
            Console.WriteLine($"The match is between: {player1.Id} vs {player2.Id}");
            decimal chance;
            int ground = (int)this.Surface;
            int player1Point = 1, player2Point = 1 ;
            if (player1.Hand == Hand.left)
                player1Point = player1Point + 2;
            if (player2.Hand == Hand.left)
                player2Point = player2Point + 2;
            if (player1.FinalExperience > player2.FinalExperience)
                player1Point = player1Point + 3;
            if (player2.FinalExperience > player1.FinalExperience)
                player2Point = player2Point + 3;
            if (player1.Skills[ground] > player2.Skills[ground])
                player1Point = player1Point + 4;
            if (player2.Skills[ground] > player1.Skills[ground])
                player2Point = player2Point + 4;
            //int chance is a variable that corresponds to the chance of player 1 winning the match
            chance = Decimal.Divide(player1Point, player1Point + player2Point);
            
            Random rand = new Random();
            decimal prob = (decimal)rand.NextDouble();
            //A random decimal is generated and if the generated decimal is smaller than the chance, player 1 is the winner. Otherwise player 2 is declared winner
            if(prob <= chance)
            {
                Console.WriteLine($"Winner is {player1.Id}");
                return player1;
            }
            else
            {
                Console.WriteLine($"Winner is {player2.Id}");
                return player2;
            }
            

        }

        public void EliminationTree(Player[] players)
        {
            //EliminationTree class builds a tree similar to a tournament bracket from bottom up. The root is the winner. This would make traversing, searching or printing the bracket much easier.
            Node[] winners = new Node[players.Length / 2];
            Player[] winningPlayers = new Player[winners.Length];
            Random rand = new Random();
            int selection;
            for (int i = 0; i < winners.Length; i++)
            {
                selection = rand.Next(players.Length);
                Node player1 = new Node(players[selection]);
                players = players.Where((val, idx) => idx != selection).ToArray();
                selection = rand.Next(players.Length);
                Node player2 = new Node(players[selection]);
                players = players.Where((val, idx) => idx != selection).ToArray();
                winners[i] = NodeMatch(player1, player2);
                winningPlayers[i] = winners[i].Player;
                
            }
            //The escape condition of the recursion is the number of players after the last match.
            if (winningPlayers.Length == 1)
            {
                winners[0].Parent = null;
                Console.WriteLine($"Tournament winner is: {winners[0].Player.Id}");
                return;
            }
            //Two random players are selected and matched. Winnners are recursively matched again until there is only one player, the winner.
            EliminationTree(winningPlayers);
            
        }

        public Node NodeMatch(Node node1, Node node2)
        {
            //This method prepares the nodes inserting players inside and distributing the experience points.
            Node winner = new Node(Match(node1.Player, node2.Player));
            if(winner.Player == node1.Player)
            {
                node1.Player.FinalExperience = node1.Player.FinalExperience + 20;
                node2.Player.FinalExperience = node2.Player.FinalExperience + 10;
            }
            else if (winner.Player == node2.Player)
            {
                node2.Player.FinalExperience = node2.Player.FinalExperience + 20;
                node1.Player.FinalExperience = node1.Player.FinalExperience + 10;
            }
            node1.Parent = winner;
            node2.Parent = winner;
            winner.leftLeaf = node1;
            winner.rightLeaf = node2;
            return winner;
        }

        public void LeagueMatch(List<Player> players)
        {
            //This is the method for league format matches. Every player is matched with each other once. The order is random but once a player is selected she/he plays with latter player.
            List<int> numberList = new List<int>();
            int[,] playerMatchings = new int[players.Count, players.Count];
            for(int i = 0; i < players.Count; i++)
            {
                playerMatchings[i, i] = 1;
            }

            Random rand = new Random();
            int randomNumber, randomNumber2; 
            while ( FindArraySum(playerMatchings, players.Count) < Math.Pow(players.Count, 2))
            {
               
                randomNumber = rand.Next(players.Count );
                randomNumber2 = rand.Next(players.Count );
                if(playerMatchings[randomNumber, randomNumber2] != 1 && playerMatchings[randomNumber2, randomNumber] != 1 && randomNumber != randomNumber2)
                {
                    
                    if (Match(players[randomNumber], players[randomNumber2]) == players[randomNumber])
                    {
                        players[randomNumber].FinalExperience = players[randomNumber].FinalExperience + 10;
                        players[randomNumber2].FinalExperience = players[randomNumber2].FinalExperience + 1;
                    }
                    else
                    {
                        players[randomNumber2].FinalExperience = players[randomNumber2].FinalExperience + 10;
                        players[randomNumber].FinalExperience = players[randomNumber].FinalExperience + 1;
                    }  
                    playerMatchings[randomNumber, randomNumber2] = 1;
                    playerMatchings[randomNumber2, randomNumber] = 1;
                }
            }
            

        }


        public int FindArraySum(int[,] arr, int length)
        {
            int sum = 0;
            for(int i = 0; i < length; i++)
            {
                for(int j = 0; j < length; j++)
                {         
                    sum += arr[i, j];
                }
            }
            return sum;
        }

      
    }   
}
