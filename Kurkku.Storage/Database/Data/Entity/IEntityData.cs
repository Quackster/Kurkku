using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Storage.Database.Data.Entity
{
    public interface IEntityData
    {
        int Id { get; set; }
        string Name { get; set; }
        string Figure { get; set; }
        string Sex { get; set; }
    }
}
