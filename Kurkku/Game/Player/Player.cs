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
        /// Get subscription manager
        /// </summary>
        public Subscription Subscription { get; private set; }

        /// <summary>
        /// Get whether has subscription data
        /// </summary>
        public bool IsSubscribed
        {
            get
            {
                if (Subscription.Data == null)
                    return false;

                return Subscription.Data.ExpireDate > DateTime.Now;
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
        /// Get currency manager for user
        /// </summary>
        public CurrencyManager Currency { get; set; }

        /// <summary>
        /// Get inventory instance for user
        /// </summary>
        public Inventory Inventory { get; set; }

        /// <summary>
        /// Whether the player has logged in or not
        /// </summary>
        public bool Authenticated { get; private set; }

        /// <summary>
        /// The time when player connected
        /// </summary>
        public DateTime AuthenticationTime { get; private set; }

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

            playerData.PreviousLastOnline = playerData.LastOnline;
            playerData.LastOnline = DateTime.Now;

            UserDao.SaveLastOnline(playerData);
            PlayerManager.Instance.AddPlayer(this);

            Subscription = new Subscription(this);
            Subscription.Load();
            Subscription.CountMemberDays();

            Currency = new CurrencyManager(this);
            Currency.Load();

            Inventory = new Inventory(this);
            Inventory.Load();

            Messenger = new Messenger(this);
            Messenger.SendStatus();

            Authenticated = true;
            AuthenticationTime = DateTime.Now;

            Send(new AuthenticationOKComposer());
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
            Subscription.CountMemberDays();

            playerData.LastOnline = DateTime.Now;
            UserDao.SaveLastOnline(playerData);

            long timeInSeconds = (long)(DateTime.Now - AuthenticationTime).TotalSeconds;
            settings.OnlineTime += timeInSeconds;
            UserSettingsDao.Update(settings);
           
        }

        #endregion
    }
}
