using System;
using Kurkku.Messages.Outgoing;

namespace Kurkku.Game
{
    public class RoomFurniture
    {
        #region Fields

        private Room room;

        #endregion

        #region Constructors

        public RoomFurniture(Room room)
        {
            this.room = room;
        }

        #endregion

        #region Public methods

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

                item.Data.X = position.X;
                item.Data.Y = position.Y;
                item.Data.Z = position.Z;
                item.Data.Rotation = position.Rotation;
                item.ApplyPosition();

                HandleItemAdjusted(item, false);

                room.Send(new FloorItemComposer(item));
                room.Mapping.AddItem(item);
                item.UpdateEntities();
            }

            item.Interactor.OnPlace(player);
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
                bool isRotation = false;

                if (item.Position == new Position(position.X, position.Y) && item.Position.Rotation != position.Rotation)
                    isRotation = true;

                var oldTile = item.Position.GetTile(room);

                if (oldTile == null)
                    return;

                room.Mapping.RemoveItem(item);

                var newTile = position.GetTile(room);

                if (newTile == null)
                    return;

                position.Z = newTile.TileHeight;

                item.Data.X = position.X;
                item.Data.Y = position.Y;
                item.Data.Z = position.Z;
                item.Data.Rotation = position.Rotation;
                item.ApplyPosition();

                HandleItemAdjusted(item, isRotation);
                room.Mapping.AddItem(item);

                item.UpdateEntities(oldTile.Position);
                room.Send(new UpdateFloorItemComposer(item));
            }


            item.Save();
        }

        private void HandleItemAdjusted(Item item, bool isRotation)
        {
            RoomTile tile = item.Position.GetTile(room);

            if (tile == null)
            {
                return;
            }

            if (!isRotation)
            {
                Item highestItem = tile.HighestItem;
                double tileHeight = tile.TileHeight;

                if (highestItem != null && highestItem.Id == item.Id)
                {
                    tileHeight -= highestItem.Height;

                    double defaultHeight = room.Model.TileHeights[item.Position.X, item.Position.Y];//this.room.getModel().getTileHeight(item.getPosition().getX(), item.getPosition().getY());

                    if (tileHeight < defaultHeight)
                    {
                        tileHeight = defaultHeight;
                    }
                }

                item.Position.Z = tileHeight;//.getPosition().setZ(tileHeight);

                if (highestItem != null && highestItem.RollingData != null)
                {
                    Item roller = highestItem.RollingData.Roller;

                    if (highestItem.GetItemBelow() != null && highestItem.GetItemBelow().Definition.HasBehaviour(ItemBehaviour.ROLLER))
                    {
                        // If the difference between the roller, and the next item up is more than 0.5, then set the item below the floating item
                        if (Math.Abs(highestItem.Position.Z - roller.Position.Z) >= 0.5)
                        {
                            item.Position.Z = roller.Position.Z + roller.Definition.GetPositiveTopHeight();//.getPosition().setZ(roller.getPosition().getZ() + roller.getDefinition().getPositiveTopHeight());
                        }
                    }

                    item.Position.Z = roller.Position.Z + roller.Definition.GetPositiveTopHeight();//getPosition().setZ(roller.getPosition().getZ() + roller.getDefinition().getPositiveTopHeight());

                    /*if (!highestItem.hasBehaviour(ItemBehaviour.CAN_STACK_ON_TOP)) {
                        item.getPosition().setZ(roller.getPosition().getZ() + roller.getDefinition().getTopHeight());

                        /*for (Item tileItem : tile.getItems()) {
                            if (tileItem.getPosition().getZ() >= item.getPosition().getZ()) {
                                tileItem.getRollingData().setHeightUpdate(item.getDefinition().getTopHeight());
                            }
                        }
                    }*/
                }
            }
        }


        /// <summary>
        /// Remove item handler
        /// </summary>
        public void RemoveItem(Item item, Player player)
        {
            RoomTile tile = item.Position.GetTile(room);

            if (tile == null)
                return;

            room.Mapping.RemoveItem(item);

            if (item.Definition.HasBehaviour(ItemBehaviour.WALL_ITEM))
            {
                room.Send(new RemoveWallItemComposer(item));
                item.Data.WallPosition = string.Empty;
            }
            else
            {
                room.Send(new RemoveFloorItemComposer(item));
                item.UpdateEntities();

                item.Data.X = item.Position.X;
                item.Data.Y = item.Position.Y;
                item.Data.Z = item.Position.Z;
                item.Data.Rotation = item.Position.Rotation;
                item.ApplyPosition();
            }

            item.Interactor.OnPickup(player);

            item.Data.RoomId = 0;
            item.Save();

            room.ItemManager.RemoveItem(item);
        }

        #endregion
    }
}
