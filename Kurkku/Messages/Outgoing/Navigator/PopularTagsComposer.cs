using Kurkku.Storage.Database.Data;
using System.Collections.Generic;

namespace Kurkku.Messages.Outgoing
{
    class PopularTagsComposer : IMessageComposer
    {
        private List<PopularTag> popularTags;

        public PopularTagsComposer(List<PopularTag> lists)
        {
            this.popularTags = lists;
        }

        public override void Write()
        {
            m_Data.Add(this.popularTags.Count);

            foreach (var tag in this.popularTags)
            {
                m_Data.Add(tag.Tag);
                m_Data.Add(tag.Quantity);
            }
        }
    }
}