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

            if (!string.IsNullOrEmpty(roomUser.AuthenticateTeleporterId))
                return;

            Position front = Item.Position.GetSquareInFront();

            if (front != roomUser.Position && Item.Position != roomUser.Position)
            {
                roomUser.Move(front.X, front.Y);
                return;
            }

            var teleporterData = (TeleporterExtraData)GetJsonObject();

            string pairId = ((TeleporterExtraData)GetJsonObject()).LinkedItem;
            ItemData targetTeleporterData = ItemDao.GetItem(pairId);

            Item.UpdateStatus(TELEPORTER_OPEN);

            roomUser.Move(Item.Position.X, Item.Position.Y);
            roomUser.WalkingAllowed = false;

            // Broken link, make user walk in then walk out
            if (string.IsNullOrEmpty(pairId) || targetTeleporterData == null || RoomManager.Instance.GetRoom(targetTeleporterData.RoomId) == null)
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
                    Item.UpdateStatus(TELEPORTER_CLOSE);
                });

                Task.Delay(2500).ContinueWith(t =>
                {
                    roomUser.WalkingAllowed = true;
                });

                return;
            }

            var targetTeleporter = ItemManager.Instance.ResolveItem(targetTeleporterData.Id);
            var pairedTeleporter = targetTeleporter ?? new Item(targetTeleporterData);
            var pairedData = ((TeleporterExtraData)pairedTeleporter.Interactor.GetJsonObject());

            roomUser.AuthenticateTeleporterId = pairedTeleporter.Data.Id;

            // Check if the user is inside the teleporter, if so, walk out. Useful if the user is stuck inside.
            if (Item.Position == roomUser.Position && 
                !RoomTile.IsValidTile(roomUser.Room, entity, Item.Position.GetSquareInFront()))
            {
                Item.UpdateStatus(TELEPORTER_EFFECTS);

                Task.Delay(1000).ContinueWith(t =>
                {
                    if (string.IsNullOrEmpty(roomUser.AuthenticateTeleporterId))
                        return;

                    Item.UpdateStatus(TELEPORTER_CLOSE);
                });

                Task.Delay(2000).ContinueWith(t =>
                {
                    if (string.IsNullOrEmpty(roomUser.AuthenticateTeleporterId))
                        return;

                    if (pairedTeleporter.Data.RoomId == Item.Data.RoomId)
                    {
                        pairedTeleporter.UpdateStatus(TELEPORTER_EFFECTS);
                    }
                    else
                    {
                        roomUser.AuthenticateRoomId = pairedTeleporter.Data.RoomId;
                    }
                });

                Task.Delay(3000).ContinueWith(t =>
                {
                    if (string.IsNullOrEmpty(roomUser.AuthenticateTeleporterId))
                        return;

                    pairedTeleporter.UpdateStatus(TELEPORTER_OPEN);
                });

                Task.Delay(4000).ContinueWith(t =>
                {
                    if (string.IsNullOrEmpty(roomUser.AuthenticateTeleporterId))
                        return;

                    pairedTeleporter.UpdateStatus(TELEPORTER_CLOSE);
                    roomUser.AuthenticateTeleporterId = null;
                });

                return;
            }
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
