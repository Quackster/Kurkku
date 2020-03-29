using DotNetty.Common.Utilities;
using DotNetty.Transport.Channels;
using Kurkku.Messages;
using Kurkku.Network.Session;
using Kurkku.Network.Streams;
using log4net;
using System;

namespace Kurkku.Network
{
    internal class GameNetworkHandler : ChannelHandlerAdapter
    {
        #region Fields

        public static AttributeKey<ConnectionSession> CONNECTION_KEY = AttributeKey<ConnectionSession>.NewInstance("CONNECTION_KEY");
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Public methods

        /// <summary>
        /// Handle client connections.
        /// </summary>
        /// <param name="ctx">the channel context</param>
        public override void ChannelActive(IChannelHandlerContext ctx)
        {
            base.ChannelActive(ctx);

            var connection = new ConnectionSession(ctx.Channel);

            if (connection != null)
            {
                log.Debug($"Client connected to server: {connection.IpAddress}");
                ctx.Channel.GetAttribute(CONNECTION_KEY).SetIfAbsent(connection);
            }
        }

        /// <summary>
        /// Handle client disconnects.
        /// </summary>
        /// <param name="ctx">the channel context</param>
        public override void ChannelInactive(IChannelHandlerContext ctx)
        {
            base.ChannelInactive(ctx);
            ConnectionSession connection = ctx.Channel.GetAttribute(CONNECTION_KEY).Get();

            if (connection == null)
                return;

            connection.Disconnect();

            log.Debug($"Client disconnected from server: {connection.IpAddress}");
        }

        /// <summary>
        /// Handle incoming channel messages from the decoder
        /// </summary>
        /// <param name="ctx">the channel context</param>
        /// <param name="msg">the incoming message</param>
        public override void ChannelRead(IChannelHandlerContext ctx, object msg)
        {
            ConnectionSession connectionSession = ctx.Channel.GetAttribute(CONNECTION_KEY).Get();

            if (connectionSession == null)
                return;

            if (msg is Request)
            {
                Request request = (Request)msg;
                MessageHandler.Instance.HandleMesage(connectionSession.Player, request);
            }

            base.ChannelRead(ctx, msg);
        }

        /// <summary>
        /// Handle channel read complete.
        /// </summary>
        /// <param name="context">the channel context</param>
        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        /// <summary>
        /// Handle exceptions thrown by the network api.
        /// </summary>
        /// <param name="context">the channel context</param>
        /// <param name="exception">the exception</param>
        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception) =>
            log.Error(exception.ToString());

        #endregion
    }
}