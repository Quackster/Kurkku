using Kurkku.Messages.Outoing.Messenger;
using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using Kurkku.Util.Extensions;
using System.Collections.Concurrent;
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
        /// Get concurrent messenger update queue
        /// </summary>
        public ConcurrentQueue<MessengerUpdate> Queue { get; private set; }

        /// <summary>
        /// Get the player for this messenger instance
        /// </summary>
        public Player Player { get; set; }

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

            Friends = MessengerDao.GetFriends(Player.Details.Id).Select(data => Wrap(data)).ToList();
            Requests = MessengerDao.GetRequests(Player.Details.Id).Select(data => Wrap(data)).ToList();
            Categories = MessengerDao.GetCategories(Player.Details.Id);

            Queue = new ConcurrentQueue<MessengerUpdate>();
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
            {
                Messenger messenger = friend.Player.Messenger;
                List<MessengerUpdate> messengerUpdates = messenger.Queue.Dequeue();

                if (messengerUpdates.Count > 0)
                    friend.Player.Send(new UpdateMessengerComposer(messenger.Categories, messengerUpdates));
            }
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
