using Kurkku.Game;
using System.Collections.Generic;

namespace Kurkku.Messages.Outgoing
{
    class CataloguePagesComposer : IMessageComposer
    {
        private int rank;
        private bool hasClub;
        private List<CataloguePage> parentPages;

        public CataloguePagesComposer(int rank, bool hasClub)
        {
            this.rank = rank;
            this.hasClub = hasClub;
            this.parentPages = CatalogueManager.Instance.GetPages(-1, rank, hasClub);
        }

        public override void Write()
        {
            m_Data.Add(true);
            m_Data.Add(0);
            m_Data.Add(0);
            m_Data.Add(-1);
            m_Data.Add("root");
            m_Data.Add("");
            m_Data.Add(parentPages.Count);

            foreach (var childTab in parentPages)
            {
                AppendIndexNode(childTab);
                RecursiveIndexNode(childTab);
            }

            m_Data.Add(true);
        }

        private void RecursiveIndexNode(CataloguePage parentTab)
        {
            var childTabs = CatalogueManager.Instance.GetPages(parentTab.Data.Id, rank, hasClub);
            m_Data.Add(childTabs.Count);

            foreach (var childTab in childTabs)
            {
                AppendIndexNode(childTab);
                RecursiveIndexNode(childTab);
            }
        }

        private void AppendIndexNode(CataloguePage childTab)
        {
            m_Data.Add(true);
            m_Data.Add(childTab.Data.IconColour);
            m_Data.Add(childTab.Data.IconImage);
            m_Data.Add(childTab.Data.Id);
            m_Data.Add(childTab.Data.PageLink);
            m_Data.Add(childTab.Data.Caption);
        }
    }
}
