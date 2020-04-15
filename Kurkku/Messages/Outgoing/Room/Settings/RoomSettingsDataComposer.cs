using Kurkku.Storage.Database.Data;

namespace Kurkku.Messages.outoing
{
    internal class RoomSettingsDataComposer : IMessageComposer
    {
        private RoomData data;

        public RoomSettingsDataComposer(RoomData data)
        {
            this.data = data;
        }

        public override void Write()
        {
            m_Data.Add(data.Id);
            m_Data.Add(data.Name);
            m_Data.Add(data.Description);
            m_Data.Add((int)data.Status);
            m_Data.Add(data.Category.Id);
            m_Data.Add(data.UsersMax);
            m_Data.Add(data.UsersNow); // what the fuck is this??
            m_Data.Add(0); // what the fuck is this??
            m_Data.Add(0); // Tags
            m_Data.Add(0);

            m_Data.Add(data.AllowPets ? 1 : 0); 
            m_Data.Add(data.AllowPetsEat ? 1 : 0);
            m_Data.Add(data.AllowWalkthrough ? 1 : 0);
            m_Data.Add(data.IsHidingWall ? 1 : 0);
            m_Data.Add(data.WallThickness);
            m_Data.Add(data.FloorThickness);
            m_Data.Add(0);
            m_Data.Add(0);
            m_Data.Add(0);
            m_Data.Add(0);

        }
    }
}