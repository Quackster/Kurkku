﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using Kurkku.Util.Extensions;

namespace Kurkku.Game
{
    public class RoomManager : ILoadable
    {
        #region Fields

        public static readonly RoomManager Instance = new RoomManager();

        #endregion

        #region Properties

        public ConcurrentDictionary<int, Room> Rooms { get; private set; }
        public List<RoomModel> RoomModels { get; private set; }

        #endregion

        #region Constructors

        public RoomManager()
        {
            Rooms = new ConcurrentDictionary<int, Room>();
        }

        public void Load()
        {
            RoomModels = RoomDao.GetModels().Select(x => new RoomModel(x)).ToList();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Get if the room is loaded
        /// </summary>
        public bool HasRoom(int roomId)
        {
            return Rooms.TryGetValue(roomId, out _);
        }

        /// <summary>
        /// Remove room from list
        /// </summary>
        public void RemoveRoom(int roomId)
        {
            Rooms.Remove(roomId);
        }

        /// <summary>
        /// Add room to the map of loaded rooms
        /// </summary>
        public void AddRoom(Room room)
        {
            if (room == null)
                return;

            if (Rooms.ContainsKey(room.Data.Id))
                return;

            Rooms.TryAdd(room.Data.Id, room);
        }

        /// <summary>
        /// Replace the room data retrieved from the database with actual room instances
        /// </summary>
        public List<Room> ReplaceQueryRooms(List<RoomData> roomsFromDatabase)
        {
            List<Room> rooms = new List<Room>();

            foreach (var roomData in roomsFromDatabase)
            {
                if (Rooms.TryGetValue(roomData.Id, out var room))
                    rooms.Add(room);
                else
                    rooms.Add(Room.Wrap(roomData));

            }

            return rooms;
        }
 
        #endregion
    }
}