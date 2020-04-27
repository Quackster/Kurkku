using Kurkku.Messages.Outgoing;
using System;

namespace Kurkku.Game
{
    public class RoomMapping : ILoadable
    {
        #region Fields

        private Room room;
        private RoomModel model;

        #endregion

        #region Properties

        public RoomTile[,] Tiles { get; private set; }

        #endregion

        #region Constructors

        public RoomMapping(Room room)
        {
            this.room = room;
            this.model = room.Model;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Regenerate the room map
        /// </summary>
        public void Load()
        {
            Tiles = new RoomTile[model.MapSizeX, model.MapSizeY];

            for (int y = 0; y < model.MapSizeY; y++)
            {
                for (int x = 0; x < model.MapSizeX; x++)
                {
                    Tiles[x, y] = new RoomTile(
                        room,
                        new Position(x, y),
                        model.TileHeights[x, y],
                        model.TileStates[x, y]
                    );
                }
            }

            foreach (var item in room.ItemManager.Items.Values)
            {
                if (item.Definition.HasBehaviour(ItemBehaviour.WALL_ITEM))
                    continue;

                foreach (Position position in AffectedTile.GetAffectedTiles(item))
                {
                    var tile = position.GetTile(room);

                    if (tile == null)
                        continue;

                    tile.AddItem(item);
                }
            }
        }

        /// <summary>
        /// Add item to map handler
        /// </summary>
        internal void AddItem(Item item, Position position = null, string wallPosition = null, Player player = null)
        {
            item.Data.RoomId = room.Data.Id;

            if (item.Definition.HasBehaviour(ItemBehaviour.WALL_ITEM))
            {
                item.Data.WallPosition = wallPosition;
                room.Send(new WallItemComposer(item));
            }
            else
            {
                RoomTile tile = position.GetTile(room);

                if (tile == null)
                    return;

                position.Z = tile.TileHeight;

                item.Position = position;
                item.Data.X = position.X;
                item.Data.Y = position.Y;
                item.Data.Z = position.Z;
                item.Data.Rotation = position.Rotation;

                room.Send(new FloorItemComposer(item));

                foreach (var affectedPosition in AffectedTile.GetAffectedTiles(item))
                {
                    var roomTile = affectedPosition.GetTile(room);

                    if (roomTile == null)
                        continue;

                    roomTile.AddItem(item);
                }
                
                item.UpdateEntities();
            }

            item.Save();

            room.ItemManager.AddItem(item);
        }

        /// <summary>
        /// Move item handler
        /// </summary>
        internal void MoveItem(Item item, Position position = null, string wallPosition = null)
        {
            if (item.Definition.HasBehaviour(ItemBehaviour.WALL_ITEM))
            {
                item.Data.WallPosition = wallPosition;
                room.Send(new UpdateWallItemComposer(item));
            }
            else
            {
                var oldPosition = item.Position.Copy();
                var oldTile = item.Position.GetTile(room);

                if (oldTile == null)
                    return;

                // Move item from tile
                foreach (var affectedPosition in AffectedTile.GetAffectedTiles(item))
                {
                    var roomTile = affectedPosition.GetTile(room);

                    if (roomTile == null)
                        continue;

                    roomTile.RemoveItem(item);
                }

                var newTile = position.GetTile(room);

                if (newTile == null)
                    return;

                position.Z = newTile.TileHeight;

                item.Position = position;
                item.Data.X = position.X;
                item.Data.Y = position.Y;
                item.Data.Z = position.Z;
                item.Data.Rotation = position.Rotation;

                // Move item to new tile
                foreach (var affectedPosition in AffectedTile.GetAffectedTiles(item))
                {
                    var roomTile = affectedPosition.GetTile(room);

                    if (roomTile == null)
                        continue;

                    roomTile.AddItem(item);
                }

                item.UpdateEntities(oldPosition);
                room.Send(new UpdateFloorItemComposer(item));
            }

            item.Save();
        }


        /// <summary>
        /// Remove item handler
        /// </summary>
        public void RemoveItem(Item item)
        {
            RoomTile tile = item.Position.GetTile(room);

            if (tile == null)
                return;


            foreach (var affectedPosition in AffectedTile.GetAffectedTiles(item))
            {
                var roomTile = affectedPosition.GetTile(room);

                if (roomTile == null)
                    continue;

                roomTile.RemoveItem(item);
            }

            if (item.Definition.HasBehaviour(ItemBehaviour.WALL_ITEM))
            {
                room.Send(new RemoveWallItemComposer(item));
                item.Data.WallPosition = string.Empty;
            }
            else
            {
                room.Send(new RemoveFloorItemComposer(item));
                item.UpdateEntities();

                item.Position = new Position();
                item.Data.X = item.Position.X;
                item.Data.Y = item.Position.Y;
                item.Data.Z = item.Position.Z;
                item.Data.Rotation = item.Position.Rotation;

            }
            item.Data.RoomId = 0;
            item.Save();

            room.ItemManager.RemoveItem(item);
        }

        #endregion
    }
}
