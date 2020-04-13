using Kurkku.Messages.Headers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    class FloorHeightMapComposer : MessageComposer
    {
        private string heightmap;

        public FloorHeightMapComposer(string heightmap)
        {
            this.heightmap = heightmap;
        }

        public override short Header => 
            OutgoingEvents.FloorHeightMapComposer;

        public override void Write()
        {
            m_Data.Add(heightmap);
        }
    }
}
