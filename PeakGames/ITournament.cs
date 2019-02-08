using System.Collections.Generic;

namespace PeakGames
{
    interface ITournament
    {
        int Id { get; }
        Surface Surface { get; }
        TournamentType Type { get; }

        void EliminationTree(Player[] players);
        int FindArraySum(int[,] arr, int length);
        void LeagueMatch(List<Player> players);
        Player Match(Player player1, Player player2);
        Node NodeMatch(Node node1, Node node2);
        void SetMatches(List<Player> players);
    }
}