using Kurkku.Messages.Outgoing;
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
        public Dictionary<SeasonalCurrencyType, int> Currencies;

        #endregion

        #region Constructor

        public CurrencyManager(Player player, List<CurrencyData> currencies)
        {
            this.player = player;
            this.Currencies = currencies.ToDictionary(x => x.SeasonalType, x => x.Balance < 0 ? 0 : x.Balance);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Get the balance for this seasonal currency
        /// </summary>
        public int GetBalance(SeasonalCurrencyType currencyType)
        {
            return Currencies.TryGetValue(currencyType, out var balance) ? balance : 0;
        }

        /// <summary>
        /// Set the balance for this seasonal currency
        /// </summary>
        public void SetBalance(SeasonalCurrencyType currencyType, int newBalance)
        {
            Currencies[currencyType] = newBalance;
        }

        /// <summary>
        /// Add the balance for this seasonal currency (will also accept negatives)
        /// </summary>
        public void AddBalance(SeasonalCurrencyType currencyType, int newBalance)
        {
            Currencies[currencyType] = CurrencyDao.GetCurrency(player.Details.Id, currencyType).Balance + newBalance;
        }

        /// <summary>
        /// Save list of currencies for user
        /// </summary>
        public void Save()
        {
            List<CurrencyData> currencyList = Currencies
                .Select(kvp => new CurrencyData { UserId = player.Details.Id, SeasonalType = kvp.Key, Balance = kvp.Value })
                .ToList();

            CurrencyDao.SaveCurrencies(currencyList);
        }

        /// <summary>
        /// Refresh user credits from db but also override them
        /// </summary>
        public void ModifyCredits(int creditsChanged)
        {
            player.Details.Credits = CurrencyDao.SaveCredits(player.Details.Id, creditsChanged);
            player.Send(new CreditsBalanceComposer(player.Details.Credits));
        }

        #endregion
    }
}
