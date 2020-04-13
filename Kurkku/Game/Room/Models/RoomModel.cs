using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;
using System.Text;
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

                Heightmap += "\r";
            }
        }

        #endregion
    }

    public enum TileState
    {
        OPEN,
        CLOSED
    }
}
