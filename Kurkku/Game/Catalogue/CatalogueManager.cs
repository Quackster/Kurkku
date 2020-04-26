using Kurkku.Messages.Outgoing;
using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Game
{
    public class CatalogueManager : ILoadable
    {
        #region Fields

        public static readonly CatalogueManager Instance = new CatalogueManager();

        #endregion

        #region Properties

        public List<CataloguePage> Pages;
        public List<CatalogueItem> Items;
        public List<CataloguePackage> Packages;
        public List<CatalogueDiscountData> Discounts;

        #endregion

        #region Constructors

        public void Load()
        {
            Pages = CatalogueDao.GetPages().Select(x => new CataloguePage(x)).ToList();
            Items = CatalogueDao.GetItems().Select(x => new CatalogueItem(x)).ToList();
            Packages = CatalogueDao.GetPackages().Select(i => new CataloguePackage(i, Items.FirstOrDefault(x => x.Data.SaleCode == i.SaleCode))).ToList();
            Discounts = CatalogueDao.GetDiscounts();
            DeserialisePageData();

            var test = Items.Where(x => x.Data.IsPackage).ToList();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Handle item purchase
        /// </summary>
        public void Purchase(int userId, int itemId, int amount, string extraData, long datePurchase)
        {
            CatalogueItem catalogueItem = Items.FirstOrDefault(x => x.Data.Id == itemId);

            if (catalogueItem == null)
                return;

            List<ItemData> purchaseQueue = new List<ItemData>();

            for (int i = 0; i < amount; i++)
            {
                foreach (var cataloguePackage in catalogueItem.Packages)
                {
                    var dataList = GenerateItemData(userId, cataloguePackage, extraData, datePurchase);

                    if (!dataList.Any())
                        continue;

                    purchaseQueue.AddRange(dataList);
                }
            }

            // Bulk create items
            ItemDao.CreateItems(purchaseQueue);

            // Convert item data to item instance
            List<Item> items 
                = purchaseQueue.Select(x => new Item(x)).ToList();

            var player = PlayerManager.Instance.GetPlayerById(userId);

            if (player == null)
                return;

            player.Send(new PurchaseOKComposer(catalogueItem));

            foreach (var item in items)
                player.Inventory.AddItem(item);

            player.Send(new FurniListNotificationComposer(items));
            player.Send(new FurniListUpdateComposer());
        }

        /// <summary>
        /// Generate item data for purchasing item
        /// </summary>
        private List<ItemData> GenerateItemData(int userId, CataloguePackage cataloguePackage, string userInputMessage, long datePurchase)
        {
            List<ItemData> items = new List<ItemData>();

            ItemDefinition definition = ItemManager.Instance.GetDefinition(cataloguePackage.Definition.Data.Id);

            if (definition == null)
                return null;

            int itemsToGenerate = 1;
            object serializeable = null;

            switch (definition.InteractorType)
            {
                case InteractorType.POST_IT:
                    {
                        serializeable = new StickieExtraData
                        {
                            Message = string.Empty,
                            Colour = "FFFF33"
                        };

                        itemsToGenerate = 20;
                    }
                    break;
                case InteractorType.TROPHY:
                    {
                        serializeable = new TrophyExtraData
                        {
                            UserId = userId,
                            Message = userInputMessage,
                            Date = datePurchase
                        };
                    }
                    break;
            }

            var extraData = string.Empty;

            if (serializeable != null)
                extraData = JsonConvert.SerializeObject(serializeable);

            if (!string.IsNullOrEmpty(cataloguePackage.Data.SpecialSpriteId))
                extraData = cataloguePackage.Data.SpecialSpriteId;


            for (int i = 0; i < itemsToGenerate; i++)
            {
                ItemData itemData = new ItemData();
                itemData.OwnerId = userId;
                itemData.DefinitionId = cataloguePackage.Definition.Data.Id;
                itemData.ExtraData = extraData;
                items.Add(itemData);
            }

            return items;
        }

        /// <summary>
        /// Convert JSON arrays to list of images and strings
        /// </summary>
        public void DeserialisePageData()
        {
            foreach (var page in Pages)
            {
                page.Images = JsonConvert.DeserializeObject<List<string>>(page.Data.ImagesData);
                page.Texts = JsonConvert.DeserializeObject<List<string>>(page.Data.TextsData);

                var bestDiscount = GetBestDiscount(page.Data.Id);

                if (bestDiscount == null)
                {
                    foreach (var item in GetItems(page.Data.Id))
                        item.AllowBulkPurchase = false;
                }
            }
        }

        /// <summary>
        /// Get applicable pages for parent id
        /// </summary>
        public List<CataloguePage> GetPages(int parentId, int rank, bool hasClub)
        {
            var pages = Pages.Where(x => x.Data.ParentId == parentId && x.Data.IsEnabled && rank >= x.Data.MinRank).ToList();

            if (!hasClub)
                pages = pages.Where(x => !x.Data.IsClubOnly).ToList();

            return pages.OrderBy(x => x.Data.OrderId).ToList();
        }

        /// <summary>
        /// Get page by page id
        /// </summary>
        public CataloguePage GetPage(int pageId, int rank = 7, bool hasClub = true)
        {
            var page = Pages.Where(x => x.Data.Id == pageId && x.Data.IsEnabled && x.Data.IsNavigatable && rank >= x.Data.MinRank).FirstOrDefault();

            if (page == null)
                return null;

            if (page.Data.IsClubOnly && !hasClub)
                return null;

            return page;
        }

        /// <summary>
        /// Get applicable items for page id
        /// </summary>
        public List<CatalogueItem> GetItems(int pageId)
        {
            return Items.Where(x => x.PageIds.Contains(pageId) && !x.Data.IsHidden).OrderBy(x => x.Data.OrderId).ToList();
        }

        /// <summary>
        /// Get the best discount in the list of discounts by page id
        /// </summary>
        public CatalogueDiscountData GetBestDiscount(int pageId)
        {
            var discounts = Discounts.Where(x => x.PageId == pageId && x.ExpireDate > DateTime.Now).ToList();

            if (!discounts.Any())
                return null;

            return discounts
                .Where(x => x.DiscountBatchSize > 0 && x.DiscountAmountPerBatch > 0)
                .OrderByDescending(x => x.DiscountAmountPerBatch / x.DiscountBatchSize).FirstOrDefault();
        }

        /// <summary>
        /// Get package by catalogue item sale code
        /// </summary>
        public List<CataloguePackage> GetPackages(string saleCode)
        {
            return Packages.Where(x => x.Data.SaleCode == saleCode).ToList();
        }

        #endregion
    }
}
