using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    public class SaveRoomMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            int roomId = request.ReadInt();
            string name = request.ReadString();
            string description = request.ReadString();
            int roomAccess = request.ReadInt();
            string password = request.ReadString();
            int maxUsers = request.ReadInt();
            int categoryId = request.ReadInt();
            int tagCount = request.ReadInt();

            List<string> tags = new List<string>();

            for (int i = 0; i < tagCount; i++)
                tags.Add(request.ReadString().ToLower());

            int tradeSettings = request.ReadInt();
            bool allowPets = request.ReadBoolean();
            bool allowPetsEat = request.ReadBoolean();
            bool roomBlockingEnabled = request.ReadBoolean();
            bool hidewall = request.ReadBoolean();
            int wallThickness = request.ReadInt();
            int floorThickness = request.ReadInt();
            int whoMute = request.ReadInt();
            int whoKick = request.ReadInt();
            int whoBan = request.ReadInt();

            if (tradeSettings < 0 || tradeSettings > 2)
                tradeSettings = 0;

            if (whoMute < 0 || whoMute > 1)
                whoMute = 0;

            if (whoKick < 0 || whoKick > 1)
                whoKick = 0;

            if (whoBan < 0 || whoBan > 1)
                whoBan = 0;

            if (wallThickness < -2 || wallThickness > 1)
                wallThickness = 0;

            if (floorThickness < -2 || floorThickness > 1)
                floorThickness = 0;

            if (name.Length < 1)
                return;

            if (name.Length > 60)
                name = name.Substring(0, 60);

            if (maxUsers < 0)
                maxUsers = 10;

            if (maxUsers > 50)
                maxUsers = 50;

            if (tagCount > 2)
                return;
        }
    }
}
