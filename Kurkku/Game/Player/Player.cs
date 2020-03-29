using Kurkku.Game.Entity;
using Kurkku.Messages.Outgoing.Handshake;
using Kurkku.Messages.Outgoing.User;
using Kurkku.Network.Session;
using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using Kurkku.Storage.Database.Data.Entity;
using log4net;
using System;
using System.Reflection;

namespace Kurkku.Game.Player
{
    public class Player : IEntity<PlayerData>
    {
        #region Fields

        private ILog m_Log = LogManager.GetLogger(typeof(Player));
        private ConnectionSession m_Connection;
        private PlayerData m_PlayerData;

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

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for player.
        /// </summary>
        /// <param name="channel">the channel</param>
        public Player(ConnectionSession connectionSession)
        {
            m_Connection = connectionSession;
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

            m_Log = LogManager.GetLogger(Assembly.GetExecutingAssembly(), $"Player Alex");
            m_Connection.Send(new AuthenticationOKComposer());
            //m_Connection.Send(new LandingViewComposer("2012-12-25 11:00,xmasday;2012-12-26 11:00,xmasmainc;2012-12-28 14:00,ny2013maina;2013-01-04 14:00,ny2013mainb;2013-01-11 14:00,,", "2012-12-21 14:00,xmasmainc"));
            //m_Connection.Send(new LandingViewComposer("2012-11-09 15:00,hstarsbots;2012-11-16 18:00,diarare;2012-11-19 12:00,xmasghost1;2012-11-22 20:00,xmasghost2;2012-11-22 20:45,xmasghost1;2012-11-25 21:00,xmasghost2;2012-11-25 21:45,xmasghost1;2012-11-28 22:00,xmasghost2;2012-11-28 22:45,xmasghost1;2012-11-30 14:00,", "xmasghost1"));
            //m_Connection.Send(new LandingViewComposer("2012-11-23 18:00,hstarssubmit2;2012-11-26 11:00,;2012-11-26 14:00,hstarsvote2;2012-11-28 11:00,", "hstarsvote2"));
            //m_Connection.Send(new LandingViewComposer("2012-11-09 18:00,hspeedway;2012-11-15 15:00,hstarsdiamonds;2012-11-30 12:00,", "hstarsdiamonds"));

            return true;
        }

        /// <summary>
        /// Disconnection handler
        /// </summary>
        public void Disconnect()
        {

        }

        #endregion
    }
}
