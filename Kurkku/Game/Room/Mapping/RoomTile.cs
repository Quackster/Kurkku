using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public class RoomTile
    {
        #region Fields

        private Room _room;

        #endregion

        #region Properties

        public Position Position { get; }
        public double TileHeight { get; }
        public double WalkingHeight { get; set; }
        public TileState TileState { get; }

        #endregion

        public RoomTile(Room room, Position position, double tileHeight, TileState tileState)
        {
            _room = room;
            Position = position;
            TileHeight = tileHeight;
            TileState = tileState;
            WalkingHeight = tileHeight;
        }

        /// <summary>
        /// Get if the tile is valid for this entity
        /// </summary>
        public static bool IsValidTile(Room room, IEntity entity, Position position)
        {
            var tile = position.GetTile(room);

            if (tile == null || tile.TileState == TileState.CLOSED)
                return false;

            return true;
        }
    }
}
