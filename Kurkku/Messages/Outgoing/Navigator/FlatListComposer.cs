using Kurkku.Game;
using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;

namespace Kurkku.Messages.Outgoing
{
    public class FlatListComposer : IMessageComposer
    {
        private int signifier;
        private List<Room> roomList;
        private PublicItemData promotion;

        public FlatListComposer(int signifier, List<Room> roomList, PublicItemData promotion)
        {
            this.signifier = signifier;
            this.roomList = roomList;
            this.promotion = promotion;
        }

        public override void Write()
        {
            m_Data.Add(0);
            m_Data.Add(Convert.ToString(this.signifier));
            m_Data.Add(roomList.Count);

            foreach (Room room in roomList)
            {
                FlatListComposer.Compose(this, room.Data);
            }

            //m_Data.Add(false);
            m_Data.Add(promotion != null);

            if (promotion != null)
            {
                PublicItemsComposer.Compose(this, promotion);
            }
        }

        public static void Compose(IMessageComposer messageComposer, RoomData room)
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
            messageComposer.Data.Add(room.TradeSetting); // can category trade?
            messageComposer.Data.Add(room.Rating);
            messageComposer.Data.Add(0);
            messageComposer.Data.Add(room.Category.Id);
            messageComposer.Data.Add(0);
            messageComposer.Data.Add(0);
            messageComposer.Data.Add("");
            messageComposer.Data.Add(room.Tags.Count);

            foreach (var tag in room.Tags)
            {
                messageComposer.Data.Add(tag);
            }

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
