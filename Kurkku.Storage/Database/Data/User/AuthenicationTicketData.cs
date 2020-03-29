using Kurkku.Storage.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Storage.Database.Data
{
    public class AuthenicationTicketData
    {
        public virtual int UserId { get; set; }
        public virtual string Ticket { get; set; }
        public virtual DateTime? ExpireDate { get; set; }
        public virtual PlayerData PlayerData { get; set; }
    }
}
