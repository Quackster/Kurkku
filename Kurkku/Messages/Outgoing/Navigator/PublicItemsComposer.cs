using Kurkku.Storage.Database.Data;
using System.Collections.Generic;

namespace Kurkku.Messages.Outgoing
{
    class PublicItemsComposer : IMessageComposer
    {
        public List<PublicItemData> publicItems;

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
        
        public static void Compose(IMessageComposer messageComposer, PublicItemData publicItem)
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
                FlatListComposer.Compose(messageComposer, publicItem.Room);

            if (publicItem.BannerType == BannerType.PUBLIC_FLAT)
            {
                /*
                public function _SafeStr_3944(k:_SafeStr_2170)
                {
                    this._SafeStr_10591 = k.readString();
                    this._SafeStr_10175 = k.readInt();
                    this._SafeStr_10592 = k.readInt();
                    this._SafeStr_10174 = k.readString();
                    this._SafeStr_10658 = k.readInt();
                    this._SafeStr_10173 = k.readInt();
                }
                */
                messageComposer.Data.Add(publicItem.Description);
                messageComposer.Data.Add(0);
                messageComposer.Data.Add(publicItem.DescriptionEntry);
                messageComposer.Data.Add(publicItem.Room.CCTs);
                messageComposer.Data.Add(publicItem.Room.UsersMax);
                messageComposer.Data.Add(publicItem.Room.Id);
            }
        }
    }
}