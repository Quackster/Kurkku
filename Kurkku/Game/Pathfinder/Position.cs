using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public class Position : ICloneable
    {
        public int X;
        public int Y;
        public double Z;
        public int BodyRotation;
        public int HeadRotation;

        public Position() : this(0, 0, 0)
        {
        }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
            Z = 0;
        }

        public Position(int x, int y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Position(int x, int y, double z, int headRotation, int bodyRotation)
        {
            X = x;
            Y = y;
            Z = z;
            HeadRotation = headRotation;
            BodyRotation = bodyRotation;
        }

        /**
         * Checks if current tile touches target tile
         */
        public bool Touches(Position position)
        {
            return GetDistance(position) <= 2;
        }

        public Position Add(Position other)
        {
            return new Position(other.X + X, other.Y + Y, other.Z + Z);
        }

        public Position Subtract(Position other)
        {
            return new Position(other.X - X, other.Y - Y, other.Z - Z);
        }

        public int GetDistance(Position point)
        {
            int dx = X - point.X;
            int dy = Y - point.Y;

            return (dx * dx) + (dy * dy);
        }

        public double GetDistanceSquared(Position point)
        {
            int dx = X - point.X;
            int dy = Y - point.Y;

            return Math.Sqrt((dx * dx) + (dy * dy));
        }

        public Position GetSquareInFront()
        {
            Position square = (Position)Clone();

            if (BodyRotation == 0)
            {
                square.Y--;
            }
            else if (BodyRotation == 1)
            {
                square.X++;
                square.Y--;
            }
            else if (BodyRotation == 2)
            {
                square.X++;
            }
            else if (BodyRotation == 3)
            {
                square.X++;
                square.Y++;
            }
            else if (BodyRotation == 4)
            {
                square.Y++;
            }
            else if (BodyRotation == 5)
            {
                square.X--;
                square.Y++;
            }
            else if (BodyRotation == 6)
            {
                square.X--;
            }
            else if (BodyRotation == 7)
            {
                square.X--;
                square.Y--;
            }

            return square;
        }


        public Position GetSquareBehind()
        {
            Position square = (Position)Clone();

            if (BodyRotation == 0)
            {
                square.Y++;
            }
            else if (BodyRotation == 1)
            {
                square.X--;
                square.Y++;
            }
            else if (BodyRotation == 2)
            {
                square.X--;
            }
            else if (BodyRotation == 3)
            {
                square.X--;
                square.Y--;
            }
            else if (BodyRotation == 4)
            {
                square.Y--;
            }
            else if (BodyRotation == 5)
            {
                square.X++;
                square.Y--;
            }
            else if (BodyRotation == 6)
            {
                square.X++;
            }
            else if (BodyRotation == 7)
            {
                square.X++;
                square.Y++;
            }

            return square;
        }

        public Position GetSquareRight()
        {
            Position square = (Position)Clone();

            if (BodyRotation == 0)
            {
                square.X++;
            }
            else if (BodyRotation == 1)
            {
                square.X++;
                square.Y++;
            }
            else if (BodyRotation == 2)
            {
                square.Y++;
            }
            else if (BodyRotation == 3)
            {
                square.X--;
                square.Y++;
            }
            else if (BodyRotation == 4)
            {
                square.X--;
            }
            else if (BodyRotation == 5)
            {
                square.X--;
                square.Y--;
            }
            else if (BodyRotation == 6)
            {
                square.Y--;
            }
            else if (BodyRotation == 7)
            {
                square.X++;
                square.Y--;
            }

            return square;
        }

        public Position GetSquareLeft()
        {
            Position square = (Position)Clone();

            if (BodyRotation == 0)
            {
                square.X--;
            }
            else if (BodyRotation == 1)
            {
                square.X--;
                square.Y--;
            }
            else if (BodyRotation == 2)
            {
                square.Y--;
            }
            else if (BodyRotation == 3)
            {
                square.X++;
                square.Y--;
            }
            else if (BodyRotation == 4)
            {
                square.X++;
            }
            else if (BodyRotation == 5)
            {
                square.X++;
                square.Y++;
            }
            else if (BodyRotation == 6)
            {
                square.Y++;
            }
            else if (BodyRotation == 7)
            {
                square.X--;
                square.Y++;
            }

            return square;
        }

        public object Clone()
        {
            return new Position(X, Y, Z, HeadRotation, BodyRotation);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var t = obj as Position;

            if (t == null)
                return false;

            if (X == t.X && Y == t.Y)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
