using Kurkku.Game;

namespace Kurkku.Messages.Outgoing
{
    class MessengerRequestErrorComposer : IMessageComposer
    {
        private MessengerRequestError messageRequestError;
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
