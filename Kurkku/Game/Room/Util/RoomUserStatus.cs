namespace Kurkku.Game
{
    public class RoomUserStatus
    {
        #region Fields
        #endregion

        #region Properties

        public string Key { get;  }
        public string Value { get; }

        #endregion

        #region Constructor

        public RoomUserStatus(string key, string value)
        {
            Key = key;
            Value = value;
        }

        #endregion
    }
}