using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Game
{
    public class Messenger
    {
        #region Fields

        private Player m_Player;
        private List<MessengerRequestData> m_Requests;
        private List<MessengerFriendData> m_Friends;
        private List<MessengerCategoryData> m_Categories;

        #endregion

        #region Properties

        /// <summary>
        /// Get friend requests
        /// </summary>
        public List<MessengerRequestData> Requests
        {
            get { return m_Requests; }
        }

        /// <summary>
        /// Get friends
        /// </summary>
        public List<MessengerFriendData> Friends
        {
            get { return m_Friends; }
        }

        /// <summary>
        /// Get categories
        /// </summary>
        public List<MessengerCategoryData> Categories
        {
            get { return m_Categories; }
        }

        /// <summary>
        /// Get the player for this messenger instance
        /// </summary>
        public Player Player
        {
            get { return m_Player; }
        }

        #endregion

        #region Constructors

        public Messenger(Player player)
        {
            m_Player = player;
        }

        public void Init()
        {
            m_Friends = MessengerDao.GetFriends(m_Player.Data.Id);
            m_Requests = MessengerDao.GetRequests(m_Player.Data.Id);
            m_Categories = MessengerDao.GetCategories(m_Player.Data.Id);
        }

        /// <summary>
        /// Get if the user id is friends
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <returns>true if successful</returns>
        public bool HasFriend(int userId) => 
            m_Friends.Count(friend => friend.UserId == userId) > 0;

        /// <summary>
        /// Get the user id as friend instance
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <returns>user data instance if is friend</returns>
        public MessengerUserData GetFriend(int userId) =>
            m_Friends.FirstOrDefault(friend => friend.UserId == userId);

        /// <summary>
        /// Get if the user id has requested user
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <returns>true if successful</returns>
        public bool HasRequest(int userId) =>
            m_Requests.Count(requester => requester.UserId == userId) > 0;

        /// <summary>
        /// Get the user id as request instance
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <returns>user data instance if is requester</returns>
        public MessengerUserData GetRequest(int userId) =>
            m_Requests.FirstOrDefault(friend => friend.UserId == userId);

        #endregion
    }
}
