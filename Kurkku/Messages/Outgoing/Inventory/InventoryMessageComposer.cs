using System.Collections.Concurrent;
using Kurkku.Game;

namespace Kurkku.Messages.Outgoing
{
    public class InventoryMessageComposer : IMessageComposer
    {
        private ConcurrentDictionary<int, Item> items;

        public InventoryMessageComposer(ConcurrentDictionary<int, Item> items)
        {
            this.items = items;
        }

        public override void Write()
        {
            m_Data.Add(1);
            m_Data.Add(1);
            m_Data.Add(items.Count);

            foreach (var item in items.Values)
            {
                m_Data.Add(item.Id);
                m_Data.Add(item.Definition.Type.ToUpper());
                m_Data.Add(item.Id);
                m_Data.Add(item.Definition.Data.SpriteId);

                switch (item.Definition.Data.Sprite)
                {
                    case "landscape":
                        m_Data.Add(4);
                        break;
                    case "wallpaper":
                        m_Data.Add(2);
                        break;
                    case "floor":
                        m_Data.Add(3);
                        break;
                    case "poster":
                        m_Data.Add(6);
                        break;
                    default:
                        m_Data.Add(1);
                        break;
                }

                m_Data.Add(0); // ??
                m_Data.Add(item.ExtraData);
                m_Data.Add(item.Definition.Data.IsRecyclable);
                m_Data.Add(item.Definition.Data.IsTradable);
                m_Data.Add(item.Definition.Data.IsStackable);
                m_Data.Add(item.Definition.Data.IsSellable);

                m_Data.Add(-1);
                m_Data.Add(true);
                m_Data.Add(-1);

                if (!item.Definition.HasBehaviour(ItemBehaviour.WALL_ITEM))
                {
                    m_Data.Add("");
                    m_Data.Add(0); // todo: sprite code for wrapping
                }
            }
        }
    }
}