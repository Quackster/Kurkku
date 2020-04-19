using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    class CatalogItemDiscountComposer : IMessageComposer
    {
        public override void Write()
        {
            m_Data.Add(100); // The discount / bulk buy limit
            m_Data.Add(5); // A - "Buy A get B free"
            m_Data.Add(2); // B
            m_Data.Add(0);
            m_Data.Add(0);//Count
            /*{
                m_Data.Add(40);
                m_Data.Add(99);
            }*/
        }
    }
}
