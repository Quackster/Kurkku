using Kurkku.Util.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public class BedInteractor : Interactor
    {
        public override ExtraDataType ExtraDataType => ExtraDataType.StringData;

        public BedInteractor(Item item) : base(item) { }

        public override void OnStop(IEntity entity) {
            Position destination = entity.RoomEntity.Position.Copy();

            if (!IsPillowTile(destination))
                destination = ConvertToPillow(destination);

            if (destination != null)
            {
                var tile = destination.GetTile(entity.RoomEntity.Room);

                if (tile == null)
                    return;

                if (!RoomTile.IsValidTile(entity.RoomEntity.Room, entity, destination))
                    return;

                entity.RoomEntity.Position.Rotation = Item.Position.Rotation;
                entity.RoomEntity.AddStatus("lay", Item.Definition.Data.TopHeight.ToClientValue());
                entity.RoomEntity.NeedsUpdate = true;
            }
        }

        /// <summary>
        /// Redirect walk request to pillow
        /// </summary>
        public override bool OnWalkRequest(IEntity entity, Position goal)
        {
            if (!IsPillowTile(goal))
            {
                var destination = ConvertToPillow(goal);

                if (destination != null)
                {
                    entity.RoomEntity.Move(destination.X, destination.Y);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Converts any affected tile of the item to the closest pillow tile
        /// </summary>
        public Position ConvertToPillow(Position position)
        {
            Position destination = position.Copy();

            if (!IsPillowTile(position))
            {
                foreach (Position tile in GetPillowTiles())
                {
                    if (Item.Position.Rotation == 0)
                        destination.Y = tile.Y;
                    else
                        destination.X = tile.X;

                    break;
                }
            }

            return destination;
        }

        /// <summary>
        /// Get if the tile a pillow tile
        /// </summary>
        public bool IsPillowTile(Position entityPosition)
        {
            if (entityPosition == Item.Position)
                return true;
            else
            {
                foreach (Position validTile in GetPillowTiles())
                {
                    if (validTile == entityPosition)
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Get the two pillow tiles for bed
        /// </summary>
        public List<Position> GetPillowTiles()
        {
            if (Item == null || Item.Position == null)
                return List.Create<Position>();

            List<Position> tiles = new List<Position>();
            tiles.Add(new Position(Item.Position.X, Item.Position.Y));

            int validPillowX = -1;
            int validPillowY = -1;

            if (Item.Position.Rotation == 0)
            {
                validPillowX = Item.Position.X + 1;
                validPillowY = Item.Position.Y;
            }

            if (Item.Position.Rotation == 2)
            {
                validPillowX = Item.Position.X;
                validPillowY = Item.Position.Y + 1;
            }

            tiles.Add(new Position(validPillowX, validPillowY));
            return tiles;
        }
    }
}
