namespace Kurkku.Game
{
    public abstract class RoomEntity {
        public IEntity Entity { get; set; }
        public Room Room { get; set; }
        public Position Position { get; set; }
        public Position Next { get; set; }
        public Position Goal { get; set; }
        public bool IsWalking { get; set; }
        public int InstanceId { get; set; }

        public RoomEntity(IEntity entity)
        {
            Entity = entity;
        }

        public void Reset()
        {
            IsWalking = false;
            Goal = null;
            Next = null;
            InstanceId = -1;
            Room = null;
        }
    }
}
