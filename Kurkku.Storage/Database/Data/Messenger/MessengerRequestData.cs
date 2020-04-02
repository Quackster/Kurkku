
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

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

            Map(x => x.UserId, "user_id");
            Map(x => x.FriendId, "friend_id");

            References(x => x.FriendData, "friend_id").Not.Nullable();
        }
    }

    public class MessengerRequestData : MessengerUserData
    {

    }
}
