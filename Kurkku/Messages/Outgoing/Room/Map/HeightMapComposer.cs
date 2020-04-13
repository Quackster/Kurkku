using Kurkku.Messages.Headers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    class HeightMapComposer : IMessageComposer
    {
        private string heightmap;

        public HeightMapComposer(string heightmap)
        {
            this.heightmap = heightmap;
        }

        public override void Write()
        {
            m_Data.Add(heightmap);
        }
    }
}
