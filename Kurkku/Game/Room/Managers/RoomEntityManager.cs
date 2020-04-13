using Kurkku.Messages.Outgoing;
using Kurkku.Storage.Database.Access;
using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Util.Extensions;

namespace Kurkku.Game.Managers
{
    public class RoomEntityManager
    {
        #region Fields

        private Room room;
        private int instanceCounter;

        #endregion

        #region Constructors

        public RoomEntityManager(Room room)
        {
            this.room = room;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Generate instance ID for new entity that entered room
        /// </summary>
        private int GenerateInstanceId()
        {
            return instanceCounter++;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Enter room handler, used when user clicks room to enter
        /// </summary>
        public void EnterRoom(IEntity entity, Position entryPosition = null)
        {
            SilentlyEntityRoom(entity, entryPosition);

            if (!(entity is Player player))
                return;

            player.Send(new RoomReadyComposer(room.Data.Model, room.Data.Id));

        }

        /// <summary>
        /// Silently enter room handler for every other entity type
        /// </summary>
        public void SilentlyEntityRoom(IEntity entity, Position entryPosition = null)
        {
            if (entity.RoomEntity.Room != null)
                entity.RoomEntity.Room.EntityManager.LeaveRoom(entity);

            if (!RoomManager.Instance.HasRoom(room.Data.Id))
                RoomManager.Instance.AddRoom(room);

            entity.RoomEntity.Reset();
            entity.RoomEntity.Room = room;
            entity.RoomEntity.InstanceId = GenerateInstanceId();
            entity.RoomEntity.Position = (entryPosition ?? room.Model.Door);

            room.Entities.TryAdd(entity.RoomEntity.InstanceId, entity);

            if (entity is Player player)
            {
                // We're in room, yayyy >:)
                player.Messenger.SendStatus();

                room.Data.UsersNow++;
                RoomDao.SaveRoom(room.Data);
            }
        }

        /// <summary>
        /// Leave room handler, called when user leaves room, clicks another room, re-enters room, and disconnects
        /// </summary>
        public void LeaveRoom(IEntity entity, bool hotelView = false)
        {
            room.Entities.Remove(entity.RoomEntity.InstanceId);
            entity.RoomEntity.Reset();

            if (entity is Player player)
            {
                room.Data.UsersNow--;
                RoomDao.SaveRoom(room.Data);

                if (hotelView)
                    player.Send(new CloseConnectionComposer());
                
                player.Messenger.SendStatus();
            }
        }

        #endregion
    }
}
