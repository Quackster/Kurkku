
using FluentNHibernate.Mapping;

namespace Kurkku.Storage.Database.Data
{
    public class MessengerFriendMapping : ClassMap<MessengerFriendData>
    {
        public MessengerFriendMapping()
        {
            Table("messenger_friend");

            CompositeId()
                .KeyProperty(x => x.UserId, "user_id")
                .KeyProperty(x => x.FriendId, "friend_id");

            Map(x => x.UserId, "user_id");
            Map(x => x.FriendId, "friend_id");

            References(x => x.FriendData, "friend_id").NotFound.Ignore();
        }
    }

    public class MessengerFriendData : MessengerUserData
    {

    }
}
