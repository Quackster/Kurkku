using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Game
{
    public class Messenger
    {
        #region Properties

        /// <summary>
        /// Get friend requests
        /// </summary>
        public List<MessengerUser> Requests { get; set; }

        /// <summary>
        /// Get friends
        /// </summary>
        public List<MessengerUser> Friends { get; set; }

        /// <summary>
        /// Get categories
        /// </summary>
        public List<MessengerCategoryData> Categories { get; set; }

        /// <summary>
        /// Get the player for this messenger instance
        /// </summary>
        public Player Player { get; set; }

        #endregion

        #region Constructors

        public Messenger(Player player)
        {
            Player = player;
        }

        public void Init()
        {
            Friends = MessengerDao.GetFriends(Player.Data.Id).Select(data => Wrap(data)).ToList();
            Requests = MessengerDao.GetRequests(Player.Data.Id).Select(data => Wrap(data)).ToList();
            Categories = MessengerDao.GetCategories(Player.Data.Id);
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
            Friends.Count(friend => friend.PlayerData.Id == userId) > 0;

        /// <summary>
        /// Get the user id as friend instance
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <returns>user data instance if is friend</returns>
        public MessengerUser GetFriend(int userId) =>
            Friends.FirstOrDefault(friend => friend.PlayerData.Id == userId);

        /// <summary>
        /// Get if the user id has requested user
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <returns>true if successful</returns>
        public bool HasRequest(int userId) =>
            Requests.Count(requester => requester.PlayerData.Id == userId) > 0;

        /// <summary>
        /// Get the user id as request instance
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <returns>user data instance if is requester</returns>
        public MessengerUser GetRequest(int userId) =>
            Requests.FirstOrDefault(friend => friend.PlayerData.Id == userId);

        #endregion
    }
}
