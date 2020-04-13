using DotNetty.Transport.Channels;
using Kurkku.Network.Codec;

namespace Kurkku.Network
{
    internal class GameChannelInitializer : ChannelInitializer<IChannel>
    {
        protected override void InitChannel(IChannel channel)
        {
            IChannelPipeline pipeline = channel.Pipeline;
            pipeline.AddLast("gameEncoder", new NetworkEncoder());
            pipeline.AddLast("gameDecoder", new NetworkDecoder());
            pipeline.AddLast("clientHandler", new GameNetworkHandler());
        }
    }
}