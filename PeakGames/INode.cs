namespace PeakGames
{
    interface INode
    {
        Node Parent { get; set; }
        Player Player { get; set; }

        void Display(Node node);
        void InsertPlayer(Node node, Player player);
        bool IsLeaf(Node node);
        Node Search(Node node, Player player);
    }
}