using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Kurkku.Game
{
    public class TeleporterInteractor : Interactor
    {
        public const string TELEPORTER_CLOSE = "0";
        public const string TELEPORTER_OPEN = "1";
        public const string TELEPORTER_EFFECTS = "2";

        public override ExtraDataType ExtraDataType => ExtraDataType.StringData;

        public TeleporterInteractor(Item item) : base(item) { }

        public override void OnInteract(IEntity entity)
        {
            var roomUser = entity.RoomEntity;

            if (roomUser.AuthenticateTeleporterId != -1)
            {
                return;
            }

            Position front = Item.Position.GetSquareInFront();

            if (front != roomUser.Position && Item.Position != roomUser.Position)
            {
                roomUser.Move(front.X, front.Y);
                return;
            }

            var teleporterData = (TeleporterExtraData)GetJsonObject();

            string pairId = ((TeleporterExtraData)GetJsonObject()).LinkedItem;
            Item targetTeleporter = new Item(ItemDao.GetItem(pairId) ?? new ItemData());

            Item.Interactor.SetJsonObject(new TeleporterExtraData
            {
                LinkedItem = teleporterData.LinkedItem,
                State = TELEPORTER_OPEN
            });

            Item.Update();

            roomUser.Move(Item.Position.X, Item.Position.Y);
            // TODO: Reject walk requests 

            // Broken link, make user walk in then walk out
            if (string.IsNullOrEmpty(pairId) || targetTeleporter == null || targetTeleporter.Room == null)
            {
                Task.Delay(1000).ContinueWith(t =>
                {
                    roomUser.Move(
                        Item.Position.GetSquareInFront().X, 
                        Item.Position.GetSquareInFront().Y
                    );
                });


                Task.Delay(2000).ContinueWith(t =>
                {
                    Item.Interactor.SetJsonObject(new TeleporterExtraData
                    {
                        LinkedItem = teleporterData.LinkedItem,
                        State = TELEPORTER_CLOSE
                    });

                    Item.Update();
                });

                Task.Delay(2500).ContinueWith(t =>
                {
                    // TODO: Accept walk requests
                });

                return;
            }

            Task.Delay(500).ContinueWith(t =>
            {

            });
        }

        public override void OnPickup(IEntity entity)
        {
            var teleporterData = (TeleporterExtraData)GetJsonObject();

            Item.Interactor.SetJsonObject(new TeleporterExtraData
            {
                LinkedItem = teleporterData.LinkedItem,
                State = TELEPORTER_CLOSE
            });

            Item.Update();
        }

        public override void OnPlace(IEntity entity)
        {
            var teleporterData = (TeleporterExtraData)GetJsonObject();

            Item.Interactor.SetJsonObject(new TeleporterExtraData
            {
                LinkedItem = teleporterData.LinkedItem,
                State = TELEPORTER_CLOSE
            });

            Item.Update();
        }

        public override object GetJsonObject()
        {
            TeleporterExtraData extraData = null;

            try
            {
                extraData = JsonConvert.DeserializeObject<TeleporterExtraData>(Item.Data.ExtraData);
            }
            catch { }

            return extraData;
        }

        public override object GetExtraData(bool inventoryView = false)
        {
            if (NeedsExtraDataUpdate)
            {
                var jsonObject = (TeleporterExtraData)GetJsonObject();

                if (string.IsNullOrEmpty(jsonObject.State))
                    jsonObject.State = TELEPORTER_CLOSE;

                NeedsExtraDataUpdate = false;
                ExtraData = jsonObject.State;
            }

            return ExtraData;
        }
    }
}
