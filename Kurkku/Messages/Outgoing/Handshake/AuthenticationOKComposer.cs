using Kurkku.Messages.Headers;

namespace Kurkku.Messages.Outgoing.Handshake
{
    public class AuthenticationOKComposer : MessageComposer
    {
        public override short Header
        {
            get { return OutgoingEvents.AuthenticationOKComposer; }
        }

        public override void Write()
        {
            
        }
    }
}
