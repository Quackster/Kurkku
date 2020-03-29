using Kurkku.Storage.Database.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game.Entity
{
    interface IEntity<T> where T : IEntityData
    {
        T Data { get; }
    }
}
