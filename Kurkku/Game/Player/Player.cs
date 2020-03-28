using Kurkku.Network.Session;
using log4net;
using System;
using System.Reflection;

namespace Kurkku.Game.Player
{
    public class Player
    {
        #region Fields

        private ILog m_Log = LogManager.GetLogger(typeof(Player));
        private ConnectionSession m_ConnectionSession;

        #endregion

        #region Properties

        /// <summary>
        /// Get the connection session
        /// </summary>
        public ConnectionSession ConnectionSession
        {
            get { return m_ConnectionSession; }
        }

        public ILog Log
        {
            get { return m_Log; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for player.
        /// </summary>
        /// <param name="channel">the channel</param>
        public Player(ConnectionSession connectionSession)
        {
            this.m_ConnectionSession = connectionSession;
            this.m_Log = LogManager.GetLogger(Assembly.GetExecutingAssembly(), $"Connection {connectionSession.Channel.Id}");
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Disconnection handler
        /// </summary>
        public void Disconnect()
        {

        }

        #endregion
    }
}
