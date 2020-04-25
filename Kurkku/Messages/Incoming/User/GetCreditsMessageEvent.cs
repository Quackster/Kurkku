using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    class GetCreditsMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            player.Currency.UpdateCredits();
            player.Currency.UpdateCurrencies();
        }
    }
}
