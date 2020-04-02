using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public class MessengerUser
    {
        #region Properties

        public PlayerData PlayerData
        {
            get; set;
        }

        public Player Player
        {
            get { return PlayerManager.Instance.GetPlayerById(PlayerData.Id); }
        }

        public bool IsOnline
        {
            get { return Player != null; }
        }

        public bool InRoom
        {
            get { return false; }
        }

        #endregion
    }
}
