﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moudle.DataAccess
{
    public class StoreProductInventoyRepository
    {
        /// <summary>
        /// Delete ProductInventoy By InventoryID
        /// </summary>
        /// <param name="InventoryID"></param>
        public void DelProductInventoy(long InventoryID)
        {
            using (LinqDataContext linq = new LinqDataContext())
            {
                linq.StoreProductInventories.DeleteAllOnSubmit(linq.StoreProductInventories.Where(c => c.InventoryID == InventoryID));
                linq.SubmitChanges();
            }
        }

        /// <summary>
        /// Get StoreProductInventory By ProductID
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public StoreProductInventory GetInventoryByProductID(long productID)
        {
            StoreProductInventory result = null;
            using (LinqDataContext linq = new LinqDataContext())
            {
                result = linq.StoreProductInventories.Where(p => p.ProductID == productID).OrderBy(c=> c.SortOrder).SingleOrDefault();
            }
            return result;
        }

        /// <summary>
        /// Get StoreProductInventory By InventoryID
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public StoreProductInventory GetInventoryByInventoryID(long inventoryID)
        {
            StoreProductInventory result = null;
            using (LinqDataContext linq = new LinqDataContext())
            {
                result = linq.StoreProductInventories.Where(p => p.InventoryID == inventoryID).SingleOrDefault();
            }
            return result;
        }

        /// <summary>
        /// Add or Edit StoreProductInventory
        /// </summary>
        /// <param name="product"></param>
        public void SaveProductInventory(StoreProductInventory proInv)
        {
            using (LinqDataContext linq = new LinqDataContext())
            {
                if (proInv.InventoryID > 0)
                {
                    linq.StoreProductInventories.Attach(proInv, true);
                }
                else
                {
                    linq.StoreProductInventories.InsertOnSubmit(proInv);
                }
                linq.SubmitChanges();
            }
        }

    }
}