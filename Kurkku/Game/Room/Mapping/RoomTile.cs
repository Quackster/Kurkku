using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Kurkku.Util.Extensions;

namespace Kurkku.Game
{
    public class RoomTile
    {
        #region Fields

        private Room _room;

        #endregion

        #region Properties

        public Position Position { get; }
        public double TileHeight { get; set; }
        public double DefaultHeight { get; }
        public double WalkingHeight { get; set; }
        public TileState TileState { get; }
        public ConcurrentDictionary<int, IEntity> Entities { get; }
        public ConcurrentDictionary<int, Item> Furniture { get; }
        public Item HighestItem { get; set; }

        #endregion

        public RoomTile(Room room, Position position, double tileHeight, TileState tileState)
        {
            _room = room;
            Position = position;
            TileHeight = tileHeight;
            DefaultHeight = tileHeight;
            TileState = tileState;
            WalkingHeight = tileHeight;
            Entities = new ConcurrentDictionary<int, IEntity>();
            Furniture = new ConcurrentDictionary<int, Item>();
        }

        /// <summary>
        /// Get if the tile is valid for this entity
        /// </summary>
        public static bool IsValidTile(Room room, IEntity entity, Position position, bool lastStep = false)
        {
            if (room == null || entity == null || position == null)
                return false;

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

            if (tile.HighestItem != null)
            {
                if (!tile.HighestItem.IsWalkable(position))
                    return false;
            }
            
            return true;
        }

        /// <summary>
        /// Add entity to the tile
        /// </summary>
        public void AddEntity(IEntity entity)
        {
            if (entity == null)
                return;

            Entities.TryAdd(entity.RoomEntity.InstanceId, entity);
        }

        public double GetWalkingHeight()
        {
            double height = TileHeight;

            if (HighestItem != null)
            {
                if (HighestItem.Definition.InteractorType == InteractorType.CHAIR ||
                    HighestItem.Definition.InteractorType == InteractorType.BED)
                {
                    height -= HighestItem.Definition.Data.TopHeight;
                }
            }

            return height;
        }

        /// <summary>
        /// Remove entity from tile
        /// </summary>
        public void RemoveEntity(IEntity entity)
        {
            if (entity == null)
                return;

            Entities.Remove(entity.RoomEntity.InstanceId);
        }

        /// <summary>
        /// Reset highest item, called after an item in the tile has been changed
        /// </summary>
        private void ResetHighestItem()
        {
            HighestItem = null;
            TileHeight = DefaultHeight;

            foreach (var item in Furniture.Values)
            {
                if (item == null)
                    continue;

                double height = item.Height;

                // TODO: Ignore stack helpers

                if (height < TileHeight)
                    continue;

                HighestItem = item;
                TileHeight = height;
            }
        }

        /// <summary>
        /// Add item to tile map
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            if (item == null)
                return;

            Furniture.TryAdd(item.Id, item);

            if (item.Height < TileHeight) // TODO: Stack helper?
                return;

            ResetHighestItem();
        }

        /// <summary>
        /// Remove item from tile map
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(Item item)
        {
            if (item == null)
                return;

            Furniture.Remove(item.Id);

            if (HighestItem == null || item.Id != HighestItem.Id) // TODO: Stack helper?
                return;

            ResetHighestItem();
        }

    }
}
