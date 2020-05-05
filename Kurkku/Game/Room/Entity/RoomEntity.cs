using Kurkku.Messages.Outgoing;
using Kurkku.Util.Extensions;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Kurkku.Game
{
    public abstract class RoomEntity {
        public IEntity Entity { get; set; }
        public Room Room { get; set; }
        public Position Position { get; set; }
        public Position Next { get; set; }
        public Position Goal { get; set; }
        public List<Position> PathList { get; private set; }
        public bool IsWalking { get; set; }
        public int InstanceId { get; set; }
        public bool NeedsUpdate { get; set; }
        public int RoomId => Room != null ? Room.Data.Id : 0;
        public RoomTile Tile => Position != null ? (Position.GetTile(Room) ?? null) : null;
        public Item CurrentItem => Tile?.HighestItem;
        public RoomTimerManager TimerManager { get; set; }

        /// <summary>
        /// Get the status handling, the value is the value string and the time it was added.
        /// </summary>
        public ConcurrentDictionary<string, RoomUserStatus> Status { get; set; }

        public RoomEntity(IEntity entity)
        {
            Entity = entity;
            TimerManager = new RoomTimerManager();
        }

        /// <summary>
        /// Reset handler called when user leaves room
        /// </summary>
        public void Reset()
        {
            Status = new ConcurrentDictionary<string, RoomUserStatus>();
            IsWalking = false;
            Goal = null;
            Next = null;
            InstanceId = -1;
            Room = null;
            TimerManager.Reset();
        }

        /// <summary>
        /// Chat message handling
        /// </summary>
        public void Talk(ChatMessageType chatMessageType, string chatMsg, int colourId = 0, List<Player> recieveMessages = null)
        {
            if (recieveMessages == null)
                recieveMessages = Room.EntityManager.GetEntities<Player>();

            // Send talk message to room
            foreach (Player player in recieveMessages)
            {
                switch (chatMessageType)
                {
                    case ChatMessageType.SHOUT:
                        player.Send(new ShoutMessageComposer(InstanceId, chatMsg, colourId, GetChatGesture(chatMsg)));
                        break;
                    case ChatMessageType.CHAT:
                        player.Send(new ChatMessageComposer(InstanceId, chatMsg, colourId, GetChatGesture(chatMsg)));
                        break;
                }
            }
        }

        /// <summary>
        /// Get chat gesture for message
        /// </summary>
        private int GetChatGesture(string chatMsg)
        {
            chatMsg = chatMsg.ToLower();

            if (chatMsg.Contains(":)") || chatMsg.Contains(":d") || chatMsg.Contains("=]") ||
                chatMsg.Contains("=d") || chatMsg.Contains(":>"))
            {
                return 1;
            }

            if (chatMsg.Contains(">:(") || chatMsg.Contains(":@"))
                return 2;

            if (chatMsg.Contains(":o"))
                return 3;

            if (chatMsg.Contains(":(") || chatMsg.Contains("=[") || chatMsg.Contains(":'(") || chatMsg.Contains("='["))
                return 4;

            return 0;
        }

        /// <summary>
        /// Request move handler
        /// </summary>
        /// <param name="x">x coord goal</param>
        /// <param name="y">y coord goal</param>
        public void Move(int x, int y)
        {
            if (Room == null)
                return;

            if (Next != null)
            {
                var oldPosition = Next.Copy();
                Position.X = oldPosition.X;
                Position.Y = oldPosition.Y;
                Position.Z = Room.Model.TileHeights[oldPosition.X, oldPosition.Y];
                NeedsUpdate = true;
            }

            Goal = new Position(x, y);

            if (!RoomTile.IsValidTile(Room, Entity, Goal))
                return;

            var pathList = Pathfinder.FindPath(Entity, Room, Position, Goal);

            if (pathList == null)
                return;

            if (pathList.Count > 0)
            {
                var goalTile = Goal.GetTile(Room);

                if (goalTile != null && goalTile.HighestItem != null)
                    if (goalTile.HighestItem.Interactor.OnWalkRequest(Entity, Goal))
                        return;

                PathList = pathList;
                IsWalking = true;
            }
        }

        /// <summary>
        /// Stopped walking handler
        /// </summary>
        public void StopWalking()
        {
            if (!this.IsWalking)
                return;

            this.IsWalking = false;
            this.PathList.Clear();
            this.Next = null;
            this.RemoveStatus("mv");
            this.InteractItem();
            this.NeedsUpdate = true;
        }

        /// <summary>
        /// Interact with current item by calling entity stop on interactor
        /// </summary>
        public void InteractItem()
        {
            var roomTile = Tile;

            if (roomTile == null)
                return;

            Position.Z = roomTile.GetWalkingHeight();

            Item item = CurrentItem;

            if (item == null || (
                item.Definition.InteractorType != InteractorType.CHAIR ||
                item.Definition.InteractorType != InteractorType.BED))
            {
                if (ContainsStatus("sit") || ContainsStatus("lay"))
                {
                    RemoveStatus("sit");
                    RemoveStatus("lay");
                }
            }
            
            if (item != null)
            {
                item.Interactor.OnStop(this.Entity);
            }

            this.NeedsUpdate = true;
        }

        /// <summary>
        /// Get if entity contains status
        /// </summary>
        public bool ContainsStatus(string statusKey)
        {
            return Status.ContainsKey(statusKey);
        }

        /// <summary>
        /// Adds a status with a key and value, along with the int64 time of when the status was added.
        /// </summary>
        /// <param name="key">the key</param>
        /// <param name="value">the value</param>
        public void AddStatus(string key, string value)
        {
            this.RemoveStatus(key);
            Status.TryAdd(key, new RoomUserStatus(key, value));
        }

        /// <summary>
        /// Removes a status by its given key
        /// </summary>
        /// <param name="key">the key to check for</param>
        public void RemoveStatus(string key)
        {
            if (Status.ContainsKey(key))
                this.Status.Remove(key, out _);
        }

        public RoomTile CurrentTile => Position.GetTile(Room);
    }
}
