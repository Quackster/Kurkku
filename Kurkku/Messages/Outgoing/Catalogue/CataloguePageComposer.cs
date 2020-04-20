using Kurkku.Game;
using System;

namespace Kurkku.Messages.Outgoing
{
    internal class CataloguePageComposer : IMessageComposer
    {
        private CataloguePage page;

        public CataloguePageComposer(CataloguePage page)
        {
            this.page = page;
        }

        public override void Write()
        {
            m_Data.Add(page.Data.Id);
            m_Data.Add(page.Data.Layout);

            m_Data.Add(page.Images.Count);

            foreach (var image in page.Images)
                m_Data.Add(image);

            m_Data.Add(page.Texts.Count);

            foreach (var text in page.Texts)
                m_Data.Add(text);

            m_Data.Add(page.Items.Count);

            foreach (CatalogueItem item in page.Items)
            {
                m_Data.Add(item.Data.Id);
                m_Data.Add(item.Data.SaleCode);
                m_Data.Add(item.Data.PriceCoins);
                m_Data.Add(item.Data.PricePixels);
                m_Data.Add(0);
                m_Data.Add(false);
                m_Data.Add(item.Packages.Count);
                
                foreach (CataloguePackage package in item.Packages)
                {
                    m_Data.Add(package.Definition.Type);
                    m_Data.Add(package.Definition.Data.SpriteId);
                    m_Data.Add(package.Data.SpecialSpriteId); // extra data
                    m_Data.Add(package.Data.Amount);
                    m_Data.Add(0);
                }

                m_Data.Add(false);
                m_Data.Add(item.AllowBulkPurchase);
            }

            m_Data.Add(-1);
            m_Data.Add(false);

           /*
            foreach (CatalogueItem item in page.Items)
            {
                m_Data.Add(item.Data.Id);
                m_Data.Add(item.Data.SaleCode);
                m_Data.Add(item.Data.PriceCoins);
                m_Data.Add(item.Data.PricePixels);
                m_Data.Add(0);
                m_Data.Add(false);
                m_Data.Add(item.Packages.Count);
                
                foreach (CataloguePackage package in item.Packages)
                {
                    m_Data.Add(package.Definition.Type);
                    m_Data.Add(package.Definition.Data.SpriteId);
                    m_Data.Add(package.Data.SpecialSpriteId); // extra data
                    m_Data.Add(package.Data.Amount);
                    //m_Data.Add(0);
                }

                m_Data.Add(true);

                m_Data.Add(1000);
                m_Data.Add(1000 - 24);
                m_Data.Add(0);

                m_Data.Add(true);
            }

            m_Data.Add(-1); // club level
            m_Data.Add(false);
            */
        }
    }
}
 