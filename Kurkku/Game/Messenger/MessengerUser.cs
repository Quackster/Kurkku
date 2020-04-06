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

        #region Public methods

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var t = obj as MessengerUser;

            if (t == null)
                return false;

            if (t.PlayerData.Id == this.PlayerData.Id)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
