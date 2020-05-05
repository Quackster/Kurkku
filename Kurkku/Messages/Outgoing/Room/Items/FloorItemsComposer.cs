using Kurkku.Game;
using Kurkku.Storage.Database.Data;
using Kurkku.Util.Extensions;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System;

namespace Kurkku.Messages.Outgoing
{
    public class FloorItemsComposer : IMessageComposer
    {
        private List<PlayerData> owners;
        private List<Item> floorItems;

        public FloorItemsComposer(ConcurrentDictionary<int, Item> items)
        {
            floorItems = items.Where(x => !x.Value.Definition.HasBehaviour(ItemBehaviour.WALL_ITEM)).Select(x => x.Value).ToList();
            owners = floorItems.GroupBy(x => x.Data.OwnerId).Select(p => p.First().Data.OwnerData).ToList(); // Create distinct list of room owners
        }

        public override void Write()
        {
            m_Data.Add(owners.Count);

            foreach (PlayerData playerData in owners)
            {
                m_Data.Add(playerData.Id);
                m_Data.Add(playerData.Name);
            }

            m_Data.Add(floorItems.Count);

            foreach (Item item in floorItems)
            {
                Serialize(this, item);
                m_Data.Add(item.Data.OwnerId);
            }
        }

        public static void Serialize(IMessageComposer composer, Item item)
        {
            composer.Data.Add(item.Id);
            composer.Data.Add(item.Definition.Data.SpriteId);
            composer.Data.Add(item.Position.X);
            composer.Data.Add(item.Position.Y);
            composer.Data.Add(item.Position.Rotation);
            composer.Data.Add(item.Position.Z.ToClientValue());
            composer.Data.Add(0);
            InteractionManager.Instance.WriteExtraData(composer, item);
            composer.Data.Add(-1);
            composer.Data.Add(item.Definition.Data.MaxStatus > 1 ? 1 : 0);
        }
    }
}
