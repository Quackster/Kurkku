using System;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Game
{
    public class RoomTaskManager : ILoadable
    {
        #region Fields

        public static readonly RollerEntityTask RollerEntityTask = new RollerEntityTask();
        public static readonly RollerItemTask RollerItemTask = new RollerItemTask();

        private Room room;

        #endregion

        #region Properties

        public List<IRoomTask> Tasks { get; }

        #endregion

        #region Constructors

        public RoomTaskManager(Room room)
        {
           this.room = room;
           this.Tasks = new List<IRoomTask>();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Start all registered tasks
        /// </summary>
        public void Load()
        {
            StopTasks();
            Tasks.Clear();
            RegisterTasks();

            foreach (var task in Tasks)
                task.CreateTask();
        }

        /// <summary>
        /// Register all tasks
        /// </summary>
        private void RegisterTasks()
        {
            Tasks.Add(new EntityTask(room));
            //Tasks.Add(new StatusTask(room));
            Tasks.Add(new ItemTickTask(room));
        }

        /// <summary>
        /// Stop all registered tasks
        /// </summary>
        public void StopTasks()
        {
            foreach (var task in Tasks)
                task.StopTask();
        }

        /// <summary>
        /// Convert real time to Habbo tick processing time
        /// </summary>
        public static int GetProcessTime(double time)
        {
            return (int)(time / 0.5);
        }

        #endregion
    }
}
