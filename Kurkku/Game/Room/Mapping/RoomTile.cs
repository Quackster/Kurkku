using System.Collections.Generic;
using System.Linq;

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
        public Dictionary<int, IEntity> Entities { get; }

        #endregion

        public RoomTile(Room room, Position position, double tileHeight, TileState tileState)
        {
            _room = room;
            Position = position;
            TileHeight = tileHeight;
            TileState = tileState;
            WalkingHeight = tileHeight;
            Entities = new Dictionary<int, IEntity>();
        }

        /// <summary>
        /// Get if the tile is valid for this entity
        /// </summary>
        public static bool IsValidTile(Room room, IEntity entity, Position position, bool lastStep = false)
        {
            var tile = position.GetTile(room);

            if (tile == null || tile.TileState == TileState.CLOSED)
                return false;

            // Return true always... otherwise the user will be stuck
            if (entity.RoomEntity.Position == position)
                return true;
            
            // If the room doesn't allow walking through users OR it's the last step, then check if a user that IS NOT you is on the tile
            if (!room.Data.AllowWalkthrough || lastStep)
                if (tile.Entities.Count(x => x.Value != entity) > 0)
                    return false;
            
            return true;
        }

        /// <summary>
        /// Add entity to the tile
        /// </summary>
        public void AddEntity(IEntity entity)
        {
            Entities.Add(entity.RoomEntity.InstanceId, entity);
        }

        /// <summary>
        /// Remove entity from tile
        /// </summary>
        public void RemoveEntity(IEntity entity)
        {
            Entities.Remove(entity.RoomEntity.InstanceId);
        }
    }
}
