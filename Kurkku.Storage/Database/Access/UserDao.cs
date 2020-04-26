using Kurkku.Storage.Database.Data;
using NHibernate.Linq;
using System;
using System.Linq;

namespace Kurkku.Storage.Database.Access
{
    public class UserDao
    {
        /// <summary>
        /// Login user with SSO ticket
        /// </summary>
        /// <param name="loginData">the player data to set</param>
        /// <param name="ssoTicket">the sso ticket to try</param>
        /// <returns></returns>
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
                        (ticketAlias.PlayerData != null && ticketAlias.Ticket == ssoTicket) &&
                        (ticketAlias.UserId == playerDataAlias.Id) &&
                        (ticketAlias.ExpireDate == null || ticketAlias.ExpireDate > DateTime.Now))
                    .Take(1)
                .SingleOrDefault();

                if (row != null)
                    playerData = row.PlayerData;
            }

            loginData = playerData;
            return false;
        }

        /// <summary>
        /// Save player data
        /// </summary>
        /// <param name="playerData">the player data to save</param>
        public static void Update(PlayerData playerData)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.SaveOrUpdate(playerData);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        /// <summary>
        /// Get player by username
        /// </summary>
        public static PlayerData GetByName(string name)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                return session.QueryOver<PlayerData>().Where(x => x.Name == name).SingleOrDefault();
            }
        }

        /// <summary>
        /// Get player by id
        /// </summary>
        public static PlayerData GetById(int id)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                return session.QueryOver<PlayerData>().Where(x => x.Id == id).SingleOrDefault();
            }
        }

        /// <summary>
        /// Get player name by id
        /// </summary>
        public static string GetNameById(int id)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                return session.QueryOver<PlayerData>().Select(x => x.Name).Where(x => x.Id == id).SingleOrDefault<string>();
            }
        }

        /// <summary>
        /// Get player id by name
        /// </summary>
        public static int GetIdByName(string name)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                return session.QueryOver<PlayerData>().Select(x => x.Id).Where(x => x.Name == name).SingleOrDefault<int>();
            }
        }

        /// <summary>
        /// Update last online for the last online
        /// </summary>
        public static void SaveLastOnline(PlayerData playerData)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                session.Query<PlayerData>().Where(x => x.Id == playerData.Id).Update(x => new PlayerData { LastOnline = playerData.LastOnline });
            }
        }
    }
}
