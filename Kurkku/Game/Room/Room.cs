﻿using Kurkku.Game.Managers;
using Kurkku.Messages;
using Kurkku.Storage.Database.Data;
using System.Collections.Concurrent;
using System.Linq;

namespace Kurkku.Game
{
    public class Room
    {
        #region Properties

        public RoomData Data { get; }
        public RoomEntityManager EntityManager { get; }
        public RoomModel Model => RoomManager.Instance.RoomModels.FirstOrDefault(x => x.Data.Model == Data.Model);
        public ConcurrentDictionary<int, IEntity> Entities { get; }

        #endregion

        #region Constructors

        public Room(RoomData data)
        {
            Data = data;
            Entities = new ConcurrentDictionary<int, IEntity>();
            EntityManager = new RoomEntityManager(this);
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

        /// <summary>
        /// Get if the user has rights
        /// </summary>
        public bool HasRights(int userId, bool checkOwner = false)
        {
            if (checkOwner)
                if (Data.OwnerId == userId)
                    return true;

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

            RoomManager.Instance.RemoveRoom(Data.Id);
        }

        /// <summary>
        /// Send packet to entire player list in room
        /// </summary>
        public void Send(IMessageComposer composer)
        {
            foreach (var player in EntityManager.GetEntities<Player>())
                player.Send(composer);
        }

        #endregion
    }
}
