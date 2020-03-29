using DotNetty.Transport.Channels;
using Kurkku.Game.Player;
using Kurkku.Messages;
using Kurkku.Messages.Incoming.Handshake;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Network.Session
{
    public class ConnectionSession
    {
        #region Fields

        private bool m_Disconnected;
        private IChannel m_Channel;
        private Player m_Player;

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

        /// <summary>
        /// Get player instance
        /// </summary>
        public Player Player
        {
            get { return m_Player; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for player.
        /// </summary>
        /// <param name="channel">the channel</param>
        public ConnectionSession(IChannel channel)
        {
            m_Channel = channel;
            m_Player = new Player(this);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Send message composer
        /// </summary>
        public void Send(MessageComposer composer)
        {
            if (!composer.Composed)
            {
                composer.Composed = true;
                composer.Write();
            }

            m_Channel.WriteAndFlushAsync(composer);
        }

        /// <summary>
        /// Kick handler
        /// </summary>
        public virtual void Kick()
        {
            m_Channel.CloseAsync();
        }

        /// <summary>
        /// Disconnection handler
        /// </summary>
        public virtual void Disconnect()
        {
            if (m_Disconnected)
                return;

            m_Disconnected = true;
            m_Player.Disconnect();
        }

        #endregion

    }
}
