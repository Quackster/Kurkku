using Kurkku.Messages.Outgoing;
using Kurkku.Storage.Database.Access;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game.Managers
{
    public class RoomEntityManager
    {
        private Room room;
        private int instanceCounter;

        public RoomEntityManager(Room room)
        {
            this.room = room;
        }

        /// <summary>
        /// Generate instance ID for new entity that entered room
        /// </summary>
        private int GenerateInstanceId()
        {
            return instanceCounter++;
        }

        public void EnterRoom(IEntity entity, Position entryPosition = null)
        {
            SilentlyEntityRoom(entity, entryPosition);

            if (!(entity is Player player))
                return;

            player.Send(new RoomReadyComposer(room.Data.Model, room.Data.Id));


        }

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

            room.Entities.Add(entity);

            if (entity is Player player)
            {
                // We're in room, yayyy >:)
                player.Messenger.SendStatus();

                room.Data.UsersNow++;
                RoomDao.SaveRoom(room.Data);
            }
        }

        public void LeaveRoom(IEntity entity, bool hotelView = false)
        {
            if (entity is Player player)
            {
                room.Data.UsersNow--;
                RoomDao.SaveRoom(room.Data);
            }
        }
    }
}
