using Kurkku.Game;
using Kurkku.Messages.Headers;
using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    class UserFlatCatsComposer : MessageComposer
    {
        public List<NavigatorCategoryData> categories;

        public override short Header
        {
            get { return OutgoingEvents.UserFlatCatsComposer; }
        }

        public UserFlatCatsComposer(List<NavigatorCategoryData> categories)
        {
            this.categories = categories;
        }

        public override void Write()
        {
            m_Data.Add(categories.Count);

            foreach (var category in categories)
            {
                m_Data.Add(category.Id);
                m_Data.Add(category.Caption);
                m_Data.Add(category.IsEnabled);
            }
        }
    }
}