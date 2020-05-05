using Kurkku.Game;

namespace Kurkku.Messages.Outgoing
{
    public class ClubGiftReceivedComposer : IMessageComposer
    {
        private CatalogueItem item;

        public ClubGiftReceivedComposer(CatalogueItem offer)
        {
            this.item = offer;
        }

        public override void Write()
        {
            m_Data.Add(item.Data.SaleCode);
            m_Data.Add(item.Packages.Count);

            foreach (CataloguePackage package in item.Packages)
            {
                m_Data.Add(package.Definition.Type);
                m_Data.Add(package.Definition.Data.SpriteId);
                m_Data.Add(package.Data.SpecialSpriteId);
                m_Data.Add(package.Data.Amount);
                m_Data.Add(false);
            }
        }
    }
}
