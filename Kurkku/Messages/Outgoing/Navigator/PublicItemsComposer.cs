using Kurkku.Game;
using Kurkku.Messages.Headers;
using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    class PublicItemsComposer : MessageComposer
    {
        public List<PublicItemData> publicItems;

        public override short Header
        {
            get { return OutgoingEvents.PublicItemsComposer; }
        }

        public PublicItemsComposer(List<PublicItemData> publicItems)
        {
            this.publicItems = publicItems;
        }

        public override void Write()
        {
            m_Data.Add(publicItems.Count);

            foreach (var item in publicItems)
                Compose(this, item);

            m_Data.Add(0);
            m_Data.Add(0);
        }
        
        public static void Compose(MessageComposer messageComposer, PublicItemData publicItem)
        {
            messageComposer.Data.Add(publicItem.BannerId);
            messageComposer.Data.Add(publicItem.BannerType != BannerType.PUBLIC_FLAT ? publicItem.Label : string.Empty);
            messageComposer.Data.Add(publicItem.Description);
            messageComposer.Data.Add((int)publicItem.ImageType);
            messageComposer.Data.Add(publicItem.BannerType != BannerType.PUBLIC_FLAT ? publicItem.Label : string.Empty);
            messageComposer.Data.Add(publicItem.Image);
            messageComposer.Data.Add(publicItem.ParentId);
            messageComposer.Data.Add(publicItem.Room != null ? publicItem.Room.UsersNow : 0);
            messageComposer.Data.Add((int)publicItem.BannerType);

            if (publicItem.BannerType == BannerType.TAG)
                messageComposer.Data.Add(string.Empty); // Tag to search

            if (publicItem.BannerType == BannerType.CATEGORY)
                messageComposer.Data.Add(true); // is open

            if (publicItem.BannerType == BannerType.FLAT)
                RoomInfoComposer.Compose(messageComposer, publicItem.Room, false);

            if (publicItem.BannerType == BannerType.PUBLIC_FLAT)
            {
                messageComposer.Data.Add(publicItem.ImageType == ImageType.INTERNAL ? publicItem.Image : "");
                messageComposer.Data.Add(0);
                messageComposer.Data.Add(0);
                messageComposer.Data.Add(publicItem.Room.Description);
                messageComposer.Data.Add(publicItem.Room.UsersMax);
                messageComposer.Data.Add(publicItem.Room.Id);
            }
        }
    }
}