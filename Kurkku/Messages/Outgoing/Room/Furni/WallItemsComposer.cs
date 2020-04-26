using Kurkku.Game;
using Kurkku.Storage.Database.Data;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Kurkku.Messages.Outgoing
{
    public class WallItemsComposer : IMessageComposer
    {
        private List<PlayerData> owners;
        private List<Item> wallItems;

        public WallItemsComposer(ConcurrentDictionary<int, Item> items)
        {
            wallItems = items.Where(x => x.Value.Definition.HasBehaviour(ItemBehaviour.WALL_ITEM)).Select(x => x.Value).ToList();
            owners = wallItems.GroupBy(x => x.Data.OwnerId).Select(p => p.First().Data.OwnerData).ToList(); // Create distinct list of room owners
        }

        public override void Write()
        {
            m_Data.Add(owners.Count);

            foreach (PlayerData playerData in owners)
            {
                m_Data.Add(playerData.Id);
                m_Data.Add(playerData.Name);
            }

            m_Data.Add(wallItems.Count);

            foreach (Item item in wallItems)
            {
                Serialize(this, item);
                m_Data.Add(item.Data.OwnerId);
            }
        }

        public static void Serialize(IMessageComposer composer, Item item)
        {
            composer.Data.Add(item.Id.ToString());
            composer.Data.Add(item.Definition.Data.SpriteId);
            composer.Data.Add(item.Data.WallPosition);
            composer.Data.Add((string)item.Interactor.GetExtraData());
            composer.Data.Add(-1);
            composer.Data.Add(item.Definition.Data.MaxStatus > 1 ? 1 : 0);
        }
    }
}
