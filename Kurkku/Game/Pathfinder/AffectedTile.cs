using System.Collections.Generic;

namespace Kurkku.Game
{
    class AffectedTile
    {
        public static List<Position> GetAffectedTiles(Item item)
        {
            return AffectedTile.GetAffectedTiles(
                    item.Definition.Data.Length,
                    item.Definition.Data.Width,
                    item.Position.X,
                    item.Position.Y,
                    item.Position.Rotation);
        }

        public static List<Position> GetAffectedTiles(Item item, int x, int y, int rotation)
        {
            return AffectedTile.GetAffectedTiles(
                    item.Definition.Data.Length,
                    item.Definition.Data.Width,
                    x,
                    y,
                    rotation);
        }

        public static List<Position> GetAffectedTiles(int length, int width, int x, int y, int rotation)
        {
            var points = new List<Position>();

            if (length != width)
            {
                if (rotation == 0 || rotation == 4)
                {
                    int l = length;
                    length = width;
                    width = l;
                }
            }

            for (int newX = x; newX < x + width; newX++)
            {
                for (int newY = y; newY < y + length; newY++)
                {
                    Position pos = new Position(newX, newY);
                    points.Add(pos);
                }
            }

            return points;
        }
    }
}
