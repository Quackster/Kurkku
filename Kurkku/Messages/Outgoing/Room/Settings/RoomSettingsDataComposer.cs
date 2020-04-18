using Kurkku.Game;

namespace Kurkku.Messages.outoing
{
    internal class RoomSettingsDataComposer : IMessageComposer
    {
        private Room room;

        public RoomSettingsDataComposer(Room room)
        {
            this.room = room;
        }

        public override void Write()
        {
            m_Data.Add(room.Data.Id);
            m_Data.Add(room.Data.Name);
            m_Data.Add(room.Data.Description);
            m_Data.Add((int)room.Data.Status);
            m_Data.Add(room.Data.Category.Id);
            m_Data.Add(room.Data.UsersMax);
            m_Data.Add(((room.Model.MapSizeX * room.Model.MapSizeY) > 100) ? 50 : 25); // what the fuck is this??
            m_Data.Add(0); // Tags
            m_Data.Add(1); // Trade settings
            m_Data.Add(room.Data.AllowPets ? 1 : 0);
            m_Data.Add(room.Data.AllowPetsEat ? 1 : 0);
            m_Data.Add(room.Data.AllowWalkthrough ? 1 : 0);
            m_Data.Add(room.Data.IsHidingWall ? 0 : 1);
            m_Data.Add(room.Data.WallThickness);
            m_Data.Add(room.Data.FloorThickness);
            m_Data.Add(1);
            m_Data.Add(1);
            m_Data.Add(1);
            //m_Data.Add(room.Data.AllowPets ? 1 : 0); 
            //m_Data.Add(room.Data.AllowPetsEat ? 1 : 0);
            //m_Data.Add(room.Data.AllowWalkthrough ? 1 : 0);
            //m_Data.Add(room.Data.IsHidingWall ? 1 : 0);
            //m_Data.Add(room.Data.WallThickness);
            //m_Data.Add(room.Data.FloorThickness);

        }
    }
}