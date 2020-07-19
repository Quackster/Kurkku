using System;
using System.Collections.Generic;

namespace Kurkku.Game
{
    public class DiceInteractor : Interactor
    {
        #region Overridden Properties

        public override ExtraDataType ExtraDataType => ExtraDataType.StringData;
        public override bool RequiresTick => false;

        #endregion

        #region Constructor

        public DiceInteractor(Item item) : base(item) { }

        #endregion

        public override bool OnWalkRequest(IEntity entity, Position goal)
        {
            var closestTile = Item.Position.ClosestTile(Item.Room, entity.RoomEntity.Position, entity);

            if (closestTile != null)
            {
                entity.RoomEntity.Move(closestTile.X, closestTile.Y);
                return true;
            }

            return false;
        }

        public override void OnInteract(IEntity entity)
        {
            if (!Item.Position.Touches(entity.RoomEntity.Position))
                return;

            // Queue future task for rolling dice
            if (!QueuedStates.ContainsKey("ROLL_DICE"))
            {
                QueueState("ROLL_DICE", 4.0, new Dictionary<object, object>
                {
                    { "ENTITY", entity }
                });
            }
        }

        public override void ProcessTickState(string state, Dictionary<object, object> attributes) 
        {
            switch (state)
            {
                case "ROLL_DICE":
                    {
                        Console.WriteLine("dice rolled!");
                    }
                    break;
            }
        }
    }
}
