using Kurkku.Game;
using Kurkku.Messages.Headers;

namespace Kurkku.Messages.Outgoing
{
    public class UserInfoComposer : IMessageComposer
    {
        private Player player;

        public UserInfoComposer(Player player)
        {
            this.player = player;
        }

        public override void Write()
        {
            m_Data.Add(player.Details.Id);
            m_Data.Add(player.Details.Name);
            m_Data.Add(player.Details.Figure);
            m_Data.Add(player.Details.Sex.ToUpper());
            m_Data.Add(player.Details.Motto);
            m_Data.Add(player.Details.RealName);
            m_Data.Add(false);
            m_Data.Add(player.Settings.Respect);
            m_Data.Add(player.Settings.DailyRespectPoints);
            m_Data.Add(player.Settings.DailyPetRespectPoints);
            m_Data.Add(true);
            m_Data.Add(player.Details.PreviousLastOnline.ToString("MM-dd-yyyy HH:mm:ss"));
            m_Data.Add(false);
            m_Data.Add(false);
        }
    }
}
