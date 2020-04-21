using System;
using System.Linq;
using Kurkku.Game;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    class PurchaseItemMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            var cataloguePage = CatalogueManager.Instance.GetPage(request.ReadInt(), player.Details.Rank, player.IsSubscribed);

            if (cataloguePage == null)
                return;

            int itemId = request.ReadInt();
            var catalogueItem = cataloguePage.Items.Where(x => x.Data.Id == itemId).FirstOrDefault();

            if (catalogueItem == null)
                return;

            string extraData = request.ReadString();
            int amount = request.ReadInt();

            var discount = CatalogueManager.Instance.GetBestDiscount(cataloguePage.Data.Id);
            
            Console.WriteLine("extra data: " + extraData);
            Console.WriteLine("amount: " + amount);

            /*
            private function _SafeStr_12512():void
            {
	            var _local_3:int;
	            var _local_4:int;
	            this._SafeStr_12508 = new Map();
	            var k:int = 1;
	            var _local_2:int;
	            while (k <= 100) {
		            _local_3 = this._SafeStr_5446.utils._SafeStr_6452(true, 1, k);
		            _local_4 = (k - _local_3);
		            if ((((_local_4 > _local_2)) && ((this._SafeStr_5446.utils._SafeStr_6860.indexOf(k) == -1)))){
			            this._SafeStr_12508.add(k, _local_4);
			            _local_2 = _local_4;
		            };
		            k++;
	            };
            }

            private function _SafeStr_12514(k:TimerEvent):void
            {
	            if (this._SafeStr_12510 > 0){
		            this._SafeStr_12510 = (this._SafeStr_12510 - 0.1);
		            if (this._SafeStr_12510 < 0){
			            this._SafeStr_12510 = 0;
		            };
		            this._SafeStr_4607.findChildByName("promo_text_effect").blend = this._SafeStr_12510;
	            };
            }
            */

            decimal amountToCharge = catalogueItem.Data.PriceCoins * amount;

            if (catalogueItem.AllowBulkPurchase && discount != null)
            {
                decimal percentageSaved = (decimal)(discount.ItemCountFree / discount.ItemCountRequired);
                int amountSaved = (int)Math.Ceiling(percentageSaved * amountToCharge);
                int newAmount = (int)(amountToCharge - amountSaved);
            }
        }
    }
}
