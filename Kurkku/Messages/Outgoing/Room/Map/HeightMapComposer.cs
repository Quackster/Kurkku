using Kurkku.Game;

namespace Kurkku.Messages.Outgoing
{
    class HeightMapComposer : IMessageComposer
    {
        private RoomModel map;

        public HeightMapComposer(RoomModel roomModel)
        {
            this.map = roomModel;
        }

        public override void Write()
        {
            m_Data.Add(this.map.MapSizeX);
            m_Data.Add(this.map.MapSizeX * this.map.MapSizeY);

            for (int y = 0; y < this.map.MapSizeY; y++)
            {
                for (int x = 0; x < this.map.MapSizeX; x++)
                {
                    if (!this.map.IsTile(new Position(x, y)))
                    {
                        m_Data.Add((short)-1);
                    }
                    else
                    {
                        m_Data.Add((short)1);
                    }
                }

            }
        }
    }
}
