using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public class RoomPlayer : RoomEntity
    {
        #region Fields

        #endregion

        #region Properties

        public Player Player { get; private set; }

        #endregion

        #region Constructors

        public RoomPlayer(Player player) : base((IEntity)player)
        {
            Player = player;
        }

        #endregion
    }
}
