using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Game
{
    public class RoomTaskManager : ILoadable
    {
        #region Fields

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
            if (!Tasks.Any())
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
            Tasks.Add(new MaintenanceTask(room));
        }

        /// <summary>
        /// Stop all registered tasks
        /// </summary>
        public void StopTasks()
        {
            foreach (var task in Tasks)
                task.StopTask();
        }

        #endregion
    }
}
