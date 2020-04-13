using Kurkku.Game;
using Kurkku.Util.Extensions;
using System.Collections.Generic;

namespace Kurkku.Messages.Outgoing
{
    public class UsersComposer : IMessageComposer
    {
        private List<IEntity> entities;

        public UsersComposer(List<IEntity> entities)
        {
            this.entities = entities;
        }

        public override void Write()
        {
            m_Data.Add(entities.Count);

            foreach (var entity in entities)
            {
                if (entity is Player player)
                {
                    m_Data.Add(player.Details.Id);
                    m_Data.Add(player.Details.Name);
                    m_Data.Add(player.Details.Motto);
                    m_Data.Add(player.Details.Figure);
                    m_Data.Add(player.RoomUser.InstanceId);
                    m_Data.Add(player.RoomUser.Position.X);
                    m_Data.Add(player.RoomUser.Position.Y);
                    m_Data.Add(player.RoomUser.Position.Z.ToClientValue());
                    m_Data.Add(player.RoomUser.Position.BodyRotation);
                    m_Data.Add(1);
                    m_Data.Add(player.Details.Sex.ToLower());

                    // TODO: Group shit for later
                    m_Data.Add(-1);
                    m_Data.Add(-1);
                    m_Data.Add(0);

                    m_Data.Add(player.Details.AchievementPoints);
                }
            }
        }
    }
}
