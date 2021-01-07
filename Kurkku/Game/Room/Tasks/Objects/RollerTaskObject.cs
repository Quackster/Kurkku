using Kurkku.Messages.Outgoing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurkku.Game
{
    public class RollerTaskObject : ITaskObject
    {
        #region Properties

        public double TaskProcessTime { get { return 2; } }
        public List<RollingData> RollingItems { get; set; }
        public IEntity RollingEntity { get; set; }

        #endregion

        #region Override Properties

        public override bool RequiresTick => true;

        #endregion

        #region Constructor

        public RollerTaskObject(Item item) : base(item) { }

        #endregion

        #region Public methods

        public override void OnTick()
        {
            var itemsRolling = new Dictionary<Item, Tuple<Item, Position>>();
            var entitiesRolling = new Dictionary<IEntity, Tuple<Item, Position>>();

            if (Item.CurrentTile == null)
                return;

            var rollerTile = Item.CurrentTile;

            RollingItems = new List<RollingData>();
            RollingEntity = null;

            // Process items on rollers
            foreach (Item item in rollerTile.GetTileItems())
            {
                if (item.Definition.HasBehaviour(ItemBehaviour.ROLLER))
                    continue;

                if (itemsRolling.ContainsKey(item) || item.IsRolling)
                    continue;

                RoomTaskManager.RollerItemTask.TryGetRollingData(item, Item, Item.Room, out var nextPosition);

                if (nextPosition != null)
                {
                    itemsRolling.Add(item, Tuple.Create(Item, nextPosition));
                    RollingItems.Add(item.RollingData);
                }

            }

            // Process entities on rollers
            var rollerEntities = rollerTile.Entities;

            if (rollerEntities != null && rollerEntities.Count > 0)
            {
                var entity = rollerEntities.Values.Select(x => x).FirstOrDefault();

                if (!entitiesRolling.ContainsKey(entity) || entity.RoomEntity.IsRolling)
                {
                    RoomTaskManager.RollerEntityTask.TryGetRollingData(entity, Item, Item.Room, out var nextPosition);

                    if (nextPosition != null)
                    {
                        entitiesRolling.Add(entity, Tuple.Create(Item, nextPosition));
                        RollingEntity = entity;
                    }
                }
            }
        }

        public override void OnTickComplete()
        {
            if (RollingItems.Any() || RollingEntity != null)
            {
                var entity = RollingEntity;
                var rollingItems = new List<RollingData>(RollingItems);

                if (entity != null)
                    RoomTaskManager.RollerEntityTask.DoRoll(entity, entity.RoomEntity.RollingData.Roller, Item.Room, entity.RoomEntity.RollingData.FromPosition, entity.RoomEntity.RollingData.NextPosition);

                // Perform roll animation for item
                foreach (var item in rollingItems)
                {
                    if (item.RollingItem.IsRollingBlocked)
                        continue;

                    RoomTaskManager.RollerItemTask.DoRoll(item.RollingItem, item.Roller, Item.Room, item.FromPosition, item.NextPosition);
                    item.RollingItem.Save();
                }

                // Send roller packet
                if (RollingItems.Count > 0 || RollingEntity != null)
                {
                    Item.Room?.Send(new SlideObjectBundleComposer(Item, rollingItems.Where(x => !x.RollingItem.IsRollingBlocked).ToList(), entity?.RoomEntity?.RollingData));

                    // Delay after rolling finished
                    int delay = (int)(double)(TaskProcessTime * 0.4 * 1000);
                    Task.Delay(delay).ContinueWith(t =>
                    {
                        foreach (RollingData rollingData in rollingItems)
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
            }

            TicksTimer = RoomTaskManager.GetProcessTime(TaskProcessTime);
        }

        #endregion
    }
}
