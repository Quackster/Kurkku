using Kurkku.Messages.Outgoing;
using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public class Subscription : ILoadable
    {
        #region Fields

        private Player player;

        #endregion

        #region Properties

        public SubscriptionData Data { get; set; }

        #endregion

        #region Constructors

        public Subscription(Player player)
        {
            this.player = player;
        }

        public void Load()
        {
            this.Data = SubscriptionDao.GetSubscription(player.Details.Id);
        }

        #endregion

        #region Public methods

        public void AddMonths(int months)
        {
            DateTime startTime;

            if (player.IsSubscribed)
                startTime = Data.ExpireDate;
            else
                startTime = DateTime.Now;

            Data = new SubscriptionData
            {
                SubscribedDate = DateTime.Now,
                ExpireDate = startTime.AddMonths(months),
                UserId = player.Details.Id
            };

            SubscriptionDao.SaveSubscription(Data);

            player.Send(new UserRightsMessageComposer(player.IsSubscribed ? 2 : 0, player.Details.Rank));
            player.Send(new ScrSendUserInfoComposer(player.Subscription.Data));
        }

        #endregion
    }
}
