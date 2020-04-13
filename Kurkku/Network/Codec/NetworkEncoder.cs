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
    internal class NetworkEncoder : MessageToMessageEncoder<IMessageComposer>
    {
        protected override void Encode(IChannelHandlerContext ctx, IMessageComposer composer, List<object> output)
        {
            ConnectionSession connection = ctx.Channel.GetAttribute(GameNetworkHandler.CONNECTION_KEY).Get();

            try
            {
                short? header = MessageHandler.Instance.GetComposerId(composer);

                if (header == null)
                    throw new NullReferenceException($"No header found for composer class {composer.GetType().Name}");

                var buffer = Unpooled.Buffer();
                var response = new Response(header.Value, buffer);

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

                if (connection != null)
                    connection.Player.Log.Debug($"SENT {composer.GetType().Name}: " + response.Header + " / " + response.MessageBody);

                output.Add(buffer);
            }
            catch (Exception ex)
            {
                connection.Player.Log.Error("Error occurred: ", ex);
            }
        }
    }
}