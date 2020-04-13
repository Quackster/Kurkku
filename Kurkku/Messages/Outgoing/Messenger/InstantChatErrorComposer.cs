using System.Collections.Generic;
using Kurkku.Game;
using Kurkku.Messages.Headers;
using Kurkku.Storage.Database.Data;

namespace Kurkku.Messages.Outgoing
{
    public class InstantChatErrorComposer : IMessageComposer
    {
        private InstantChatError instantChatError;
        private int userId;
        private string message = string.Empty;

        public InstantChatErrorComposer(InstantChatError instantChatError, int userId)
        {
            this.instantChatError = instantChatError;
            this.userId = userId;
        }

        public override void Write()
        {
            m_Data.Add((int)instantChatError);
            m_Data.Add(userId);
            m_Data.Add(message);
        }
    }

    public enum InstantChatError
    {
        ReceiverMuted = 3,
        SenderMuted = 4,
        FriendOffline = 5,
        NotFriend = 6,
        FriendBusy = 7,
        SendingFailed = 10,
    }
}