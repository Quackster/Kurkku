
using Kurkku.Game;
using Kurkku.Messages.Headers;

namespace Kurkku.Messages.Outgoing.User
{
    public class UserInfoComposer : MessageComposer
    {
        private Player player;

        public UserInfoComposer(Player player)
        {
            this.player = player;
        }

        public override short Header
        {
            get { return OutgoingEvents.UserInfoComposer; }
        }

        public override void Write()
        {
            m_Data.Add(player.Data.Id);
            m_Data.Add(player.Data.Name);
            m_Data.Add(player.Data.Figure);
            m_Data.Add(player.Data.Sex.ToUpper());
            m_Data.Add(player.Data.Motto);
            m_Data.Add(player.Data.RealName);
            m_Data.Add(false);
            m_Data.Add(player.Statistics.Respect);
            m_Data.Add(player.Statistics.DailyRespectPoints);
            m_Data.Add(player.Statistics.DailyPetRespectPoints);
            m_Data.Add(true);
            m_Data.Add(player.Data.PreviousLastOnline.ToString("MM-dd-yyyy HH:mm:ss"));
            m_Data.Add(false);
            m_Data.Add(false);
        }
    }
}
