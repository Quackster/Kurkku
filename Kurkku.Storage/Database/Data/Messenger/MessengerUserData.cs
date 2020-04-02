namespace Kurkku.Storage.Database.Data
{
    public class MessengerUserData
    {
        public virtual int UserId { get; set; }
        public virtual int FriendId { get; set; }

        public virtual PlayerData FriendData { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var t = obj as MessengerUserData;

            if (t == null)
                return false;

            if (FriendId == t.FriendId && UserId == t.UserId && t.FriendData != null && t.FriendData.Id == FriendId)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
