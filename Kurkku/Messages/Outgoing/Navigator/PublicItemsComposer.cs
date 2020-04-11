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
        public List<PublicItem> publicItems;

        public override short Header
        {
            get { return OutgoingEvents.PublicItemsComposer; }
        }

        public PublicItemsComposer(List<PublicItem> publicItems)
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
        
        public static void Compose(MessageComposer messageComposer, PublicItem publicItem)
        {
            messageComposer.Data.Add(publicItem.Data.BannerId);
            messageComposer.Data.Add(publicItem.Data.BannerType != BannerType.PUBLIC_FLAT ? publicItem.Data.Label : string.Empty);
            messageComposer.Data.Add(publicItem.Data.Description);
            messageComposer.Data.Add((int)publicItem.Data.ImageType);
            messageComposer.Data.Add(publicItem.Data.BannerType != BannerType.PUBLIC_FLAT ? publicItem.Data.Label : string.Empty);
            messageComposer.Data.Add(publicItem.Data.Image);
            messageComposer.Data.Add(publicItem.Data.ParentId);
            messageComposer.Data.Add(publicItem.Data.Room != null ? publicItem.Data.Room.UsersNow : 0);
            messageComposer.Data.Add((int)publicItem.Data.BannerType);

            if (publicItem.Data.BannerType == BannerType.TAG)
                messageComposer.Data.Add(string.Empty); // Tag to search

            if (publicItem.Data.BannerType == BannerType.CATEGORY)
                messageComposer.Data.Add(true); // is open

            if (publicItem.Data.BannerType == BannerType.FLAT)
                RoomInfoComposer.Compose(messageComposer, publicItem.Data.Room, false);

            if (publicItem.Data.BannerType == BannerType.PUBLIC_FLAT)
            {
                messageComposer.Data.Add(publicItem.Data.ImageType == ImageType.INTERNAL ? publicItem.Data.Image : "");
                messageComposer.Data.Add(0);
                messageComposer.Data.Add(0);
                messageComposer.Data.Add(publicItem.Data.Room.Description);
                messageComposer.Data.Add(publicItem.Data.Room.UsersMax);
                messageComposer.Data.Add(publicItem.Data.Room.Id);
            }
        }
    }
}