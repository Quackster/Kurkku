using Kurkku.Storage.Database.Data;

namespace Kurkku.Messages.Outgoing
{
    internal class CataloguePageComposer : IMessageComposer
    {
        private CataloguePageData page;

        public CataloguePageComposer(CataloguePageData page)
        {
            this.page = page;
        }

        public override void Write()
        {
            m_Data.Add(page.Id);
            m_Data.Add(page.Layout);

            m_Data.Add(page.Images.Count);

            foreach (var image in page.Images)
                m_Data.Add(image);

            m_Data.Add(page.Texts.Count);

            foreach (var text in page.Texts)
                m_Data.Add(text);

            m_Data.Add(0);
            m_Data.Add(-1);
            m_Data.Add(false);
        }
    }
}