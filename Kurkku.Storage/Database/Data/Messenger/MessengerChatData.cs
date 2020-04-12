using FluentNHibernate.Mapping;
using System;

namespace Kurkku.Storage.Database.Data
{
    public class MessengerChatMapping : ClassMap<MessengerChatData>
    {
        public MessengerChatMapping()
        {
            Table("messenger_chat_history");

            CompositeId()
                .KeyProperty(x => x.UserId, "user_id")
                .KeyProperty(x => x.FriendId, "friend_id");

            Map(x => x.UserId, "user_id").Generated.Insert();
            Map(x => x.FriendId, "friend_id").Generated.Insert();
            Map(x => x.Message, "message");
            Map(x => x.IsRead, "has_read");
            Map(x => x.MessagedAt, "messaged_at").Generated.Insert();
        }
    }

    public class MessengerChatData
    {
        public virtual int UserId { get; set; }
        public virtual int FriendId { get; set; }
        public virtual string Message { get; set; }
        public virtual bool IsRead { get; set; }
        public virtual DateTime MessagedAt { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var t = obj as MessengerChatData;

            if (t == null)
                return false;

            if (FriendId == t.FriendId &&
                UserId == t.UserId)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
