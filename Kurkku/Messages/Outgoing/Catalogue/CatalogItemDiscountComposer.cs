using Kurkku.Storage.Database.Data;

namespace Kurkku.Messages.Outgoing
{
    class CatalogItemDiscountComposer : IMessageComposer
    {
        private CatalogueDiscountData discount;

        public CatalogItemDiscountComposer(CatalogueDiscountData discount)
        {
            this.discount = discount;
        }

        public override void Write()
        {
            m_Data.Add(discount.PurchaseLimit); // The discount / bulk buy limit
            m_Data.Add((int)discount.ItemCountRequired); // A - "Buy A get B free"
            m_Data.Add((int)discount.ItemCountFree); // B
            m_Data.Add(0);
            m_Data.Add(0);//Count
            /*{
                m_Data.Add(40);
                m_Data.Add(99);
            }*/
        }
    }
}
