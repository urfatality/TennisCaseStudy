using System.Collections.Generic;

namespace PeakGames
{
    interface IJsonUtilities
    {
        List<Player> PlayerFromJson(string path);
        List<Tournament> TournamentFromJson(string path);
        string WriteResults(List<Player> playerList, string path);
    }
}