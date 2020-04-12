

using Kurkku.Game;
using Kurkku.Messages.Headers;
using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    class RoomInfoComposer : MessageComposer
    {
        public RoomData roomData;

        public override short Header
        {
            get { return OutgoingEvents.PublicItemsComposer; }
        }

        public RoomInfoComposer(RoomData room)
        {
            roomData = room;
        }

        public override void Write()
        {

        }

        public static void Compose(MessageComposer messageComposer, RoomData room)
        {
            messageComposer.Data.Add(room.Id);
            messageComposer.Data.Add(room.Name);
            messageComposer.Data.Add(true);
            messageComposer.Data.Add(room.OwnerId);
            messageComposer.Data.Add(room.OwnerData == null ? string.Empty : room.OwnerData.Name);
            messageComposer.Data.Add((int)room.Status);
            messageComposer.Data.Add(room.UsersNow);
            messageComposer.Data.Add(room.UsersMax);
            messageComposer.Data.Add(room.Description);
            messageComposer.Data.Add(0);
            messageComposer.Data.Add(room.Category.IsTradingAllowed ? 2 : 0); // can category trade?
            messageComposer.Data.Add(room.Rating);
            messageComposer.Data.Add(0);
            messageComposer.Data.Add(room.Category.Id);
            messageComposer.Data.Add(0);
            messageComposer.Data.Add(0);
            messageComposer.Data.Add("");
            messageComposer.Data.Add(0); // tags
            messageComposer.Data.Add(0);
            messageComposer.Data.Add(0);
            messageComposer.Data.Add(0);
            messageComposer.Data.Add(true);
            messageComposer.Data.Add(true);
            messageComposer.Data.Add(0);
            messageComposer.Data.Add(0);
        }
    }
}
