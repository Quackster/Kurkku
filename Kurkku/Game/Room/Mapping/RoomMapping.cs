using System;
using Kurkku.Messages.Outgoing;

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
        /// Mapping item handler for adding/removing item to collision map
        /// </summary>
        public void AddItem(Item item)
        {
            // Add item to tile
            foreach (var affectedPosition in AffectedTile.GetAffectedTiles(item))
            {
                var roomTile = affectedPosition.GetTile(room);

                if (roomTile == null)
                    continue;

                roomTile.AddItem(item);
            }

        }

        /// <summary>
        /// Mapping item handler for adding/removing item to collision map
        /// </summary>
        public void RemoveItem(Item item)
        {
            // Remove item from tile
            foreach (var affectedPosition in AffectedTile.GetAffectedTiles(item))
            {
                var roomTile = affectedPosition.GetTile(room);

                if (roomTile == null)
                    continue;

                roomTile.RemoveItem(item);
            }

        }

        #endregion
    }
}
