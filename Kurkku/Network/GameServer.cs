using DotNetty.Buffers;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using log4net;
using System;
using System.Net;
using System.Reflection;

namespace Kurkku.Network
{
    public class GameServer
    {
        #region Fields

        private static readonly ILog m_Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly GameServer m_GameServer = new GameServer();

        private MultithreadEventLoopGroup m_BossGroup;
        private MultithreadEventLoopGroup m_WorkerGroup;

        private string m_Ip;
        private int m_Port;

        #endregion

        #region Properties

        /// <summary>
        /// Invoke the singleton instance
        /// </summary>
        public static GameServer Instance
        {
            get
            {
                return m_GameServer;
            }
        }

        /// <summary>
        /// Get the logger
        /// </summary>
        public static ILog Logger
        {
            get
            {
                return m_Log;
            }
        }

        /// <summary>
        /// Get the server IP address
        /// </summary>
        public string IpAddress
        {
            get { return m_Ip; }
        }

        /// <summary>
        /// Port
        /// </summary>
        public int Port
        {
            get { return m_Port; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// GameServer constructor
        /// </summary>
        public GameServer()
        {
            this.m_BossGroup = new MultithreadEventLoopGroup(1);
            this.m_WorkerGroup = new MultithreadEventLoopGroup(10);
        }

        #endregion

        #region Public methods

        public void CreateServer(string ip, int port)
        {
            this.m_Ip = ip;
            this.m_Port = port;
        }

        /// <summary>
        /// Initialise the game server by given pot
        /// </summary>
        /// <param name="port">the game port</param>
        public void InitialiseServer()
        {
            try
            {
                ServerBootstrap bootstrap = new ServerBootstrap()
                    .Group(m_BossGroup, m_WorkerGroup)
                    .Channel<TcpServerSocketChannel>()
                    .ChildHandler(new GameChannelInitializer())
                    .ChildOption(ChannelOption.TcpNodelay, true)
                    .ChildOption(ChannelOption.SoKeepalive, true)
                    .ChildOption(ChannelOption.SoReuseaddr, true)
                    .ChildOption(ChannelOption.SoRcvbuf, 1024)
                    .ChildOption(ChannelOption.RcvbufAllocator, new FixedRecvByteBufAllocator(1024))
                    .ChildOption(ChannelOption.Allocator, UnpooledByteBufferAllocator.Default);

                bootstrap.BindAsync(IPAddress.Parse(m_Ip), m_Port);
            }
            catch (Exception e)
            {
                m_Log.Error($"Failed to setup network listener... {e}");
            }
        }

        #endregion
    }
}
