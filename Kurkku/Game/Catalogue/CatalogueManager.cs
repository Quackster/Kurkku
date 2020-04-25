﻿using Kurkku.Storage.Database.Access;
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
            Packages = CatalogueDao.GetPackages().Select(x => new CataloguePackage(x)).ToList();
            Discounts = CatalogueDao.GetDiscounts();
            DeserialisePageData();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Handle item purchase
        /// </summary>
        public void Purchase(int userId, int amount, string extraData, long datePurchase)
        {
            throw new NotImplementedException();
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
            return Items.Where(x => x.PageIds.Contains(pageId) && !x.Data.IsHidden && x.Definition != null).OrderBy(x => x.Data.OrderId).ToList();
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
