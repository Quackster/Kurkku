using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Storage.Database.Models
{
    public class Test
    {
        public virtual int Id
        {
            get;
            set;
        }

        public virtual int TestId
        {
            get;
            set;
        }

        public virtual string User
        {
            get;
            set;
        }
    }
}
