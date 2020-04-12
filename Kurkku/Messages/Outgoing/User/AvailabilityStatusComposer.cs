using Kurkku.Messages.Headers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    class AvailabilityStatusComposer : MessageComposer
    {
        private bool isOpen;
        private bool isTradingEnded;

        public override short Header
        {
            get { return OutgoingEvents.AvailabilityStatusComposer; }
        }

        public AvailabilityStatusComposer()
        {
            this.isOpen = true;
            this.isTradingEnded = false;
        }

        public override void Write()
        {
            m_Data.Add(isOpen);
            m_Data.Add(isTradingEnded);
        }
    }
}
