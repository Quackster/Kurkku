using Kurkku.Messages.Headers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    class HeightMapComposer : MessageComposer
    {
        private string heightmap;

        public HeightMapComposer(string heightmap)
        {
            this.heightmap = heightmap;
        }

        public override short Header => 
            OutgoingEvents.HeightMapComposer;

        public override void Write()
        {
            m_Data.Add(heightmap);
        }
    }
}
