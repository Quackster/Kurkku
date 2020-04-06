using Kurkku.Messages.Headers;

namespace Kurkku.Messages.Outgoing.User
{
    public class WelcomeUserComposer : MessageComposer
    {

        public override short Header
        {
            get { return OutgoingEvents.WelcomeUserComposer; }
        }

        public override void Write()
        {
            m_Data.Add(0);
        }
    }
}
