﻿using Kurkku.Messages.Outgoing;
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

        /// <summary>
        /// Extend club days by months
        /// </summary>
        public void AddMonths(int months)
        {
            DateTime startTime;

            if (player.IsSubscribed)
                startTime = Data.ExpireDate;
            else
                startTime = DateTime.Now;

            if (Data == null)
            {
                var nextGiftDate = DateTime.Now;

                switch (ValueManager.Instance.GetString("club.gift.interval.type"))
                {
                    case "MONTH":
                        nextGiftDate = nextGiftDate.AddMonths(ValueManager.Instance.GetInt("club.gift.interval"));
                        break;
                    case "DAY":
                        nextGiftDate = nextGiftDate.AddDays(ValueManager.Instance.GetInt("club.gift.interval"));
                        break;
                }

                Data = new SubscriptionData
                {
                    SubscribedDate = DateTime.Now,
                    ExpireDate = startTime.AddMonths(months),
                    UserId = player.Details.Id,
                    GiftedDate = nextGiftDate
                };
            }
            else
                Data.ExpireDate = startTime.AddMonths(months);

            SubscriptionDao.SaveSubscription(Data);
            Update();
        }

        /// <summary>
        /// Send packets to update club
        /// </summary>
        public void Update()
        {
            player.Send(new UserRightsMessageComposer(player.IsSubscribed ? 2 : 0, player.Details.Rank));
            player.Send(new ScrSendUserInfoComposer(player.Subscription.Data));
        }


        /// <summary>
        /// Count the membership days when user logs on/off
        /// </summary>
        public void CountMemberDays()
        {
            if (player.IsSubscribed)
            {
                Data.SubscriptionAge += (long)DateTime.Now.Subtract(Data.SubscriptionAgeLastUpdated).TotalSeconds;
                Data.SubscriptionAgeLastUpdated = DateTime.Now;

                SubscriptionDao.SaveSubscription(Data);
            }
        }

        #endregion
    }
}
