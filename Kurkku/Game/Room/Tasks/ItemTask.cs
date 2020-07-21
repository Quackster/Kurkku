using log4net;
using System;
using Kurkku.Util.Extensions;
using System.Collections.Concurrent;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Kurkku.Game
{
    public class ItemTickTask : IRoomTask
    {
        #region Fields

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        private Room room;
        private ConcurrentQueue<ITaskObject> tickedItems;
        private List<ITaskObject> queuedItems;

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
            this.tickedItems = new ConcurrentQueue<ITaskObject>(); 
        }

        /// <summary>
        /// Run method called every 500ms
        /// </summary>
        public override void Run(object sender, ElapsedEventArgs e)
        {
            try
            {
                queuedItems = new List<ITaskObject>();
                queuedItems.AddRange(room.ItemManager.Items.Values.Where(x => x.Interactor.TaskObject != null).Select(x => x.Interactor.TaskObject).ToList());
                queuedItems.AddRange(room.EntityManager.GetEntities<Player>().Where(x => x.RoomEntity.TaskObject != null).Select(x => x.RoomEntity.TaskObject).ToList());

                foreach (var taskObject in queuedItems)
                {
                    if (taskObject.RequiresTick)
                    {
                        if (taskObject.CanTick())
                        {
                            taskObject.OnTick();

                            if (!tickedItems.Contains(taskObject))
                                tickedItems.Enqueue(taskObject);
                        }
                    }

                    if (taskObject.EventQueue.Count > 0)
                        taskObject.TryTickState();
                }

                foreach (var taskObject in tickedItems.Dequeue())
                    taskObject.OnTickComplete();
            }
            catch (Exception ex)
            {
                log.Error("Item tick task crashed: ", ex);
            }
        }
    }
}
