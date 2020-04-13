
using FluentNHibernate.Mapping;

namespace Kurkku.Storage.Database.Data
{
    public class MessengerRequestMapping : ClassMap<MessengerRequestData>
    {
        public MessengerRequestMapping()
        {
            Table("messenger_request");

            CompositeId()
                .KeyProperty(x => x.UserId, "user_id")
                .KeyProperty(x => x.FriendId, "friend_id");

            Map(x => x.UserId, "user_id").Generated.Insert();
            Map(x => x.FriendId, "friend_id").Generated.Insert();

            References(x => x.FriendData, "friend_id")
                .ReadOnly()
                .Cascade.None();
        }
    }

    public class MessengerRequestData : MessengerUserData
    {

    }
}
