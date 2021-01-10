using Kurkku.Game;

namespace Kurkku.Messages.Outoing
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

            m_Data.Add(room.Data.Tags.Count);

            foreach (var tag in room.Data.Tags)
            {
                m_Data.Add(tag.Text);
            }

            m_Data.Add(room.Data.TradeSetting);
            m_Data.Add(room.Data.AllowPets ? 1 : 0);
            m_Data.Add(room.Data.AllowPetsEat ? 1 : 0);
            m_Data.Add(room.Data.AllowWalkthrough ? 1 : 0);
            m_Data.Add(room.Data.IsHidingWall ? 1 : 0);
            m_Data.Add(room.Data.WallThickness);
            m_Data.Add(room.Data.FloorThickness);
            m_Data.Add((int)room.Data.WhoCanMute);
            m_Data.Add((int)room.Data.WhoCanKick);
            m_Data.Add((int)room.Data.WhoCanBan);
            //m_Data.Add(room.Data.AllowPets ? 1 : 0); 
            //m_Data.Add(room.Data.AllowPetsEat ? 1 : 0);
            //m_Data.Add(room.Data.AllowWalkthrough ? 1 : 0);
            //m_Data.Add(room.Data.IsHidingWall ? 1 : 0);
            //m_Data.Add(room.Data.WallThickness);
            //m_Data.Add(room.Data.FloorThickness);

        }
    }
}