using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Util.Extensions;
using Kurkku.Game;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    public class ShoutMessageMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            string message = request.ReadString().FilterInput(true);
            int colourId = request.ReadInt();

            if (colourId >= 24)//HabboHotel.ColourChatCrash)
                return;

            if (colourId == 1 || colourId == 2)
                return;

            /*if (colourId == 23 && !session.getHabbo().hasFuse("fuse_mod"))
            {
                return;
            }*/

            player.RoomUser.Talk(ChatMessageType.SHOUT, message, colourId);

        }
    }
}
