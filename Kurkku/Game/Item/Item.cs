using System.Collections.Generic;
using Kurkku.Messages.Outgoing;
using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;

namespace Kurkku.Game
{
    public class Item
    {
        #region Properties

        public int Id;
        public ItemData Data { get; }
        public ItemDefinition Definition => ItemManager.Instance.GetDefinition(Data.DefinitionId);
        public Room Room => RoomManager.Instance.GetRoom(Data.RoomId);
        public Interactor Interactor { get; }
        public Position Position { get; set; }

        #endregion

        #region Constructors

        public Item(ItemData data)
        {
            Data = data;
            Interactor = InteractionManager.Instance.CreateInteractor(this);
            Id = ItemManager.Instance.GenerateId();
            Position = new Position(data.X, data.Y, data.Z, data.Rotation, data.Rotation);
        }

        #endregion

        #region Public methods

        public void UpdateEntities(Position position = null)
        {
            List<IEntity> entities = new List<IEntity>();

            foreach (Position affectedPositon in AffectedTile.GetAffectedTiles(this))
            {
                var tile = affectedPositon.GetTile(Room);

                if (tile == null)
                    continue;

                entities.AddRange(tile.Entities.Values);
            }

            if (position != null)
            {
                foreach (Position affectedPositon in AffectedTile.GetAffectedTiles(this, position.X, position.Y, position.Rotation))
                {
                    var tile = affectedPositon.GetTile(Room);

                    if (tile == null)
                        continue;

                    entities.AddRange(tile.Entities.Values);
                }
            }

            foreach (IEntity entity in entities)
                entity.RoomEntity.InteractItem();
        }

        /// <summary>
        /// Get whether the item is walkable
        /// </summary>
        public bool IsWalkable(Position position)
        {
            if (Definition.HasBehaviour(ItemBehaviour.IS_WALKABLE))
                return true;

            if (Definition.HasBehaviour(ItemBehaviour.SOLID_SINGLE_TILE))
                return Position != position;

            if (Definition.InteractorType == InteractorType.CHAIR)
                return true;

            if (Definition.InteractorType == InteractorType.BED)
                return true;

            if (Definition.InteractorType == InteractorType.GATE || Definition.InteractorType == InteractorType.ONE_WAY_GATE)
                return Interactor.GetExtraData().Equals("1");

            return false;
        }

        /// <summary>
        /// Get total height for furni, which is floor z axis, plus height of furni
        /// </summary>
        public double Height => Definition.Data.TopHeight + Position.Z;

        /// <summary>
        /// Save item, todo: queue saving
        /// </summary>
        public void Save()
        {
            ItemDao.SaveItem(Data);
        }

        /// <summary>
        /// Updates item state
        /// </summary>
        public void Update()
        {
            if (Room == null)
                return;

            if (Definition.HasBehaviour(ItemBehaviour.WALL_ITEM))
                Room.Send(new UpdateWallItemComposer(this));
            else
                Room.Send(new UpdateFloorItemComposer(this));
        }

        /// <summary>
        ///  Check if the move is valid before moving an item. Will prevent long
        ///  furniture from being on top of rollers, will prevent placing rollers on top of other rollers.
        ///  Will prevent items being placed on closed tile states.
        /// </summary>
        public bool IsValidMove(Item item, Room room, int x, int y, int rotation)
        {
            RoomTile tile = Position.GetTile(room);

            if (tile == null || room == null)
                return false;

            bool isRotation = (item.Position.Rotation != rotation) && (new Position(x, y) == item.Position);

            if (isRotation)
            {
                if (item.Definition.Data.Length <= 1 && item.Definition.Data.Width <= 1)
                    return true;


                /*
                if (item.getRollingData() != null) {
                    return false; // Don't allow rotating items when they're rolling
                }

                if (item.getDefinition().getLength() <= 1 && item.getDefinition().getWidth() <= 1) {
                    return true;
                }
                */

                /*if (item.Definition.Data.Length <= 1 && item.Definition.Data.Width <= 1)
                    return true;*/
            }

            foreach (Position position in AffectedTile.GetAffectedTiles(this, x, y, rotation))
            {
                tile = position.GetTile(room);

                if (tile == null || !room.Model.IsTile(position))
                    return false;

                if (room.Model.TileStates[position.X, position.Y] == TileState.CLOSED)
                    return false;

                if (!isRotation/* && item.Definition.InteractorType != InteractorType.BED && item.Definition.InteractorType != InteractorType.CHAIR*/)
                {
                    if (tile.Entities.Count > 0)
                        return false;
                }

                Item highestItem = tile.HighestItem;

                if (highestItem != null && highestItem.Id != item.Id)
                {
                    if (!CanItemsMerge(item, highestItem, new Position(x, y)))
                        return false;

                }

                foreach (Item tileItem in tile.Furniture.Values)
                {
                    if (tileItem.Id == item.Id)
                        continue;

                    if (!CanItemsMerge(item, tileItem, new Position(x, y)))
                        return false;

                    if (tileItem.Definition.HasBehaviour(ItemBehaviour.ROLLER))
                    {
                        if (Definition.HasBehaviour(ItemBehaviour.ROLLER))
                            return false; // Can't place rollers on top of rollers

                        if ((Definition.Data.Length > 1 || Definition.Data.Width > 1) && (Definition.InteractorType == InteractorType.CHAIR || Definition.InteractorType == InteractorType.BED))
                            return false; // Chair or bed is too big to place on rollers.
                    }
                }
            }


            return true;
        }

        /// <summary>
        /// Get if placing an item on top of another item is allowed.
        /// </summary>
        private bool CanItemsMerge(Item item, Item tileItem, Position targetTile)
        {
            if (tileItem.Definition.HasBehaviour(ItemBehaviour.CAN_NOT_STACK_ON_TOP))
                return false;

            if (item.Definition.HasBehaviour(ItemBehaviour.ROLLER) && tileItem.Definition.HasBehaviour(ItemBehaviour.IS_STACKABLE) && !tileItem.Definition.HasBehaviour(ItemBehaviour.PLACE_ROLLER_ON_TOP))
            {
                if (tileItem.Definition.Data.TopHeight >= 0.1)
                    return false;
            }

            if ((tileItem.Definition.HasBehaviour(ItemBehaviour.SOLID) || tileItem.Definition.HasBehaviour(ItemBehaviour.SOLID_SINGLE_TILE)) && !tileItem.Definition.HasBehaviour(ItemBehaviour.IS_STACKABLE))
                return false;

            if (tileItem.Definition.HasBehaviour(ItemBehaviour.SOLID_SINGLE_TILE) && tileItem.Definition.HasBehaviour(ItemBehaviour.IS_STACKABLE))
                return tileItem.Position == targetTile;

            if (tileItem.Definition.InteractorType == InteractorType.ONE_WAY_GATE || tileItem.Definition.InteractorType == InteractorType.GATE)
                return false;


            if (tileItem.Definition.InteractorType == InteractorType.CHAIR)
                return false;

            if (tileItem.Definition.InteractorType == InteractorType.BED)
                return false;

            return true;
        }

        #endregion
    }
}
