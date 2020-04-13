
using Kurkku.Messages.Headers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    class CloseConnectionComposer : MessageComposer
    {
        public override short Header => OutgoingEvents.CloseConnectionComposer;

        public override void Write()
        {

        }
    }
}
