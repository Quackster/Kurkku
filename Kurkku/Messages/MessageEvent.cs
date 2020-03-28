using Kurkku.Game.Player;
using Kurkku.Network.Streams;

namespace Kurkku.Messages
{
    public interface MessageComposer
    {
        void Write();
        object[] GetMessageArray();
    }
}