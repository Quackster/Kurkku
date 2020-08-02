using System;
using System.Collections.Generic;

namespace Kurkku.Game
{
    public class DiceInteractor : Interactor
    {
        public static class DiceAttributes
        {
            public const string ROLL_DICE = "ROLL_DICE";
            public const string ENTITY = "ENTITY";
        }

        #region Fields

        public DefaultTaskObject taskObject;
        public Random random;

        #endregion

        #region Overridden Properties

        public override ExtraDataType ExtraDataType => ExtraDataType.StringData;
        public override ITaskObject TaskObject => taskObject;

        #endregion

        #region Constructor

        public DiceInteractor(Item item) : base(item)
        {
            this.taskObject = new DefaultTaskObject(item); // If we want item ticking, this must not be null
            this.random = new Random();
        }

        #endregion

        /// <summary>
        /// Find closest sourrounding avaliable tile for player
        /// </summary>
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

        /// <summary>
        /// On interact dice handler
        /// </summary>
        public override void OnInteract(IEntity entity, int requestData)
        {
            if (Item.IsRolling || entity.RoomEntity.IsWalking)
                return;

            if (!Item.Position.Touches(entity.RoomEntity.Position))
                return;

            int.TryParse(Item.Data.ExtraData, out int currentMode);

            if (currentMode > 0)
            {
                Item.UpdateState("0");
                Item.Save();
                return;
            }
            else
            {
                // Queue future task for rolling dice
                if (!TaskObject.EventQueue.ContainsKey(DiceAttributes.ROLL_DICE))
                {
                    Item.UpdateState("-1");
                    Item.Save();

                    TaskObject.QueueEvent(DiceAttributes.ROLL_DICE, 2.0, RolledDice, new Dictionary<object, object>() {
                        {DiceAttributes.ENTITY, entity}
                    });
                }
            }
        }

        /// <summary>
        /// Handle rolled dice (see task object it 
        /// </summary>
        public void RolledDice(QueuedEvent queuedEvent)
        {
            if (!queuedEvent.HasAttribute(DiceAttributes.ENTITY))
                return;

            var diceRoll = random.Next(1, 7);

            Item.UpdateState(Convert.ToString(diceRoll));
            Item.Save();
        }
    }
}
