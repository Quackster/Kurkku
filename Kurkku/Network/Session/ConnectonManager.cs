using DotNetty.Transport.Channels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Network.Session
{
    public class ConnectionManager
    {
        #region Field

        private static readonly ConnectionManager m_ConnectionManager = new ConnectionManager();
        private ConcurrentDictionary<IChannel, ConnectionSession> m_Connections;

        #endregion

        #region Properties

        /// <summary>
        /// Get the singleton instance
        /// </summary>
        public ConnectionManager Instance
        {
            get { return m_ConnectionManager; }
        }

        #endregion

        #region Constructors

        public ConnectionManager()
        {
            m_Connections = new ConcurrentDictionary<IChannel, ConnectionSession>();
        }

        #endregion

        #region Public methods

        public void addConnection(IChannel channel)
        {
            if (m_Connections.ContainsKey(channel))
                return;

            m_Connections.TryAdd(channel, new ConnectionSession(channel));
        }

        #endregion
    }
}
