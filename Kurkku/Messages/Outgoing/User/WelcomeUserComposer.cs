namespace Kurkku.Messages.Outgoing
{
    public class WelcomeUserComposer : IMessageComposer
    {
        public override void Write()
        {
            m_Data.Add(0);
        }
    }
}
