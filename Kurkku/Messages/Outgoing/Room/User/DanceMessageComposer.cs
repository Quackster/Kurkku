namespace Kurkku.Messages.Outgoing
{
    public class DanceMessageComposer : IMessageComposer
    {
        private int instanceId;
        private int danceId;

        public DanceMessageComposer(int instanceId, int danceId)
        {
            this.instanceId = instanceId;
            this.danceId = danceId;
        }

        public override void Write()
        {
            m_Data.Add(instanceId);
            m_Data.Add(danceId);
        }
    }
}
