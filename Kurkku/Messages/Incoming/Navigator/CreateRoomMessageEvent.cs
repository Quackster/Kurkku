using Kurkku.Game;
using Kurkku.Network.Streams;
using System.Linq;
using Kurkku.Util.Extensions;
using Kurkku.Storage.Database.Data;
using Kurkku.Storage.Database.Access;
using Kurkku.Messages.Outgoing;

namespace Kurkku.Messages.Incoming
{
    public class CreateRoomMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            //  CreateRoomMessageEvent: 9 / [0][4]test[0][7]model_t
            string name = request.ReadString().FilterInput(true);
            string model = request.ReadString();

            var roomModel = RoomManager.Instance.RoomModels.FirstOrDefault(x => x.Data.Model == model);

            if (roomModel == null)
                return;

            string modelType = roomModel.Data.Model.Replace("model_", "");

            if (modelType != "a" &&
                    modelType != "b" &&
                    modelType != "c" &&
                    modelType != "d" &&
                    modelType != "e" &&
                    modelType != "f" &&
                    modelType != "i" &&
                    modelType != "j" &&
                    modelType != "k" &&
                    modelType != "l" &&
                    modelType != "m" &&
                    modelType != "n" &&
                    !player.IsSubscribed)
            {
                return; // Fuck off, scripter.
            }

            RoomData roomData = new RoomData
            {
                OwnerId = player.Details.Id,
                Name = name,
                Model = model,
                Description = string.Empty
            };

            RoomDao.NewRoom(roomData);
            player.Send(new FlatCreatedComposer(roomData.Id, roomData.Name));
        }
    }
}
