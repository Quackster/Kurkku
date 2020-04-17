using Kurkku.Game;
using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    class CataloguePagesComposer : IMessageComposer
    {
        private int rank;
        private bool hasClub;
        private List<CataloguePageData> parentPages;

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

        private void RecursiveIndexNode(CataloguePageData parentTab)
        {
            var childTabs = CatalogueManager.Instance.GetPages(parentTab.Id, rank, hasClub);
            m_Data.Add(childTabs.Count);

            foreach (var childTab in childTabs)
            {
                AppendIndexNode(childTab);
                RecursiveIndexNode(childTab);
            }
        }

        private void AppendIndexNode(CataloguePageData childTab)
        {
            m_Data.Add(true);
            m_Data.Add(childTab.IconColour);
            m_Data.Add(childTab.IconImage);
            m_Data.Add(childTab.Id);
            m_Data.Add(childTab.Caption.ToLower().Replace(" ", "_"));
            m_Data.Add(childTab.Caption);
        }
    }
}
