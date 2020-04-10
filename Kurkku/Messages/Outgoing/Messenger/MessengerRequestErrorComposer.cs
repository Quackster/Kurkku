using Kurkku.Game;
using Kurkku.Messages.Headers;

namespace Kurkku.Messages.Outgoing
{
    class MessengerRequestErrorComposer : MessageComposer
    {
        private MessengerRequestError messageRequestError;

        public override short Header
        {
            get { return OutgoingEvents.MessengerRequestErrorComposer; }
        }

        public MessengerRequestErrorComposer(MessengerRequestError messageRequestError)
        {
            this.messageRequestError = messageRequestError;
        }

        public override void Write()
        {
            m_Data.Add((int)messageRequestError); // error code if enum error specified wasn't found by client
            m_Data.Add((int)messageRequestError);
        }
    }
}
