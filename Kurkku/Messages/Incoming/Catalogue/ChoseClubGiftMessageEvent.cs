using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using Kurkku.Util;
using Kurkku.Util.Extensions;

namespace Kurkku.Messages.Incoming.Catalogue
{
    class ChoseClubGiftMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            var subscriptionGift = SubscriptionManager.Instance.GetGift(request.ReadString());

            if (player.IsSubscribed)
                player.Subscription.Refresh();

            if (subscriptionGift == null || !player.IsSubscribed || player.Subscription.Data.GiftsRedeemable <= 0)
                return;

            if (!subscriptionGift.IsGiftRedeemable(player.Subscription.Data.SubscriptionAge))
                return;

            player.Subscription.Data.GiftsRedeemable--;

            SubscriptionDao.SaveGiftsRedeemable(player.Details.Id, player.Subscription.Data.GiftsRedeemable);
            CatalogueManager.Instance.Purchase(player.Details.Id, subscriptionGift.CatalogueItem.Data.Id, 1, string.Empty, DateUtil.GetUnixTimestamp());
        }
    }
}
