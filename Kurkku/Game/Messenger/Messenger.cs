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
        private List<MessengerUser> m_Requests;
        private List<MessengerUser> m_Friends;
        private List<MessengerCategoryData> m_Categories;

        #endregion

        #region Properties

        /// <summary>
        /// Get friend requests
        /// </summary>
        public List<MessengerUser> Requests
        {
            get { return m_Requests; }
        }

        /// <summary>
        /// Get friends
        /// </summary>
        public List<MessengerUser> Friends
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
            m_Friends = MessengerDao.GetFriends(m_Player.Data.Id).Select(data => Wrap(data)).ToList();
            m_Requests = MessengerDao.GetRequests(m_Player.Data.Id).Select(data => Wrap(data)).ToList();
            m_Categories = MessengerDao.GetCategories(m_Player.Data.Id);
        }

        /// <summary>
        /// Wrapper around messenger user data
        /// </summary>
        /// <param name="messengerUserData">the data to wrap</param>
        /// <returns>the wrapped class</returns>
        public MessengerUser Wrap(MessengerUserData messengerUserData)
        {
            var messengerUser = new MessengerUser
            {
                PlayerData = messengerUserData.FriendData
            };

            return messengerUser;
        }

        /// <summary>
        /// Get if the user id is friends
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <returns>true if successful</returns>
        public bool HasFriend(int userId) => 
            m_Friends.Count(friend => friend.PlayerData.Id == userId) > 0;

        /// <summary>
        /// Get the user id as friend instance
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <returns>user data instance if is friend</returns>
        public MessengerUser GetFriend(int userId) =>
            m_Friends.FirstOrDefault(friend => friend.PlayerData.Id == userId);

        /// <summary>
        /// Get if the user id has requested user
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <returns>true if successful</returns>
        public bool HasRequest(int userId) =>
            m_Requests.Count(requester => requester.PlayerData.Id == userId) > 0;

        /// <summary>
        /// Get the user id as request instance
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <returns>user data instance if is requester</returns>
        public MessengerUser GetRequest(int userId) =>
            m_Requests.FirstOrDefault(friend => friend.PlayerData.Id == userId);

        #endregion
    }
}
