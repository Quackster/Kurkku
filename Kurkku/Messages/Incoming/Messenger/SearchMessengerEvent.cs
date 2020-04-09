using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using Kurkku.Util.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Messages.Incoming
{
    class SearchMessengerEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            List<PlayerData> resultSet = MessengerDao.SearchMessenger(request.ReadString().FilterInput(), player.Details.Id);

            var friends = resultSet.Where(data => player.Messenger.HasFriend(data.Id))
                .Select(data => new MessengerUser {
                PlayerData = data
            }).ToList();

            var users = resultSet.Where(data => !player.Messenger.HasFriend(data.Id))
                .Select(data => new MessengerUser {
                PlayerData = data
            }).ToList();

            player.Send(new SearchMessengerComposer(friends, users));
        }
    }
}
