using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Storage.Database.Access
{
    public class CurrencyDao
    {
        /// <summary>
        /// Get currency data for user, if doesn't exist, create rows in database for each currency
        /// </summary>
        public static List<CurrencyData> GetCurrencies(int userId)
        {
            List<CurrencyData> currencyList = new List<CurrencyData>();

            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                CurrencyData currencyDataAlias = null;

                currencyList = session.QueryOver(() => currencyDataAlias)
                    .Where(() => currencyDataAlias.UserId == userId)
                    .List() as List<CurrencyData>;

                if (!currencyList.Any())
                {
                    currencyList = new List<CurrencyData>();
                    currencyList.Add(new CurrencyData { UserId = userId, SeasonalType = SeasonalCurrencyType.PUMPKINS, Balance = 0 });
                    currencyList.Add(new CurrencyData { UserId = userId, SeasonalType = SeasonalCurrencyType.PEANUTS, Balance = 0 });
                    currencyList.Add(new CurrencyData { UserId = userId, SeasonalType = SeasonalCurrencyType.STARS, Balance = 0 });
                    currencyList.Add(new CurrencyData { UserId = userId, SeasonalType = SeasonalCurrencyType.CLOUDS, Balance = 0 });
                    currencyList.Add(new CurrencyData { UserId = userId, SeasonalType = SeasonalCurrencyType.DIAMONDS, Balance = 0 });
                    currencyList.Add(new CurrencyData { UserId = userId, SeasonalType = SeasonalCurrencyType.DUCKETS, Balance = 0 });
                    currencyList.Add(new CurrencyData { UserId = userId, SeasonalType = SeasonalCurrencyType.LOYALTY_POINTS, Balance = 0 });

                    using (var transaction = session.BeginTransaction())
                    {
                        try
                        {
                            foreach (var currencyData in currencyList)
                                session.Save(currencyData);

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            transaction.Rollback();
             
                        }

                    }
                }
            }

            return currencyList;
        }

        /// <summary>
        /// Get singular currency for user straight from database
        /// </summary>
        public static CurrencyData GetCurrency(int userId, SeasonalCurrencyType seasonalCurrencyType)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                CurrencyData currencyDataAlias = null;

                return session.QueryOver(() => currencyDataAlias).Where(() => currencyDataAlias.UserId == userId && currencyDataAlias.SeasonalType == seasonalCurrencyType).SingleOrDefault();
            }
        }

        /// <summary>
        /// Save all currencies for user
        /// </summary>
        public static void SaveCurrencies(List<CurrencyData> currencyList)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        foreach (var currencyData in currencyList)
                            session.Update(currencyData);

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
            }
        }
    }
}
