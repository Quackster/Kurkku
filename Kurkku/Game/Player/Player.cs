using System;
using System.Collections.Generic;
using System.Text;
using DotNetty.Transport.Channels;
using log4net;

namespace Kurkku.Game.Player
{
    class Player
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Player));

        #region Fields

        private bool m_Disconnected;
        private IChannel m_Channel;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the player channel.
        /// </summary>
        public IChannel Channel
        {
            get { return m_Channel; }
        }

        /// <summary>
        /// Get the ip address of the player connected.
        /// </summary>
        public string IpAddress
        {
            get { return m_Channel.RemoteAddress.ToString().Split(':')[3].Replace("]", ""); }
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Constructor for player.
        /// </summary>
        /// <param name="channel">the channel</param>
        public Player(IChannel channel)
        {
            m_Channel = channel;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Disconnection handler
        /// </summary>
        internal void Disconnect()
        {

        }

        #endregion
    }
}
