using System;
using System.Collections.Generic;
using System.Text;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Kurkku.Messages;
using Kurkku.Network.Streams;

namespace Kurkku.Network.Codec
{
    internal class NetworkEncoder : MessageToMessageEncoder<MessageComposer>
    {
        protected override void Encode(IChannelHandlerContext context, MessageComposer buffer, List<object> output)
        {
            output.Add(buffer);
        }
    }
}