using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakGames
{

    class Node : INode
    {
        //Node class is used to create elimination trees. 
        public Player Player { get; set; }
        public Node Parent { get; set; }
        public Node rightLeaf;
        public Node leftLeaf;
        public Node()
        {
            Parent = null;
            Player = null;
            rightLeaf = null;
            leftLeaf = null;

        }
        public Node(Player player)
        {

            this.Player = player;
            this.Parent = null;
            this.rightLeaf = null;
            this.leftLeaf = null;

        }

        
        public bool IsLeaf(Node node)
        {
            if (node != null && node.rightLeaf == null && node.leftLeaf == null)
                return true;
            else
                return false;
        }

        public void InsertPlayer(Node node, Player player)
        {
            if (node == null)
                node = new Node(player);
            else
                node.Player = player;

        }

        public Node Search(Node node, Player player)
        {

            if (IsLeaf(node))
            {
                if (node.Player == player)
                    return node;
                else
                {
                    Search(node.leftLeaf, player);
                    Search(node.rightLeaf, player);
                }
            }
            return null;

        }

        public void Display(Node node)
        {
            if (node == null)
                return;

            Display(node.leftLeaf);
            Console.Write(" " + node.Player.Id);
            Display(node.rightLeaf);
        }

    }
}
