using Kurkku.Messages.Outgoing;
using Kurkku.Storage.Database.Access;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Select entities where it's assignable by entity type
        /// </summary>
        public List<T> GetEntities<T>()
        {
            return room.Entities
                .Where(x => x.Value.GetType().IsAssignableFrom(typeof(T)) || x.Value.GetType().GetInterfaces().Contains(typeof(T)))
                .Select(x => x.Value).Cast<T>().ToList();
        }

        /// <summary>
        /// Enter room handler, used when user clicks room to enter
        /// </summary>
        public void EnterRoom(IEntity entity, Position entryPosition = null)
        {
            SilentlyEntityRoom(entity, entryPosition);

            if (!(entity is Player player))
                return;

            player.Send(new RoomReadyComposer(room.Data.Model, room.Data.Id));

            if (room.Data.Wallpaper > 0)
                player.Send(new RoomPropertyComposer("wallpaper", Convert.ToString(room.Data.Wallpaper)));

            if (room.Data.Floor > 0)
                player.Send(new RoomPropertyComposer("floor", Convert.ToString(room.Data.Floor)));

            player.Send(new RoomPropertyComposer("landscape", Convert.ToString(room.Data.Wallpaper)));
            
            if (!room.Data.IsPublicRoom)
            {
                if (room.HasRights(player.Details.Id, true))
                {
                    player.Send(new YouAreOwnerMessageEvent());
                    player.Send(new YouAreControllerComposer(4));
                }

                if (room.HasRights(player.Details.Id))
                    player.Send(new YouAreControllerComposer(1));
            }
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

            room.Send(new UsersComposer(List.Create<IEntity>(entity)));
            room.Entities.TryAdd(entity.RoomEntity.InstanceId, entity);

            if (entity is Player player)
            {
                // We're in room, yayyy >:)
                player.Messenger.SendStatus();

                room.Data.UsersNow++;
                RoomDao.SetVisitorCount(room.Data.Id, room.Data.UsersNow);
            }
        }

        /// <summary>
        /// Leave room handler, called when user leaves room, clicks another room, re-enters room, and disconnects
        /// </summary>
        public void LeaveRoom(IEntity entity, bool hotelView = false)
        {
            room.Entities.Remove(entity.RoomEntity.InstanceId);
            room.Send(new UserRemoveComposer(entity.RoomEntity.InstanceId));

            entity.RoomEntity.Reset();

            if (entity is Player player)
            {
                room.Data.UsersNow--;
                RoomDao.SetVisitorCount(room.Data.Id, room.Data.UsersNow);

                if (hotelView)
                    player.Send(new CloseConnectionComposer());
                
                player.Messenger.SendStatus();
            }

            room.TryDispose();
        }

        #endregion
    }
}
