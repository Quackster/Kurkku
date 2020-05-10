using System;
using DotNetty.Transport.Channels;
using Kurkku.Game;
using Kurkku.Messages;

namespace Kurkku.Network.Session
{
    public class ConnectionSession
    {
        #region Fields

        private bool m_Disconnected;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the player channel.
        /// </summary>
        public IChannel Channel { get; private set; }

        /// <summary>
        /// Get the ip address of the player connected.
        /// </summary>
        public string IpAddress => Channel.RemoteAddress.ToString().Split(':')[3].Replace("]", "");

        /// <summary>
        /// Get player instance
        /// </summary>
        public Player Player { get; private set; }
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for player.
        /// </summary>
        /// <param name="channel">the channel</param>
        public ConnectionSession(IChannel channel)
        {
            Channel = channel;
            Player = new Player(this);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Send message composer
        /// </summary>
        public void Send(IMessageComposer composer)
        {
            if (!composer.Composed)
            {
                composer.Composed = true;
                composer.Write();
            }

            try
            {
                Channel.WriteAndFlushAsync(composer);
            }
            catch { }
        }

        /// <summary>
        /// Kick handler
        /// </summary>
        public void Close()
        {
            Channel.CloseAsync();
        }

        /// <summary>
        /// Disconnection handler
        /// </summary>
        public virtual void Disconnect()
        {
            if (m_Disconnected)
                return;

            m_Disconnected = true;
            Player.Disconnect();
        }

        #endregion

    }
}
