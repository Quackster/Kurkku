using Kurkku.Game.Managers;
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
        public RoomModel Model => RoomManager.Instance.RoomModels.FirstOrDefault(x => x.Data.Model == Data.Model);
        public ConcurrentBag<IEntity> Entities { get; }

        #endregion

        #region Constructors

        public Room(RoomData data)
        {
            Data = data;
            Entities = new ConcurrentBag<IEntity>();
            EntityManager = new RoomEntityManager(this);
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Wrap the retrieved database data with a room instance >:)
        /// </summary>
        internal static Room Wrap(RoomData roomData)
        {
            return new Room(roomData);
        }

        #endregion
    }
}
