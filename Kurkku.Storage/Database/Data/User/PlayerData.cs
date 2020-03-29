using Kurkku.Storage.Database.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Storage.Database.Data
{
    public class PlayerData : IEntityData
    {
        public virtual int Id { get; set; }
        public virtual string Username { get; set; }
        public virtual string Figure { get; set; }
        public virtual string Sex { get; set; }
        public virtual int Rank { get; set; }
        public virtual int Credits { get; set; }
        public virtual int Pixels { get; set; }
        public virtual DateTime JoinDate { get; set; }
        public virtual DateTime LastOnline { get; set; }
    }
}
