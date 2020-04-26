using System;
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
                Serialize(this, item);
            }
        }

        public static void Serialize(IMessageComposer composer, Item item)
        {
            composer.Data.Add(item.Id);
            composer.Data.Add(item.Definition.Type.ToUpper());
            composer.Data.Add(item.Id);
            composer.Data.Add(item.Definition.Data.SpriteId);

            switch (item.Definition.Data.Sprite)
            {
                case "landscape":
                    composer.Data.Add(4);
                    break;
                case "wallpaper":
                    composer.Data.Add(2);
                    break;
                case "floor":
                    composer.Data.Add(3);
                    break;
                case "poster":
                    composer.Data.Add(6);
                    break;
                default:
                    composer.Data.Add(1);
                    break;
            }

            ExtraDataManager.Instance.WriteExtraData(composer, item, true);

            composer.Data.Add(item.Definition.Data.IsRecyclable);
            composer.Data.Add(item.Definition.Data.IsTradable);
            composer.Data.Add(item.Definition.Data.IsStackable);
            composer.Data.Add(item.Definition.Data.IsSellable);

            composer.Data.Add(-1); // timer
            composer.Data.Add(false); // hasRentPeriodStarted():Boolean
            composer.Data.Add(-1); // embed room id

            if (!item.Definition.HasBehaviour(ItemBehaviour.WALL_ITEM))
            {
                composer.Data.Add("");
                composer.Data.Add(0); // todo: sprite code for wrapping
            }
        }
    }
}