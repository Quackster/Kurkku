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
            PlayerIds.TryAdd(player.Data.Id, player);
            PlayerNames.TryAdd(player.Data.Name.ToLower(), player);
        }

        /// <summary>
        /// Add the player
        /// </summary>
        /// <param name="player">remove the player</param>
        public void RemovePlayer(Player player)
        {
            PlayerIds.Remove(player.Data.Id, out _);
            PlayerNames.Remove(player.Data.Name.ToLower(), out _);
        }

        /// <summary>
        /// Get the player by username
        /// </summary>
        /// <param name="username">the player username</param>
        /// <returns></returns>
        public Player GetPlayerByName(string username)
        {
            if (PlayerNames.ContainsKey(username.ToLower()))
            {
                Player player;
                PlayerNames.TryGetValue(username.ToLower(), out player);
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
            if (PlayerIds.ContainsKey(id))
            {
                Player player;
                PlayerIds.TryGetValue(id, out player);
                return player;
            }

            return null;
        }

        #endregion
    }
}
