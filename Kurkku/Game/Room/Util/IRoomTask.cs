
using System;
using System.Timers;

namespace Kurkku.Game
{
    public abstract class IRoomTask
    {
        #region Properties

        public Timer Task { get; private set; }
        public abstract int Interval { get; }

        #endregion

        #region Public methods

        /// <summary>
        /// Create the task for the room to handle walking
        /// </summary>
        public void CreateTask()
        {
            if (Task != null)
                return;

            Task = new Timer();
            Task.Interval = Interval;
            Task.Elapsed += Run;
            Task.Enabled = true;
            Task.Start();
        }

        /// <summary>
        /// Stops the task for the room to handle walking
        /// </summary>
        public void StopTask()
        {
            if (Task == null)
                return;

            Task.Dispose();
            Task = null;
        }

        public abstract void Run(object sender, ElapsedEventArgs e);

        #endregion
    }
}
