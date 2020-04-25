using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    public class FurniListNotificationComposer : IMessageComposer
    {
        private Dictionary<int, FurniListNotificationType> notifications;

        public FurniListNotificationComposer(Dictionary<int, FurniListNotificationType> notifications)
        {
            this.notifications = notifications;
        }

        public override void Write()
        {
            m_Data.Add(notifications.Count);

            foreach (var key in notifications.Values)
                m_Data.Add((int)key);

            m_Data.Add(notifications.Count);

            foreach (var value in notifications.Keys)
                m_Data.Add(value);
        }
    }
    
    public enum FurniListNotificationType
    {
        GENERIC = 1,
        PET = 3,
        BOT = 5,
        BADGE = 4,

    }
}
