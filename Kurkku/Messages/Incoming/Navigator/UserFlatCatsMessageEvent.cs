using Kurkku.Game;
using Kurkku.Network.Streams;
using Kurkku.Messages.Outgoing;

namespace Kurkku.Messages.Incoming
{
    public class UserFlatCatsMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            player.Send(new UserFlatCatsComposer(NavigatorManager.Instance.GetCategories(player.Details.Rank)));
        }
    }
}
