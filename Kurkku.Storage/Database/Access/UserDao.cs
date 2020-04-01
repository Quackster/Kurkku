using System;
using Kurkku.Storage.Database.Data.Player;

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

                var row = session.QueryOver(() => ticketAlias)
                    .JoinQueryOver(() => ticketAlias.PlayerData, () => playerDataAlias)
                    .Where(() =>
                        (ticketAlias.PlayerData != null &&
                        ticketAlias.UserId == playerDataAlias.Id) &&
                        ticketAlias.ExpireDate == null || ticketAlias.ExpireDate > DateTime.Now)
                .SingleOrDefault();

                if (row != null)
                    playerData = row.PlayerData;
            }

            loginData = playerData;
            return false;
        }
    }
}
