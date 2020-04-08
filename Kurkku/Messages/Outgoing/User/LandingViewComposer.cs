using Kurkku.Messages.Headers;

namespace Kurkku.Messages.Outgoing
{
    public class LandingViewComposer : MessageComposer
    {
        private string promotion;
        private string catalogueTeaser;

        public override short Header
        {
            get { return OutgoingEvents.LandingViewComposer; }
        }

        public LandingViewComposer(string promotion, string catalogueTeaser)
        {
            this.promotion = promotion;
            this.catalogueTeaser = catalogueTeaser;
        }

        public override void Write()
        {
            m_Data.Add(this.promotion);
            m_Data.Add(this.catalogueTeaser);
        }
    }
}
