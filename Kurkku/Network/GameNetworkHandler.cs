using DotNetty.Buffers;
using DotNetty.Common.Utilities;
using DotNetty.Transport.Channels;
using Kurkku.Game.Player;
using log4net;
using System;
using System.Text;

namespace Kurkku.Network
{
    internal class GameNetworkHandler : ChannelHandlerAdapter
    {
        private static AttributeKey<Player> PLAYER_KEY = AttributeKey<Player>.NewInstance("PLAYER_KEY");
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Handle client connections.
        /// </summary>
        /// <param name="ctx">the channel context</param>
        public override void ChannelActive(IChannelHandlerContext ctx)
        {
            base.ChannelActive(ctx);
            var player = new Player(ctx.Channel);

            if (player != null)
            {
                log.Debug($"Client connected to server: {player.IpAddress}");

                ctx.Channel.GetAttribute(PLAYER_KEY).SetIfAbsent(player);
                //ctx.Channel.WriteAndFlushAsync(new Response("HELLO"));
            }
        }

        /// <summary>
        /// Handle client disconnects.
        /// </summary>
        /// <param name="ctx">the channel context</param>
        public override void ChannelInactive(IChannelHandlerContext ctx)
        {
            base.ChannelInactive(ctx);
            Player player = ctx.Channel.GetAttribute(PLAYER_KEY).Get();

            if (player == null)
                return;

            player.Disconnect();

            log.Debug($"Client disconnected from server: {player.IpAddress}");
        }

        /// <summary>
        /// Handle incoming channel messages from the decoder
        /// </summary>
        /// <param name="ctx">the channel context</param>
        /// <param name="msg">the incoming message</param>
        public override void ChannelRead(IChannelHandlerContext ctx, object msg)
        {
            Player player = ctx.Channel.GetAttribute(PLAYER_KEY).Get();

            if (player == null)
                return;

            /*if (msg is Request)
            {
                Request request = (Request)msg;
                MessageHandler.Instance.ProcessRequest(player, request);
            }*/

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
    }
}