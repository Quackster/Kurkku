namespace Kurkku.Game
{
    public class Rotation
    {
        public static int CalculateDirection(Position from, Position to)
        {
            return CalculateDirection(from.X, from.Y, to.X, to.Y);
        }

        public static int CalculateDirection(int X, int Y, int ToX, int ToY)
        {
            if (X == ToX)
            {
                if (Y < ToY)
                    return 4;
                else
                    return 0;
            }
            else if (X > ToX)
            {
                if (Y == ToY)
                    return 6;
                else if (Y < ToY)
                    return 5;
                else
                    return 7;
            }
            else
            {
                if (Y == ToY)
                    return 2;
                else if (Y < ToY)
                    return 3;
                else
                    return 1;
            }
        }
    }
}
