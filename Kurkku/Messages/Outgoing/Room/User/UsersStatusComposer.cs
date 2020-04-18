using Kurkku.Game;
using Kurkku.Util.Extensions;
using System.Collections.Generic;

namespace Kurkku.Messages.Outgoing
{
    public class UsersStatusComposer : IMessageComposer
    {
        private List<IEntity> entities;

        public UsersStatusComposer(List<IEntity> entities)
        {
            this.entities = entities;
        }

        public override void Write()
        {
            m_Data.Add(entities.Count);

            foreach (var entity in entities)
            {
                m_Data.Add(entity.RoomEntity.InstanceId);
                m_Data.Add(entity.RoomEntity.Position.X);
                m_Data.Add(entity.RoomEntity.Position.Y);
                m_Data.Add(entity.RoomEntity.Position.Z.ToClientValue());
                m_Data.Add(entity.RoomEntity.Position.HeadRotation);
                m_Data.Add(entity.RoomEntity.Position.BodyRotation);

                string statusString = "/";

                foreach (var kvp in entity.RoomEntity.Status)
                {
                    statusString += kvp.Key;

                    if (kvp.Value.Value.Length > 0)
                    {
                        statusString += " ";
                        statusString += kvp.Value.Value;
                    }

                    statusString += "/";
                }

                m_Data.Add(statusString);
            }
        }
    }
}
