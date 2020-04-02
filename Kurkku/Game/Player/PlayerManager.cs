using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Game
{
    public class PlayerManager
    {
        #region Fields

        private ConcurrentDictionary<int, Player> _PlayerIds;
        private ConcurrentDictionary<string, Player> _PlayerNames;

        public static PlayerManager Instance = new PlayerManager();

        #endregion

        #region Properties

        /// <summary>
        /// Get dictionary of players with id's as keys
        /// </summary>
        public ConcurrentDictionary<int, Player> PlayerIds
        {
            get { return _PlayerIds; }
        }

        /// <summary>
        /// Get dictionary of players with names as keys
        /// </summary>
        public ConcurrentDictionary<string, Player> PlayerNames
        {
            get { return _PlayerNames; }
        }

        public List<Player> Players
        {
            get { return _PlayerIds.Values.ToList(); }
        }

        #endregion

        #region Constructors

        public PlayerManager()
        {
            _PlayerIds = new ConcurrentDictionary<int, Player>();
            _PlayerNames = new ConcurrentDictionary<string, Player>();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Add the player
        /// </summary>
        /// <param name="player">remove the player</param>
        public void AddPlayer(Player player)
        {
            _PlayerIds.TryAdd(player.Data.Id, player);
            _PlayerNames.TryAdd(player.Data.Name.ToLower(), player);
        }

        /// <summary>
        /// Add the player
        /// </summary>
        /// <param name="player">remove the player</param>
        public void RemovePlayer(Player player)
        {
            _PlayerIds.Remove(player.Data.Id, out _);
            _PlayerNames.Remove(player.Data.Name.ToLower(), out _);
        }

        /// <summary>
        /// Get the player by username
        /// </summary>
        /// <param name="username">the player username</param>
        /// <returns></returns>
        public Player GetPlayerByName(string username)
        {
            if (_PlayerNames.ContainsKey(username.ToLower()))
            {
                Player player;
                _PlayerNames.TryGetValue(username.ToLower(), out player);
                return player;
            }

            return null;
        }

        /// <summary>
        /// Get the player by id
        /// </summary>
        /// <param name="id">the player username</param>
        /// <returns></returns>
        public Player GetPlayerById(int id)
        {
            if (_PlayerIds.ContainsKey(id))
            {
                Player player;
                _PlayerIds.TryGetValue(id, out player);
                return player;
            }

            return null;
        }

        #endregion
    }
}
