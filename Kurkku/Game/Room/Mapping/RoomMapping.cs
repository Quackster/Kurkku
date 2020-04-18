namespace Kurkku.Game
{
    public class RoomMapping
    {
        #region Fields

        private Room room;
        private RoomModel model;

        #endregion

        #region Properties

        public RoomTile[,] Tiles { get; private set; }

        #endregion

        #region Constructors

        public RoomMapping(Room room)
        {
            this.room = room;
            this.model = room.Model;
        }

        #endregion

        #region Public methods

        public void RegenerateMap()
        {
            Tiles = new RoomTile[model.MapSizeX, model.MapSizeY];

            for (int y = 0; y < model.MapSizeY; y++)
            {
                for (int x = 0; x < model.MapSizeX; x++)
                {
                    Tiles[x, y] = new RoomTile(
                        room,
                        new Position(x, y),
                        model.TileHeights[x, y],
                        model.TileStates[x, y]
                    );
                }
            }
        }

        #endregion
    }
}
