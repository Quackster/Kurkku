namespace Kurkku.Messages.Outgoing
{
    class RoomMuteSettingsComposer : IMessageComposer
    {
        private bool muteFlag;

        public RoomMuteSettingsComposer(bool muteFlag)
        {
            this.muteFlag = muteFlag;
        }

        public override void Write()
        {
            m_Data.Add(muteFlag);
        }
    }
}
