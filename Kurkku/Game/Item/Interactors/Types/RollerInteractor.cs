using Kurkku.Messages.Outgoing;
using Kurkku.Util.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurkku.Game
{
    public class RollerInteractor : Interactor
    {
        public override ExtraDataType ExtraDataType => ExtraDataType.StringData;

        public RollerInteractor(Item item) : base(item) { }

        public override void OnTickComplete()
        {
            try
            {
                var room = Item.Room;
                var roller = this.Item;

                var itemsRolling = new Dictionary<Item, Tuple<Item, Position>>();
                var entitiesRolling = new Dictionary<IEntity, Tuple<Item, Position>>();

                var rollerEntries = new List<QueuedRollerData>();

                if (roller.CurrentTile == null)
                    return;

                var rollerTile = roller.CurrentTile;

                QueuedRollerData rollerEntry = new QueuedRollerData(roller);

                // Process items on rollers
                foreach (Item item in rollerTile.GetTileItems())
                {
                    if (item.Definition.HasBehaviour(ItemBehaviour.ROLLER))
                        continue;

                    if (itemsRolling.ContainsKey(item))
                        continue;

                    Position nextPosition = RoomTaskManager.RollerItemTask.CanRoll(item, roller, room);

                    if (nextPosition != null)
                    {
                        itemsRolling.Add(item, Tuple.Create(roller, nextPosition));
                        rollerEntry.RollingItems.Add(item.RollingData);
                    }

                }

                // Process entities on rollers
                var rollerEntities = rollerTile.Entities;

                if (rollerEntities != null && rollerEntities.Count > 0)
                {
                    var entity = rollerEntities.Values.Select(x => x).FirstOrDefault();

                    if (!entitiesRolling.ContainsKey(entity))
                    {
                        Position nextPosition = RoomTaskManager.RollerEntityTask.CanRoll(entity, roller, room);

                        if (nextPosition != null)
                        {
                            entitiesRolling.Add(entity, Tuple.Create(roller, nextPosition));
                            rollerEntry.RollingEntity = entity;
                        }
                    }
                }

                // Roller entry has items or entities to roll so make sure the packet gets senr
                if (rollerEntry.RollingEntity != null || rollerEntry.RollingItems.Count > 0)
                    rollerEntries.Add(rollerEntry);

                // Perform roll animation for entity
                foreach (var kvp in entitiesRolling)
                    RoomTaskManager.RollerEntityTask.DoRoll(kvp.Key, kvp.Value.Item1, room, kvp.Key.RoomEntity.Position, kvp.Value.Item2);

                // Perform roll animation for item
                foreach (var kvp in itemsRolling)
                {
                    Item item = kvp.Key;

                    if (!item.IsRollingBlocked)
                        RoomTaskManager.RollerItemTask.DoRoll(kvp.Key, kvp.Value.Item1, room, kvp.Key.Position, kvp.Value.Item2);

                    item.Save();
                }

                // Send roller packets
                foreach (QueuedRollerData entry in rollerEntries)
                {
                    var rollingItems = new List<RollingData>(entry.RollingItems);
                    rollingItems.RemoveAll(item => item.RollingItem.IsRollingBlocked);

                    var entityRollerData = entry.RollingEntity == null ? null :
                            (entry.RollingEntity.RoomEntity == null ? null : entry.RollingEntity.RoomEntity.RollingData);

                    room.Send(new SlideObjectBundleComposer(entry.Roller, rollingItems, entityRollerData));
                }

                if (itemsRolling.Count > 0 || entitiesRolling.Count > 0)
                {
                    Task.Delay(800).ContinueWith(t =>
                    {
                        foreach (Item item in itemsRolling.Keys)
                        {
                            if (item.RollingData == null)
                                continue;

                            item.IsRollingBlocked = false;
                            item.RollingData = null;
                        }

                        foreach (IEntity entity in entitiesRolling.Keys)
                        {
                            if (entity.RoomEntity.RollingData == null)
                                continue;

                            entity.RoomEntity.InteractItem();//getRoomUser().invokeItem(null, true);
                            entity.RoomEntity.RollingData = null;
                        }
                    });
                }

                /*if (itemsRolling.size() > 0 || entitiesRolling.size() > 0)
                {
                    this.room.getMapping().regenerateCollisionMap();
                    GameScheduler.getInstance().getService().schedule(new RollerCompleteTask(itemsRolling.keySet(), entitiesRolling.keySet(), this.room), 800, TimeUnit.MILLISECONDS);
                }*/
            }
            catch (Exception ex)
            {
                //Log.getErrorLogger().error("RollerTask crashed: ", ex);
            }

            SetTicks(RoomTaskManager.GetProcessTime(2.0));
        }
    }
}
