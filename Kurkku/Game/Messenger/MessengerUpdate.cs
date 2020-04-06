using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public enum MessengerUpdateType
    {
        RemoveFriend = -1,
        UpdateFriend = 0,
        AddFriend = 1
    }

    public class MessengerUpdate
    {
        public MessengerUpdateType UpdateType { get; set; }

        public MessengerUser Friend { get; set; }
    }
}