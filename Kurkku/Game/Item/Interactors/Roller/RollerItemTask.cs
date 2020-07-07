﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kurkku.Game
{
    public class RollerItemTask : IRollerTask<Item>
    {
        #region Public methods

        public Position CanRoll(Item item, Item roller, Room room)
        {
            if (roller == null || roller.Room == null || roller.CurrentTile == null)
            {
                return null;
            }

            if (item.Id == roller.Id)
            {
                return null;
            }

            if (item.Position.Z < roller.Position.Z)
            {
                return null;
            }

            Position front = roller.Position.GetSquareInFront();
            RoomTile frontTile = front.GetTile(room);

            if (frontTile == null)
            {
                return null;
            }

            // Check all entities in the room
            foreach (IEntity e in room.Entities.Values)
            {
                if (e.RoomEntity.Room == null)
                {
                    continue;
                }

                // Don't roll if an entity is going to walk into the furniture
                if (e.RoomEntity.Next != null)
                {
                    if (e.RoomEntity.Next == front)
                    {
                        return null;
                    }
                }

                // Ignore people who are walking
                if (e.RoomEntity.IsWalking)
                {
                    continue;
                }

                // Don't roll if there's an entity rolling into you
                if (e.RoomEntity.RollingData != null)
                {
                    if (e.RoomEntity.RollingData.NextPosition == front)
                    {
                        return null;
                    }
                }

                if (e.RoomEntity.Position == front)
                {
                    return null;
                }
            }

            // Check all rolling items in the room
            foreach (Item floorItem in room.ItemManager.Items.Values.Where(x => !x.Definition.HasBehaviour(ItemBehaviour.WALL_ITEM)).ToList())
            {
                if (floorItem.RollingData != null)
                {
                    if (floorItem.Position == roller.Position)
                    {
                        continue;
                    }

                    // Don't roll if there's another item that's going to roll into this item
                    if (floorItem.RollingData.NextPosition == front)//.getRollingData().getNextPosition().equals(front))
                    {
                        return null;
                    }
                }
            }

            double nextHeight = item.Position.Z;//this.room.getModel().getTileHeight(roller.Position.getX(), roller.Position.getY());
            bool subtractRollerHeight = true;

            if (frontTile.HighestItem != null)
            {
                Item frontRoller = null;

                foreach (Item frontItem in frontTile.GetTileItems())
                {
                    if (!frontItem.Definition.HasBehaviour(ItemBehaviour.ROLLER))
                    {
                        continue;
                    }

                    frontRoller = frontItem;
                }

                if (frontRoller != null)
                {
                    subtractRollerHeight = false;

                    if (frontRoller.Position.Z != roller.Position.Z)
                    {
                        if (Math.Abs(frontRoller.Position.Z - roller.Position.Z) > 0.1)
                        {
                            return null; // Don't roll if the height of the roller is different by >0.1
                        }
                    }

                    foreach (Item frontItem in frontTile.GetTileItems())
                    {
                        if (frontItem.Position.Z < frontRoller.Position.Z)
                        {
                            continue;
                        }

                        if (frontItem.Id == item.Id)
                        {
                            continue;
                        }

                        if (frontItem.Definition.HasBehaviour(ItemBehaviour.ROLLER))
                        {
                            Position frontPosition = frontRoller.Position.GetSquareInFront();

                            // Don't roll an item into the next roller, if the next roller is facing towards the roller
                            // it just rolled from, and the next roller has an item on it.
                            if (frontPosition == item.Position)
                            {
                                if (frontTile.GetItemsAbove(frontRoller).Count > 0 || frontTile.Entities.Count > 0)
                                {
                                    return null;

                                }
                            }
                        }
                    }

                    Item highestNextItem = frontTile.HighestItem;

                    if (!highestNextItem.Definition.HasBehaviour(ItemBehaviour.ROLLER))
                    {
                        if (highestNextItem.Definition.HasBehaviour(ItemBehaviour.IS_WALKABLE)
                                && item.CurrentTile.Entities.IsEmpty)
                        {

                            foreach (Item frontItem in frontRoller.CurrentTile.GetTileItems())
                            {
                                frontItem.IsRollingBlocked = true;
                            }

                            nextHeight = (highestNextItem.Position.Z + highestNextItem.Definition.Data.TopHeight) +
                                item.Position.Z - frontRoller.Definition.Data.TopHeight;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    if (!RoomTile.IsValidTile(room, null, frontTile.Position))
                    {
                        return null;
                    }
                }
            }

            if (subtractRollerHeight)
            {
                nextHeight -= roller.Definition.Data.TopHeight;
            }

            /*if (nextHeight > GameConfiguration.getInstance().getInteger("stack.height.limit"))
            {
                nextHeight = GameConfiguration.getInstance().getInteger("stack.height.limit");
            }*/

            Position nextPosition = new Position(front.X, front.Y, nextHeight);

            item.RollingData = new RollingData
            {
                RollingItem = item,
                Roller = roller,
                FromPosition = item.Position.Copy(),
                NextPosition = nextPosition.Copy()
            };

            //item.setRollingData(new RollingData(item, roller, item.Position.Copy(), nextPosition));
            return nextPosition;
        }

        public void DoRoll(Item item, Item roller, Room room, Position fromPosition, Position nextPosition)
        {
            room.Mapping.MapItem(item, RoomMapping.MappingAction.REMOVE);

            item.Position.X = nextPosition.X;
            item.Position.Y = nextPosition.Y;
            item.Position.Z = nextPosition.Z;

            room.Mapping.MapItem(item, RoomMapping.MappingAction.ADD);
        }

        #endregion
    }
}