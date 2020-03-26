using DotNetty.Buffers;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using log4net;
using Kurkku.Util;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;

namespace Kurkku.Network
{
    class GameServer
    {
        #region Fields

        private static readonly ILog m_Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly GameServer m_GameServer = new GameServer();

        private MultithreadEventLoopGroup m_BossGroup;
        private MultithreadEventLoopGroup m_WorkerGroup;

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
                    .ChildHandler(new GameChannelInitializer())/*new ActionChannelInitializer<IChannel>(channel =>
                        channel.Pipeline.AddLast("gameEncoder", new NetworkEncoder()),
                        channel.Pipeline.AddLast("ClientHandler", new GameNetworkHandler())
                    ))*/
                    .ChildOption(ChannelOption.TcpNodelay, true)
                    .ChildOption(ChannelOption.SoKeepalive, true)
                    .ChildOption(ChannelOption.SoReuseaddr, true)
                    .ChildOption(ChannelOption.SoRcvbuf, 1024)
                    .ChildOption(ChannelOption.RcvbufAllocator, new FixedRecvByteBufAllocator(1024))
                    .ChildOption(ChannelOption.Allocator, UnpooledByteBufferAllocator.Default);

                bootstrap.BindAsync(IPAddress.Parse(ServerConfig.Instance.GetString("server", "ip")), ServerConfig.Instance.GetInt("server", "port"));
                m_Log.Info($"Server is now listening on port: {ServerConfig.Instance.GetInt("server", "port")}!");
            }
            catch (Exception e)
            {
                m_Log.Error($"Failed to setup network listener... {e}");
            }
        }

        #endregion
    }
}
