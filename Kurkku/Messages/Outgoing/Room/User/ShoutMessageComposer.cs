using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    public class ShoutMessageComposer : ChatMessageComposer
    {
        public ShoutMessageComposer(int instanceId, string message, int colour, int gesture) : base(instanceId, message, colour, gesture)
        {

        }
    }
}
