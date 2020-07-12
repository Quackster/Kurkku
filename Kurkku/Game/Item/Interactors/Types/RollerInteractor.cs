using Kurkku.Messages.Outgoing;
using Kurkku.Util.Extensions;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kurkku.Game
{
    public class RollerInteractor : Interactor
    {
        #region Fields

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        #endregion

        public override ExtraDataType ExtraDataType => ExtraDataType.StringData;
        public QueuedRollerData queuedRoller;

        public RollerInteractor(Item item) : base(item) { }

        public override void OnTick()
        {
            try
            {
                var room = Item.Room;
                var roller = this.Item;

                var itemsRolling = new Dictionary<Item, Tuple<Item, Position>>();
                var entitiesRolling = new Dictionary<IEntity, Tuple<Item, Position>>();

                if (roller.CurrentTile == null)
                    return;

                var rollerTile = roller.CurrentTile;
                var currentRollerData = new QueuedRollerData(roller);

                // Process items on rollers
                foreach (Item item in rollerTile.GetTileItems())
                {
                    if (item.Definition.HasBehaviour(ItemBehaviour.ROLLER))
                        continue;

                    if (itemsRolling.ContainsKey(item))
                        continue;

                    RoomTaskManager.RollerItemTask.TryGetRollingData(item, roller, room, out var nextPosition);

                    if (nextPosition != null)
                    {
                        itemsRolling.Add(item, Tuple.Create(roller, nextPosition));
                        currentRollerData.RollingItems.Add(item.RollingData);
                    }

                }

                // Process entities on rollers
                var rollerEntities = rollerTile.Entities;

                if (rollerEntities != null && rollerEntities.Count > 0)
                {
                    var entity = rollerEntities.Values.Select(x => x).FirstOrDefault();

                    if (!entitiesRolling.ContainsKey(entity))
                    {
                        RoomTaskManager.RollerEntityTask.TryGetRollingData(entity, roller, room, out var nextPosition);

                        if (nextPosition != null)
                        {
                            entitiesRolling.Add(entity, Tuple.Create(roller, nextPosition));
                            currentRollerData.RollingEntity = entity;
                        }
                    }
                }

                // We're rolling! So set the variable
                if (currentRollerData.RollingItems.Count > 0 || currentRollerData.RollingEntity != null)
                {
                    queuedRoller = currentRollerData;
                }
            }
            catch (Exception ex)
            {
                log.Error("RollerTask crashed: ", ex);
            }
        }

        public override void OnTickComplete()
        {
            if (queuedRoller != null)
            {
                // Perform roll animation for entity
                var queuedRollerData = queuedRoller;
                var entity = queuedRollerData.RollingEntity;

                if (entity != null)
                {
                    RoomTaskManager.RollerEntityTask.DoRoll(entity, entity.RoomEntity.RollingData.Roller, Item.Room, entity.RoomEntity.RollingData.FromPosition, entity.RoomEntity.RollingData.NextPosition);
                }

                // Perform roll animation for item
                foreach (var item in queuedRollerData.RollingItems)
                {
                    if (!item.RollingItem.IsRollingBlocked)
                        RoomTaskManager.RollerItemTask.DoRoll(item.RollingItem, item.Roller, Item.Room, item.FromPosition, item.NextPosition);

                    item.RollingItem.Save();
                }

                // Send roller packet
                if (queuedRollerData.RollingItems.Count > 0 || queuedRollerData.RollingEntity != null)
                {
                    var rollingItems = new List<RollingData>(queuedRollerData.RollingItems);
                    rollingItems.RemoveAll(item => item.RollingItem.IsRollingBlocked);

                    var entityRollerData = queuedRollerData.RollingEntity == null ? null :
                            (queuedRollerData.RollingEntity.RoomEntity == null ? null : queuedRollerData.RollingEntity.RoomEntity.RollingData);

                    Item.Room.Send(new SlideObjectBundleComposer(queuedRollerData.Roller, rollingItems, entityRollerData));

                    // Delay after rolling finished
                    Task.Delay(800).ContinueWith(t =>
                    {
                        foreach (RollingData rollingData in queuedRollerData.RollingItems)
                        {
                            rollingData.RollingItem.IsRollingBlocked = false;
                            rollingData.RollingItem.RollingData = null;
                        }

                        if (entity != null)
                        {
                            if (entity.RoomEntity.RollingData != null)
                            {
                                entity.RoomEntity.InteractItem();//getRoomUser().invokeItem(null, true);
                                entity.RoomEntity.RollingData = null;
                            }
                        }
                    });
                }

                queuedRoller = null;
            }

            SetTicks(RoomTaskManager.GetProcessTime(2.0));
        }
    }
}
