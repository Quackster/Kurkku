using Kurkku.Game;
using System.Collections.Generic;

namespace Kurkku.Messages.Outgoing
{
    public class FurniListNotificationComposer : IMessageComposer
    {
        private Dictionary<FurniListNotificationType, List<int>> notifications;

        public FurniListNotificationComposer(List<Item> items)
        {
            notifications = new Dictionary<FurniListNotificationType, List<int>>();

            foreach (var item in items)
            {
                // Code this shit better at some point
                if (item.Definition.HasBehaviour(ItemBehaviour.WALL_ITEM))
                {
                    if (!notifications.ContainsKey(FurniListNotificationType.GENERIC_WALL))
                        notifications.Add(FurniListNotificationType.GENERIC_WALL, new List<int>());
                }
                else
                {
                    if (!notifications.ContainsKey(FurniListNotificationType.GENERIC_FLOOR))
                        notifications.Add(FurniListNotificationType.GENERIC_FLOOR, new List<int>());
                }


                if (item.Definition.HasBehaviour(ItemBehaviour.WALL_ITEM))
                {
                    notifications[FurniListNotificationType.GENERIC_WALL].Add(item.Id);
                }
                else
                {

                    notifications[FurniListNotificationType.GENERIC_FLOOR].Add(item.Id);
                }
            }
        }

        public override void Write()
        {
            /*m_Data.Add(notifications.Count);

            foreach (var key in notifications.Values)
                m_Data.Add((int)key);

            m_Data.Add(notifications.Count);

            foreach (var value in notifications.Keys)
                m_Data.Add(value);*/

            m_Data.Add(notifications.Count);

            foreach (var kvp in notifications)
            {
                m_Data.Add((int)kvp.Key);
                m_Data.Add(kvp.Value.Count);

                foreach (int itemId in kvp.Value)
                {
                    m_Data.Add(itemId);
                }
            }

        }
    }
    
    public enum FurniListNotificationType
    {
        GENERIC_FLOOR = 1,
        GENERIC_WALL = 2,
        PET = 3,
        BADGE = 4,
        BOT = 5,

    }
}
