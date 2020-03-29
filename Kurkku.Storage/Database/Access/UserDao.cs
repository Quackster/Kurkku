using Kurkku.Storage.Database.Data;
using Kurkku.Storage.Database.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Storage.Database.Access
{
    public class UserDao
    {
        public static bool Login(out PlayerData loginData, string ssoTicket)
        {
            PlayerData playerData = null;
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                AuthenicationTicketData ticketAlias = null;
                PlayerData playerDataAlias = null;

                var query = session.QueryOver(() => ticketAlias)
                    .JoinQueryOver(() => ticketAlias.PlayerData, () => playerDataAlias)
                    .Where(() =>
                        (ticketAlias.PlayerData != null &&
                        ticketAlias.UserId == playerDataAlias.Id) &&
                        ticketAlias.ExpireDate == null || ticketAlias.ExpireDate > DateTime.Now)
                .List();

                if (query.Count > 0)
                {
                    playerData = query[0].PlayerData;
                }

                /*var query = session.CreateCriteria(typeof(AuthenicationTicketData)).List<AuthenicationTicketData>();
                
                if (query.Count > 0)
                {
                    playerData = query[0].PlayerData;
                }*/
            }

            loginData = playerData;
            return false;
        }
    }
}
