using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kurkku.Game
{
    public class RollerEntityTask : IRollerTask<IEntity>
    {
        #region Public methods

        public void TryGetRollingData(IEntity entity, Item roller, Room room, out Position nextPosition)
        {
            nextPosition = null;

            if (roller == null || 
                roller.Room == null || 
                roller.CurrentTile == null || 
                entity.RoomEntity.RollingData != null)
            {
                return;
            }

            if (entity.RoomEntity.IsWalking)
                return;

            if (entity.RoomEntity.Position.Z < roller.Position.Z)
                return;

            Position front = roller.Position.GetSquareInFront();
            RoomTile frontTile = front.GetTile(room);

            if (frontTile == null)
                return;

            // Check all entities in the room
            foreach (IEntity e in room.Entities.Values)
            {
                if (e.RoomEntity.Room == null)
                    continue;

                // Don't roll if an entity is going to walk into the furniture
                if (e.RoomEntity.Next != null)
                {
                    if (e.RoomEntity.Next == front)
                        return;
                }

                // Ignore people who are walking
                if (e.RoomEntity.IsWalking)
                    continue;

                // Don't roll if there's an entity rolling into you
                if (e.RoomEntity.RollingData != null)
                {
                    if (e.RoomEntity.RollingData.NextPosition == front)
                        return;
                }

                if (e.RoomEntity.Position == front)
                    return;
            }

            // Check all rolling items in the room
            foreach (Item floorItem in room.ItemManager.Items.Values.Where(x => !x.Definition.HasBehaviour(ItemBehaviour.WALL_ITEM)).ToList())
            {
                if (floorItem.RollingData != null)
                {
                    if (floorItem.Position == roller.Position)
                        continue;

                    // Don't roll if there's another item that's going to roll into this item
                    if (floorItem.RollingData.NextPosition == front)//.getRollingData().getNextPosition().equals(front))
                        return;
                }
            }

            double nextHeight = entity.RoomEntity.Position.Z;//this.room.getModel().getTileHeight(roller.Position.getX(), roller.Position.getY());
            bool subtractRollerHeight = true;

            if (frontTile.HighestItem != null)
            {
                Item frontRoller = null;

                foreach (Item frontItem in frontTile.GetTileItems())
                {
                    if (!frontItem.Definition.HasBehaviour(ItemBehaviour.ROLLER))
                        continue;

                    frontRoller = frontItem;
                }

                if (frontRoller != null)
                {
                    subtractRollerHeight = false;

                    if (frontRoller.Position.Z != roller.Position.Z)
                    {
                        if (Math.Abs(frontRoller.Position.Z - roller.Position.Z) > 0.1)
                            return; // Don't roll if the height of the roller is different by >0.1
                    }

                    foreach (Item frontItem in frontTile.GetTileItems())
                    {
                        if (frontItem.Position.Z < frontRoller.Position.Z)
                            continue;

                        // This is because the ItemRollingAnalysis has setHighestItem in nextTile in doRoll which blocks this
                        if (entity.RoomEntity.CurrentItem != null
                                && entity.RoomEntity.CurrentItem.Id == frontItem.Id)
                        {
                            continue;
                        }

                        if (frontItem.Definition.HasBehaviour(ItemBehaviour.ROLLER))
                        {
                            Position frontPosition = frontRoller.Position.GetSquareInFront();

                            // Don't roll an item into the next roller, if the next roller is facing towards the roller
                            // it just rolled from, and the next roller has an item on it.
                            if (frontPosition == entity.RoomEntity.Position)
                            {
                                if (frontTile.GetItemsAbove(frontRoller).Count > 0 || frontTile.Entities.Count > 0)
                                {
                                    return;

                                }
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else
                {
                    if (!RoomTile.IsValidTile(room, entity, frontTile.Position))
                    {
                        return;
                    }
                }
            }

            if (subtractRollerHeight)
            {
                nextHeight -= roller.Definition.Data.TopHeight;
            }

            Item currentItem = entity.RoomEntity.CurrentItem;

            if (currentItem != null && !currentItem.Definition.HasBehaviour(ItemBehaviour.ROLLER))
            {
                // If we can roll but our item can't, don't roll!
                RoomTaskManager.RollerItemTask.TryGetRollingData(currentItem, roller, room, out var rollPosition);

                if (rollPosition == null)
                    return;
            }

            nextPosition = new Position(front.X, front.Y, nextHeight);
            entity.RoomEntity.RollingData = new RollingData
            {
                RollingEntity = entity,
                Roller = roller,
                FromPosition = entity.RoomEntity.Position.Copy(),
                NextPosition = nextPosition.Copy()
            };
        }

        public void DoRoll(IEntity entity, Item roller, Room room, Position fromPosition, Position nextPosition)
        {
            RoomTile previousTile = fromPosition.GetTile(room);
            RoomTile nextTile = nextPosition.GetTile(room);

            // Temporary fix if the user walks on an item and their height gets put up.
            if (entity.RoomEntity.CurrentItem != null && entity.RoomEntity.CurrentItem.Definition.HasBehaviour(ItemBehaviour.ROLLER))
            {
                if (Math.Abs(entity.RoomEntity.Position.Z - roller.Position.Z) >= 0.1)
                {
                    if (nextTile.HighestItem != null && nextTile.HighestItem.Definition.HasBehaviour(ItemBehaviour.ROLLER))
                    {
                        nextPosition.Z = roller.Position.Z + roller.Definition.Data.TopHeight;
                    }
                }
            }

            // The next height but what the client sees.
            double displayNextHeight = nextPosition.Z;

            //if (entity.getRoomUser().isSittingOnGround())
            //{
            //    displayNextHeight -= 0.5; // Take away sit offset when sitting on ground, because yeah, weird stuff.
            //}

            entity.RoomEntity.RollingData.DisplayHeight = displayNextHeight;//setZ(displayNextHeight);

            entity.RoomEntity.Position.X = nextPosition.X;
            entity.RoomEntity.Position.Y = nextPosition.Y;
            entity.RoomEntity.Position.Z = nextPosition.Z;

            previousTile.RemoveEntity(entity);
            nextTile.AddEntity(entity);

            /*
                    

        // Fix bounce for sitting on chairs if the chair top height is higher 1.0
        if (entity.getRoomUser().containsStatus(StatusType.SIT)) {
            double sitHeight = Double.parseDouble(entity.getRoomUser().getStatus(StatusType.SIT).getValue());

            if (sitHeight > 1.0) {
                displayNextHeight += (sitHeight - 1.0); // Add new height offset found.
            }
        }

        entity.getRoomUser().getRollingData().setDisplayHeight(displayNextHeight);//setZ(displayNextHeight);

        entity.getRoomUser().getPosition().setX(nextPosition.getX());
        entity.getRoomUser().getPosition().setY(nextPosition.getY());
        entity.getRoomUser().getPosition().setZ(nextPosition.getZ());

        previousTile.removeEntity(entity);
        nextTile.addEntity(entity);*/

        }

        #endregion
    }
}
