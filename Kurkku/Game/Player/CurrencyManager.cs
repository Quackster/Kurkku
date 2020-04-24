using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Game
{
    public class CurrencyManager
    {
        #region Properties

        private Player player;
        private Dictionary<SeasonalCurrencyType, int> currencies;

        #endregion

        #region Constructor

        public CurrencyManager(Player player, List<CurrencyData> currencies)
        {
            this.player = player;
            this.currencies = currencies.ToDictionary(x => x.SeasonalType, x => x.Balance);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Get the balance for this seasonal currency
        /// </summary>
        public int GetBalance(SeasonalCurrencyType currencyType)
        {
            return currencies.TryGetValue(currencyType, out var balance) ? balance : 0;
        }

        /// <summary>
        /// Set the balance for this seasonal currency
        /// </summary>
        public void SetBalance(SeasonalCurrencyType currencyType, int newBalance)
        {
            currencies[currencyType] = newBalance;
        }

        /// <summary>
        /// Add the balance for this seasonal currency (will also accept negatives)
        /// </summary>
        public void AddBalance(SeasonalCurrencyType currencyType, int newBalance)
        {
            currencies[currencyType] = CurrencyDao.GetCurrency(player.Details.Id, currencyType).Balance + newBalance;
        }

        /// <summary>
        /// Save list of currencies for user
        /// </summary>
        public void Save()
        {
            List<CurrencyData> currencyList = currencies
                .Select(kvp => new CurrencyData { UserId = player.Details.Id, SeasonalType = kvp.Key, Balance = kvp.Value })
                .ToList();

            CurrencyDao.SaveCurrencies(currencyList);
        }

        #endregion
    }
}
