using Kurkku.Game.Managers;
using Kurkku.Messages;
using Kurkku.Messages.Outgoing;
using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Game
{
    public class Room
    {
        #region Properties

        public RoomData Data { get; }
        public RoomEntityManager EntityManager { get; }
        public RoomTaskManager TaskManager { get; }
        public RoomItemManager ItemManager { get; }
        public RoomMapping Mapping { get; set; }
        public RoomFurniture FurnitureManager { get; set; }
        public RoomModel Model => RoomManager.Instance.RoomModels.FirstOrDefault(x => x.Data.Model == Data.Model);
        public ConcurrentDictionary<int, IEntity> Entities { get; }
        public bool IsActive { get; set; }

        #endregion

        #region Constructors

        public Room(RoomData data)
        {
            Data = data;
            Entities = new ConcurrentDictionary<int, IEntity>();
            EntityManager = new RoomEntityManager(this);
            Mapping = new RoomMapping(this);
            TaskManager = new RoomTaskManager(this);
            ItemManager = new RoomItemManager(this);
            FurnitureManager = new RoomFurniture(this);
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Wrap the retrieved database data with a room instance >:)
        /// </summary>
        public static Room Wrap(RoomData roomData)
        {
            return new Room(roomData);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Get if the user has rights
        /// </summary>
        public bool HasRights(int userId, bool checkOwner = true)
        {
            if (checkOwner)
                if (Data.OwnerId == userId)
                    return true;

            var player = PlayerManager.Instance.GetPlayerById(userId);

            if (player != null)
            {
                if (player.UserGroup.HasPermission("room.rights"))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Get if the user is owner
        /// </summary>
        public bool IsOwner(int userId)
        {
            if (Data.OwnerId == userId)
                return true;

            var player = PlayerManager.Instance.GetPlayerById(userId);

            if (player != null)
            {
                if (player.UserGroup.HasPermission("room.owner"))
                    return true;
            }

            return false;
        }


        /// <summary>
        /// Try and dispose, only if it has 0 players active.
        /// </summary>
        public void TryDispose()
        {
            var playerList = EntityManager.GetEntities<Player>();

            if (playerList.Any())
                return;

            TaskManager.StopTasks();
            RoomManager.Instance.RemoveRoom(Data.Id);

            IsActive = false;
        }

        /// <summary>
        /// Send packet to entire player list in room
        /// </summary>
        public void Send(IMessageComposer composer, List<Player> specificUsers = null)
        {
            if (specificUsers == null)
                specificUsers = EntityManager.GetEntities<Player>();

            foreach (var player in specificUsers)
                player.Send(composer);
        }

        /// <summary>
        /// Forward entity to room
        /// </summary>
        /// <param name="entity"></param>
        public void Forward(IEntity entity)
        {
            if (!(entity is Player))
            {
                return;
            }

            var player = (Player)entity;
            
            player.Send(new RoomForwardComposer(Data.Id, Data.IsPublicRoom));
        }

        #endregion
    }
}
