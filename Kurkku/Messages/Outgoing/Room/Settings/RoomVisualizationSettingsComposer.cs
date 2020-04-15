namespace Kurkku.Messages.Outgoing
{
    class RoomVisualizationSettingsComposer : IMessageComposer
    {
        private int floorThickness;
        private int wallThickness;
        private bool hideWall;

        public RoomVisualizationSettingsComposer(int floorThickness, int wallThickness, bool hideWall)
        {
            this.floorThickness = floorThickness;
            this.wallThickness = wallThickness;
            this.hideWall = hideWall;
        }

        public override void Write()
        {
            m_Data.Add(hideWall);
            m_Data.Add(wallThickness);
            m_Data.Add(floorThickness);
        }
    }
}
