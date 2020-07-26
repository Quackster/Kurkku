using Kurkku.Storage.Database.Data;

namespace Kurkku.Messages.Outgoing
{
    class RoomInfoComposer : IMessageComposer
    {
        public RoomData roomData;
        private bool isLoading;
        private bool checkEntry;

        public RoomInfoComposer(RoomData roomData, bool isLoading, bool checkEntry)
        {
            this.roomData = roomData;
            this.isLoading = isLoading;
            this.checkEntry = checkEntry;
        }

        public override void Write()
        {
            Data.Add(isLoading);
            Data.Add(roomData.Id);
            Data.Add(roomData.Name);
            Data.Add(!roomData.IsOwnerHidden); 
            Data.Add(roomData.OwnerId);
            Data.Add(roomData.OwnerData == null ? string.Empty : roomData.OwnerData.Name);
            Data.Add((int)roomData.Status);
            Data.Add(roomData.UsersNow);
            Data.Add(roomData.UsersMax);
            Data.Add(roomData.Description);
            Data.Add(0);
            Data.Add(roomData.Category.IsTradingAllowed ? 2 : 0); // can category trade?
            Data.Add(roomData.Rating);
            Data.Add(0);
            Data.Add(roomData.Category.Id);
            Data.Add(0);
            Data.Add(0);
            Data.Add("");

            Data.Add(roomData.Tags.Count);

            foreach (var tag in roomData.Tags)
            {
                Data.Add(tag);
            }


            Data.Add(0);
            Data.Add(0);
            Data.Add(0);
            Data.Add(true);
            Data.Add(true);
            Data.Add(0);
            Data.Add(0);

            /*
            this._SafeStr_10153 = k.readBoolean(); == CHECK ENTRY
            this._SafeStr_10154 = k.readBoolean();
            this._SafeStr_10155 = k.readBoolean();
            var _local_2:Boolean = k.readBoolean();
            */

            Data.Add(checkEntry);
            Data.Add(false); // ??
            Data.Add(false); // ??
            Data.Add(false); // ??

            Data.Add(0); // ??
            Data.Add(0); // ??
            Data.Add(0); // ??
            Data.Add(true); // whether you can mute room
        }
    }
}
