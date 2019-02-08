namespace PeakGames
{
    interface IResults
    {
        int gained_experience { get; set; }
        int order { get; set; }
        int player_id { get; set; }
        int total_experience { get; set; }
    }
}