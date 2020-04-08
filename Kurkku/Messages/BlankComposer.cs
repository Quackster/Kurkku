using Kurkku.Messages.Headers;

namespace Kurkku.Messages.Outgoing
{
    public class BlankComposer : MessageComposer
    {

        public override short Header
        {
            get { return /* OutgoingEvents */0; }
        }

        public override void Write()
        {

        }
    }
}
