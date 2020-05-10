using Kurkku.Game;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    class VersionCheckMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            if (player.Authenticated)
            {
                player.Connection.Close();
                return;
            }

            var clientVersion = request.ReadString();

            if (clientVersion == Kurkku.ClientVersion)
                player.Log.Debug($"Received request: {clientVersion}");
            else
                player.Connection.Disconnect();
        }
    }
}
