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

            if (page.PageLayout == "frontpage3")
            {
                m_Data.Add("frontpage3");
                m_Data.Add(3);
                m_Data.Add(page.PageHeadline);
                m_Data.Add(page.PageTeaser);
                m_Data.Add("");

                string[] array = { "Kurkku Development", "Please browse our catalogue for a whole range of furniture", "", "How do I get Credits easily?", "1. Always ask permission from the bill payer first.\r\n2. Send HABBO in a UK SMS to 78881. You''ll get an SMS back with a voucher code and will be charged £3 plus your standard UK SMS rate, normally 10p.\r\n3. Enter the code below to redeem 35 Credits. For Habbo Credit options or to redeem a Wallie Voucher Card, simply click \"Get Credits >>\" below.", "Redeem a Habbo Voucher code here:", "", "#FAF8CC", "#FAF8CC", "Click here for more info..", "http://localhost/" };

                m_Data.Add(array.Length);

                foreach (var str in array)
                    m_Data.Add(str);

            }
            else if (page.PageLayout == "spaces")
            {
                m_Data.Add("spaces_new");
                m_Data.Add(1);
                m_Data.Add(page.PageHeadline);
                m_Data.Add(1);
                m_Data.Add(page.PageText1);
            }
            else if (page.PageLayout == "club_buy")
            {
                m_Data.Add("vip_buy");
                m_Data.Add(2);
                m_Data.Add("catalog_header_hc");
                m_Data.Add("hc_catalog_teaser");
                m_Data.Add(0);
            }
            else
            //if (page.PageLayout == "default_3x3")
            {
                m_Data.Add("default_3x3");
                m_Data.Add(3);
                m_Data.Add(page.PageHeadline);
                m_Data.Add(page.PageTeaser);
                m_Data.Add(page.PageSpecial);
                m_Data.Add(3);
                m_Data.Add(page.PageText1);
                m_Data.Add(page.PageTextDetails);
                m_Data.Add(page.PageTextTeaser);
            }

            m_Data.Add(0);
            m_Data.Add(-1);
            m_Data.Add(false);
        }
    }
}