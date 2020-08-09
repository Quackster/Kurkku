using Kurkku.Storage.Database.Data;
using System;
using Kurkku.Util.Extensions;

namespace Kurkku.Game
{
    public class RoomModel
    {
        #region Properties

        public int MapSizeX { get; private set; }
        public int MapSizeY { get; private set; }
        public TileState[,] TileStates { get; private set; }
        public double[,] TileHeights { get; private set; }
        public string Heightmap { get; private set; }

        public RoomModelData Data { get; set; }
        public Position Door => new Position(Data.DoorX, Data.DoorY, Data.DoorZ, Data.DoorDirection, Data.DoorDirection);

        #endregion

        #region Constructors

        public RoomModel(RoomModelData data)
        {
            Data = data;
            Parse();
        }

        #endregion

        #region Public methods

        private void Parse()
        {
            string[] lines = Data.Heightmap.Split('|');

            MapSizeY = lines.Length;
            MapSizeX = lines[0].Length;

            TileStates = new TileState[MapSizeX, MapSizeY];
            TileHeights = new double[MapSizeX, MapSizeY];

            Heightmap = string.Empty;

            for (int y = 0; y < MapSizeY; y++)
            {
                string line = lines[y];

                for (int x = 0; x < MapSizeX; x++)
                {
                    try
                    {
                        string tile = Convert.ToString(line[x]);

                        if (tile.IsNumeric())
                        {
                            TileStates[x, y] = TileState.OPEN;
                            TileHeights[x, y] = double.Parse(tile);
                        }
                        else
                        {
                            TileStates[x, y] = TileState.CLOSED;
                            TileHeights[x, y] = 0;
                        }

                        if (Data.DoorX == x &&
                            Data.DoorY == y)
                        {
                            TileStates[x, y] = TileState.OPEN;
                            TileHeights[x, y] = Data.DoorZ;

                            Heightmap += Convert.ToString(Data.DoorZ);
                        }
                        else
                        {
                            Heightmap += tile;
                        }
                    }
                    catch { }
                }

                Heightmap += "\r";
            }
        }

        /// <summary>
        /// Get if the position is an actual tile
        /// </summary>
        /// <param name="Position">the position</param>
        /// <returns>true, if successful</returns>
        public bool IsTile(Position Position)
        {
            if (Position.X >= 0 && Position.Y >= 0 && Position.X < this.MapSizeX && Position.Y < this.MapSizeY)
            {
                return true;
            }

            return false;
        }

        #endregion
    }

    public enum TileState
    {
        OPEN,
        CLOSED
    }
}
