using log4net;
using System;
using Kurkku.Messages.Outgoing;
using Kurkku.Util;

namespace Kurkku.Game
{
    public class MaintenanceTask : IRoomTask
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MaintenanceTask));
        private Room room;

        /// <summary>
        /// Set task interval, which is 1000ms for user maintenance
        /// </summary>
        public override int Interval => 1000;

        /// <summary>
        /// Constructor for the entity task
        /// </summary>
        public MaintenanceTask(Room room)
        {
            this.room = room;
        }

        /// <summary>
        /// Run method called every 500ms
        /// </summary>
        /// <param name="state">whatever this means??</param>
        public override void Run(object state)
        {
            try
            {
                foreach (IEntity entity in room.Entities.Values)
                {
                    if (entity.RoomEntity.RoomId != room.Data.Id)
                        continue;

                    ProcessEntity(entity);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// Process user inside room
        /// </summary>
        /// <param name="entity">the entity to process</param>
        private void ProcessEntity(IEntity entity)
        {
            if (entity is Player player)
            {
                if (player.RoomUser.TimerManager.SpeechBubbleDate != -1 && DateUtil.GetUnixTimestamp() > player.RoomUser.TimerManager.SpeechBubbleDate)
                {
                    player.RoomUser.TimerManager.ResetSpeechBubbleTimer();
                    player.RoomUser.Room.Send(new TypingStatusComposer(player.RoomUser.InstanceId, false));
                }
            }
        }
    }
}
