using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using Kurkku.Util.Extensions;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Game
{
    public class PlayerManager
    {
        #region Fields

        public static readonly PlayerManager Instance = new PlayerManager();

        #endregion

        #region Properties

        /// <summary>
        /// Get dictionary of players with id's as keys
        /// </summary>
        public ConcurrentDictionary<int, Player> PlayerIds { get; private set; }

        /// <summary>
        /// Get dictionary of players with names as keys
        /// </summary>
        public ConcurrentDictionary<string, Player> PlayerNames { get; private set; }

        /// <summary>
        /// Get the list of online players
        /// </summary>
        public List<Player> Players
        {
            get => PlayerIds.Values.ToList();
        }

        #endregion

        #region Constructors

        public PlayerManager()
        {
            PlayerIds = new ConcurrentDictionary<int, Player>();
            PlayerNames = new ConcurrentDictionary<string, Player>();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Add the player
        /// </summary>
        /// <param name="player">remove the player</param>
        public void AddPlayer(Player player)
        {
            PlayerIds.TryAdd(player.EntityData.Id, player);
            PlayerNames.TryAdd(player.EntityData.Name.ToLower(), player);
        }

        /// <summary>
        /// Add the player
        /// </summary>
        /// <param name="player">remove the player</param>
        public void RemovePlayer(Player player)
        {
            PlayerIds.Remove(player.EntityData.Id);
            PlayerNames.Remove(player.EntityData.Name.ToLower());
        }

        /// <summary>
        /// Get the player by username
        /// </summary>
        /// <param name="username">the player username</param>
        /// <returns></returns>
        public Player GetPlayerByName(string username)
        {
            return PlayerNames.TryGetValue(username.ToLower(), out var player) ? player : null;
        }

        /// <summary>
        /// Get the player by id
        /// </summary>
        /// <param name="id">the player username</param>
        /// <returns></returns>
        public Player GetPlayerById(int id)
        {
            return PlayerIds.TryGetValue(id, out var player) ? player : null;
        }

        /// <summary>
        /// Get data by id
        /// </summary>
        public PlayerData GetDataById(int userId)
        {
            var player = GetPlayerById(userId);

            if (player != null)
                return player.Details;

            return UserDao.GetById(userId);
        }

        /// <summary>
        /// Get data by name
        /// </summary>
        public PlayerData GetDataByName(int userId)
        {
            var player = GetPlayerById(userId);

            if (player != null)
                return player.Details;

            return UserDao.GetById(userId);
        }

        /// <summary>
        /// Get data by name
        /// </summary>
        public string GetName(int userId)
        {
            var player = GetPlayerById(userId);

            if (player != null)
                return player.Details.Name;

            return UserDao.GetNameById(userId);
        }

        #endregion
    }
}
