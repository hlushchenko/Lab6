using System.Collections.Generic;

namespace Lab6.BasicConstructions.RTree
{
    public abstract class Node
    {
        public string Type = "Unknown type";
        public const int maxChildren = 10;
        public const int minChildren = 4;
    }
}