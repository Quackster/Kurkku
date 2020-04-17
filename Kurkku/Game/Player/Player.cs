using Kurkku.Messages;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Session;
using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using log4net;
using System;
using System.Reflection;

namespace Kurkku.Game
{
    public class Player : IEntity
    {
        #region Fields

        private ILog log = LogManager.GetLogger(typeof(Player));
        private PlayerData playerData;
        private PlayerSettingsData settings;

        #endregion

        #region Interface properties

        /// <summary>
        /// Get room entity
        /// </summary>
        public RoomEntity RoomEntity { get; private set; }

        /// <summary>
        /// Get entity data
        /// </summary>
        public IEntityData EntityData => (IEntityData)playerData;

        #endregion

        #region Properties

        /// <summary>
        /// Get the connection session
        /// </summary>
        public ConnectionSession Connection { get; private set; }

        /// <summary>
        /// Get the logging
        /// </summary>
        public ILog Log => log;

        /// <summary>
        /// Get messenger
        /// </summary>
        public Messenger Messenger { get; private set; }

        /// <summary>
        /// Get subscription data
        /// </summary>
        public SubscriptionData Subscription { get; private set; }

        /// <summary>
        /// Get whether has subscription data
        /// </summary>
        public bool IsSubscribed
        {
            get
            {
                if (Subscription == null)
                    return false;

                return Subscription.ExpireDate > DateTime.Now;
            }
        }

        /// <summary>
        /// Get entity data
        /// </summary>
        public PlayerData Details => playerData;

        /// <summary>
        /// Get player settings
        /// </summary>
        public PlayerSettingsData Settings => settings;

        /// <summary>
        /// Get room player
        /// </summary>
        public RoomPlayer RoomUser => (RoomPlayer)RoomEntity;

        /// <summary>
        /// Whether the player has logged in or not
        /// </summary>
        public bool Authenticated { get; private set; }


        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for player.
        /// </summary>
        /// <param name="channel">the channel</param>
        public Player(ConnectionSession connectionSession)
        {
            Connection = connectionSession;
            RoomEntity = new RoomPlayer(this);
            log = LogManager.GetLogger(Assembly.GetExecutingAssembly(), $"Connection {connectionSession.Channel.Id}");
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Login handler
        /// </summary>
        /// <param name="ssoTicket">the sso ticket</param>
        /// <returns></returns>
        public bool TryLogin(string ssoTicket)
        {
            UserDao.Login(out playerData, ssoTicket);

            if (playerData == null)
                return false;

            log = LogManager.GetLogger(Assembly.GetExecutingAssembly(), $"Player {playerData.Name}");
            log.Debug($"Player {playerData.Name} has logged in");

            UserSettingsDao.CreateOrUpdate(out settings, playerData.Id);
            PlayerManager.Instance.AddPlayer(this);

            playerData.PreviousLastOnline = playerData.LastOnline;
            playerData.LastOnline = DateTime.Now;
            UserDao.Update(playerData);

            Subscription = SubscriptionDao.GetSubscription(playerData.Id);

            Messenger = new Messenger(this);
            Messenger.SendStatus();

            Authenticated = true;

            Send(new AuthenticationOKComposer());
            Send(new ActivityPointNotificationComposer());
            Send(new AvailabilityStatusComposer());
            Send(new UserRightsMessageComposer(IsSubscribed ? 2 : 0, playerData.Rank));

            return true;
        }

        /// <summary>
        /// Send message composer
        /// </summary>
        public void Send(IMessageComposer composer)
        {
            Connection.Send(composer);
        }

        /// <summary>
        /// Disconnection handler
        /// </summary>
        public void Disconnect()
        {
            if (!Authenticated)
                return;

            if (RoomEntity.Room != null)
                RoomEntity.Room.EntityManager.LeaveRoom(this);

            PlayerManager.Instance.RemovePlayer(this);

            Messenger.SendStatus();

            playerData.LastOnline = DateTime.Now;
            UserDao.Update(playerData);
        }

        #endregion
    }
}
