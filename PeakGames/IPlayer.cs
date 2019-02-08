namespace PeakGames
{
    interface IPlayer
    {
        int FinalExperience { get; set; }
        int FirstExperience { get; }
        int Id { get; }
        int[] Skills { get; }
    }
}