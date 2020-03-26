using System;
using System.Collections.Generic;
using System.Text;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using log4net;
using Kurkku.Network.Streams;

namespace Kurkku.Network.Codec
{
    internal class NetworkEncoder : MessageToMessageEncoder<object>
    {
        protected override void Encode(IChannelHandlerContext context, object msg, List<object> output)
        {
            if (msg is Response response)
                context.WriteAndFlushAsync(Unpooled.CopiedBuffer(Encoding.GetEncoding(0).GetBytes(response.GetMessage())));
        }
    }
}