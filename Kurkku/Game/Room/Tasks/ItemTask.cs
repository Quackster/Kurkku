using log4net;
using System;
using System.Collections.Generic;
using Kurkku.Util.Extensions;
using Kurkku.Messages.Outgoing;
using System.Linq;

namespace Kurkku.Game
{
    public class QueuedItemTask
    {
        private Item item;
        private long runningTick;
        private long resetEveryTick;

        public QueuedItemTask(Item item, long runningTick, long resetEveryTick)
        {
            this.item = item;
            this.runningTick = runningTick;
            this.resetEveryTick = resetEveryTick;
        }
    }

    public class ItemTask : IRoomTask
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(EntityTask));
        private Room room;

        /// <summary>
        /// Set task interval, which is 500ms
        /// </summary>
        public override int Interval => 500;

        /// <summary>
        /// Constructor for the item task
        /// </summary>
        public ItemTask(Room room)
        {
            this.room = room;
        }

        /// <summary>
        /// Run method called every 500ms
        /// </summary>
        public override void Run(object state)
        {
            try
            {
                var entityUpdates = new List<IEntity>();

                foreach (Item item in room.ItemManager.Items.Values)
                {
                    if (item.Interactor.RequiresTick())
                    {
                        item.Interactor.Tick();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}
