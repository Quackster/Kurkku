using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public class PathfinderNode : IComparable<PathfinderNode>
    {
        public Position Position;
        public PathfinderNode Next;
        public int Cost = Int32.MaxValue;
        public bool InOpen = false;
        public bool InClosed = false;

        public PathfinderNode(Position position)
        {
            this.Position = position;
        }

        public override bool Equals(object obj)
        {
            return (obj is PathfinderNode) && ((PathfinderNode)obj).Position.Equals(this.Position);
        }

        public bool Equals(PathfinderNode Breadcrumb)
        {
            return Breadcrumb.Position.Equals(this.Position);
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }

        public int CompareTo(PathfinderNode Other)
        {
            return Cost.CompareTo(Other.Cost);
        }
    }
}
