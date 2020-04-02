using System;
using System.Collections.Generic;
using System.Text;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Kurkku.Messages;
using Kurkku.Network.Session;
using Kurkku.Network.Streams;

namespace Kurkku.Network.Codec
{
    internal class NetworkEncoder : MessageToMessageEncoder<MessageComposer>
    {
        protected override void Encode(IChannelHandlerContext ctx, MessageComposer composer, List<object> output)
        {
            var buffer = Unpooled.Buffer();
            
            var response = new Response(composer.Header, buffer);

            foreach (var objectData in composer.Data)
            {
                if (objectData is string)
                    response.writeString((string)objectData);

                if (objectData is int || objectData is uint)
                    response.writeInt((int)objectData);

                if (objectData is bool)
                    response.writeBool((bool)objectData);

                if (objectData is short)
                    response.writeShort((short)objectData);
            }

            buffer.SetInt(0, buffer.WriterIndex - 4);

            ConnectionSession connection = ctx.Channel.GetAttribute(GameNetworkHandler.CONNECTION_KEY).Get();

            if (connection != null)
                connection.Player.Log.Debug("Sending: " + response.Header + " / " + response.MessageBody);
            
            output.Add(buffer);
        }
    }
}