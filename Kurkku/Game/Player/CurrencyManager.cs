using Kurkku.Messages.Outgoing;
using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Game
{
    public class CurrencyManager : ILoadable
    {
        #region Properties

        private Player player;
        public Dictionary<SeasonalCurrencyType, int> Currencies;

        #endregion

        #region Constructor

        public CurrencyManager(Player player)
        {
            this.player = player;
        }

        public void Load()
        {
            this.Currencies = CurrencyDao.GetCurrencies(player.Details.Id).ToDictionary(x => x.SeasonalType, x => x.Balance < 0 ? 0 : x.Balance);
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
        /// Refresh user credits from db but also override them
        /// </summary>
        public void ModifyCredits(int creditsChanged)
        {
            player.Details.Credits = CurrencyDao.SaveCredits(player.Details.Id, creditsChanged);
        }

        /// <summary>
        /// Update curencies on client side
        /// </summary>
        public void UpdateCurrencies()
        {
            player.Send(new ActivityPointsComposer(Currencies));
        }

        /// <summary>
        /// Updates singular currency on client side
        /// </summary>
        public void UpdateCurrency(SeasonalCurrencyType seasonalCurrency, bool notify = true)
        {
            if (Currencies.TryGetValue(seasonalCurrency, out var balance))
                player.Send(new ActivityPointsNotificationComposer(seasonalCurrency, balance, notify));
        }


        /// <summary>
        /// Update curencies on client side
        /// </summary>
        public void UpdateCredits()
        {
            player.Send(new CreditsBalanceComposer(player.Details.Credits));
        }

        /// <summary>
        /// Save list of currencies for user
        /// </summary>
        public void SaveCurrencies()
        {
            List<CurrencyData> currencyList = Currencies
                .Select(kvp => new CurrencyData { UserId = player.Details.Id, SeasonalType = kvp.Key, Balance = kvp.Value })
                .ToList();

            CurrencyDao.SaveCurrencies(currencyList);
        }

        #endregion
    }
}
