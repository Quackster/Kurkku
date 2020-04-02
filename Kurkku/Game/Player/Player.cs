using Kurkku.Messages.Outgoing.Handshake;
using Kurkku.Network.Session;
using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using log4net;
using System.Reflection;

namespace Kurkku.Game
{
    public class Player : IEntity<PlayerData>
    {
        #region Fields

        private ILog m_Log = LogManager.GetLogger(typeof(Player));
        private ConnectionSession m_Connection;
        private PlayerData m_PlayerData;
        private Messenger m_Messenger;

        private bool m_Authenticated;

        #endregion

        #region Properties

        /// <summary>
        /// Get the connection session
        /// </summary>
        public ConnectionSession Connection
        {
            get { return m_Connection; }
        }

        /// <summary>
        /// Get the logging
        /// </summary>
        public ILog Log
        {
            get { return m_Log; }
        }

        /// <summary>
        /// Get entity data
        /// </summary>
        public PlayerData Data
        {
            get { return m_PlayerData; }
        }

        /// <summary>
        /// Get messenger
        /// </summary>
        public Messenger Messenger
        {
            get { return m_Messenger; }
        }


        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for player.
        /// </summary>
        /// <param name="channel">the channel</param>
        public Player(ConnectionSession connectionSession)
        {
            m_Connection = connectionSession;
            m_Messenger = new Messenger(this);
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

            m_Messenger.Init();

            m_Log = LogManager.GetLogger(Assembly.GetExecutingAssembly(), $"Player {m_PlayerData.Name}");
            m_Connection.Send(new AuthenticationOKComposer());

            PlayerManager.Instance.AddPlayer(this);
            m_Authenticated = true;
            return true;
        }

        /// <summary>
        /// Disconnection handler
        /// </summary>
        public void Disconnect()
        {
            if (m_Authenticated)
                PlayerManager.Instance.RemovePlayer(this);
        }

        #endregion
    }
}
