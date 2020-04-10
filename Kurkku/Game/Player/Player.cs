﻿using Kurkku.Messages;
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

        private ILog m_Log = LogManager.GetLogger(typeof(Player));
        private PlayerData m_PlayerData;
        private PlayerSettingsData m_Settings;

        #endregion

        #region Interface properties

        /// <summary>
        /// Get room entity
        /// </summary>
        public RoomEntity RoomEntity { get; private set; }

        /// <summary>
        /// Get entity data
        /// </summary>
        public IEntityData EntityData => (IEntityData)m_PlayerData;

        #endregion

        #region Properties

        /// <summary>
        /// Get the connection session
        /// </summary>
        public ConnectionSession Connection { get; private set; }

        /// <summary>
        /// Get the logging
        /// </summary>
        public ILog Log => m_Log;

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

                return DateTime.Now > Subscription.ExpireDate;
            }
        }

        /// <summary>
        /// Get entity data
        /// </summary>
        public PlayerData Details => m_PlayerData;

        /// <summary>
        /// Get player settings
        /// </summary>
        public PlayerSettingsData Settings => m_Settings;

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
            m_Log = LogManager.GetLogger(Assembly.GetExecutingAssembly(), $"Connection {connectionSession.Channel.Id}");
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
            UserDao.Login(out m_PlayerData, ssoTicket);

            if (m_PlayerData == null)
                return false;

            m_Log = LogManager.GetLogger(Assembly.GetExecutingAssembly(), $"Player {m_PlayerData.Name}");
            m_Log.Debug($"Player {m_PlayerData.Name} has logged in");

            UserSettingsDao.CreateOrUpdate(out m_Settings, m_PlayerData.Id);
            PlayerManager.Instance.AddPlayer(this);

            m_PlayerData.PreviousLastOnline = m_PlayerData.LastOnline;
            m_PlayerData.LastOnline = DateTime.Now;
            UserDao.Update(m_PlayerData);

            Subscription = SubscriptionDao.GetSubscription(m_PlayerData.Id);

            Messenger = new Messenger(this);
            Messenger.SendStatus();

            Authenticated = true;

            Send(new AuthenticationOKComposer());
            return true;
        }

        /// <summary>
        /// Send message composer
        /// </summary>
        public void Send(MessageComposer composer)
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

            PlayerManager.Instance.RemovePlayer(this);

            Messenger.SendStatus();

            m_PlayerData.LastOnline = DateTime.Now;
            UserDao.Update(m_PlayerData);
        }

        #endregion
    }
}
