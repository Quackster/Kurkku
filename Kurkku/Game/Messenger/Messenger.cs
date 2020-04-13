using Kurkku.Messages.Outgoing;
using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using Kurkku.Util.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Game
{
    public class Messenger
    {
        #region Fields

        private SubscriptionData m_Subscription;

        #endregion

        #region Properties

        /// <summary>
        /// Get friend requests
        /// </summary>
        public List<MessengerUser> Requests { get; private set; }

        /// <summary>
        /// Get friends
        /// </summary>
        public List<MessengerUser> Friends { get; private set; }

        /// <summary>
        /// Get categories
        /// </summary>
        public List<MessengerCategoryData> Categories { get; private set; }

        /// <summary>
        /// Get concurrent messenger update queue
        /// </summary>
        public ConcurrentQueue<MessengerUpdate> Queue { get; private set; }

        /// <summary>
        /// Get the maximum friends allowed
        /// </summary>
        public int MaxFriendsAllowed
        {
            get
            {
                if (m_Subscription == null)
                    return ValueManager.Instance.GetInt("max.friends.normal");

                /*return m_Subscription.Type == SubscriptionType.HC ?
                    ValueManager.Instance.GetInt("max.friends.hc") :
                    ValueManager.Instance.GetInt("max.friends.vip");*/

                return ValueManager.Instance.GetInt("max.friends.hc");
            }
        }

        /// <summary>
        /// Get the player for this messenger instance
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// Get whether friend requests are enabled
        /// </summary>
        public bool FriendRequestsEnabled { get; set; }

        /// <summary>
        /// Get the player as messenger user
        /// </summary>
        public MessengerUser MessengerUser => 
            new MessengerUser { PlayerData = Player.Details };

        #endregion

        #region Constructors

        public Messenger(Player player)
        {
            Player = player;
            m_Subscription = player.Subscription;
            FriendRequestsEnabled = player.Settings.FriendRequestsEnabled;
            LoadMessengerData(player.Details.Id);
        }

        public Messenger(int userId)
        {
            m_Subscription = SubscriptionDao.GetSubscription(userId);
            FriendRequestsEnabled = MessengerDao.GetAcceptsFriendRequests(userId);
            LoadMessengerData(userId);
        }

        #endregion

        #region Static methods

        public static Messenger GetMessengerData(int userId)
        {
            var player = PlayerManager.Instance.GetPlayerById(userId);

            if (player != null)
                return player.Messenger;

            return new Messenger(userId);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load messenger data by given useer id
        /// </summary>
        private void LoadMessengerData(int userId)
        {
            Friends = MessengerDao.GetFriends(userId).Select(data => Wrap(data.FriendData)).ToList();
            Requests = MessengerDao.GetRequests(userId).Select(data => Wrap(data.FriendData)).ToList();
            Categories = MessengerDao.GetCategories(userId);
            Queue = new ConcurrentQueue<MessengerUpdate>();
        }


        /// <summary>
        /// Wrapper around messenger user data
        /// </summary>
        /// <param name="messengerUserData">the data to wrap</param>
        /// <returns>the wrapped class</returns>
        public static MessengerUser Wrap(PlayerData playerData)
        {
            return new MessengerUser
            {
                PlayerData = playerData
            };
        }

        /// <summary>
        /// Queue specific messenger update
        /// </summary>
        /// <param name="updateType">the update type</param>
        /// <param name="messengerUser">the messenger user</param>
        public void QueueUpdate(MessengerUpdateType updateType, MessengerUser messengerUser)
        {
            Queue.Enqueue(new MessengerUpdate
            {
                UpdateType = updateType,
                Friend = messengerUser
            });
        }

        /// <summary>
        /// Send status update to all friends
        /// </summary>
        public void SendStatus()
        {
            var onlineFriends = GetOnlineFriends();

            foreach (var friend in onlineFriends)
                friend.Player.Messenger.QueueUpdate(MessengerUpdateType.UpdateFriend, MessengerUser);

            foreach (var friend in onlineFriends)
                friend.Player.Messenger.ForceUpdate();
        }

        /// <summary>
        /// Forces update to own messenger
        /// </summary>
        public void ForceUpdate()
        {
            List<MessengerUpdate> messengerUpdates = Queue.Dequeue();

            if (messengerUpdates.Count > 0)
                Player.Send(new UpdateMessengerComposer(Categories, messengerUpdates));
        }

        /// <summary>
        /// Remove friend by user id
        /// </summary>
        public void RemoveFriend(int id)
        {
            Friends.RemoveAll(x => x.PlayerData.Id == id);
        }

        /// <summary>
        /// Remove request by user id
        /// </summary>
        public void RemoveRequest(int id)
        {
            Requests.RemoveAll(x => x.PlayerData.Id == id);
        }

        /// <summary>
        /// Get the list of all online friends
        /// </summary>
        public List<MessengerUser> GetOnlineFriends() => 
            Friends.Where(friend => friend.IsOnline).ToList();
             
        public bool HasFriend(int userId) => 
            Friends.Count(friend => friend.PlayerData.Id == userId) > 0;

        public MessengerUser GetFriend(int userId) =>
            Friends.FirstOrDefault(friend => friend.PlayerData.Id == userId);

        public bool HasRequest(int userId) =>
            Requests.Count(requester => requester.PlayerData.Id == userId) > 0;

        public MessengerUser GetRequest(int userId) =>
            Requests.FirstOrDefault(friend => friend.PlayerData.Id == userId);

        #endregion
    }
}
