using log4net;
using System;
using Kurkku.Util.Extensions;
using System.Collections.Concurrent;
using System.Reflection;

namespace Kurkku.Game
{
    public class ItemTickTask : IRoomTask
    {
        #region Fields

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        private Room room;
        private ConcurrentQueue<Item> tickedItems;// = new ConcurrentQueue<Item>();

        /// <summary>
        /// Set task interval, which is 500ms
        /// </summary>
        public override int Interval => 500;

        /// <summary>
        /// Constructor for the item task
        /// </summary>
        public ItemTickTask(Room room)
        {
            this.room = room;
            this.tickedItems = new ConcurrentQueue<Item>(); 
        }

        /// <summary>
        /// Run method called every 500ms
        /// </summary>
        public override void Run(object state)
        {
            try
            {
                foreach (Item item in room.ItemManager.Items.Values)
                {
                    if (item.Interactor.RequiresTick || item.Interactor.QueuedStates.Count > 0)
                    {
                        if (item.Interactor.CanTick())
                        {
                            item.Interactor.OnTick();
                            tickedItems.Enqueue(item);
                        }
                    }
                }

                foreach (var item in tickedItems.Dequeue())
                {
                    item.Interactor.OnTickComplete();
                }
            }
            catch (Exception ex)
            {
                log.Error("Item tick task crashed: ", ex);
            }
        }
    }
}
