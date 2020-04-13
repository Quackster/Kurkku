using Kurkku.Messages.Headers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    class FloorHeightMapComposer : IMessageComposer
    {
        private string heightmap;

        public FloorHeightMapComposer(string heightmap)
        {
            this.heightmap = heightmap;
        }

        public override void Write()
        {
            m_Data.Add(heightmap);
        }
    }
}
