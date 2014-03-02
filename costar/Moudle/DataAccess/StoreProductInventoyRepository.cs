using Moudle.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moudle.DataAccess
{
    public class StoreProductInventoyRepository
    {
        private Connection _conn;

        public StoreProductInventoyRepository()
        {
            _conn = new Connection();
        }

        /// <summary>
        /// Delete ProductInventoy By InventoryID
        /// </summary>
        /// <param name="InventoryID"></param>
        public void DelProductInventoy(long InventoryID)
        {
            using (CostarDataContext linq = new CostarDataContext())
            {
                linq.StoreProductInventories.DeleteAllOnSubmit(linq.StoreProductInventories.Where(c => c.InventoryID == InventoryID));
                linq.SubmitChanges();
            }
        }

        /// <summary>
        /// Delete StoreProductInventory
        /// </summary>
        /// <param name="product"></param>
        public void DelProductInventoryByProduct(long productID)
        {
            using (CostarDataContext linq = new CostarDataContext())
            {
                linq.StoreProductInventories.DeleteAllOnSubmit(linq.StoreProductInventories.Where(c => c.ProductID == productID));
                linq.SubmitChanges();
            }
        }

        /// <summary>
        /// Get StoreProductInventory By ProductID
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public List<StoreProductInventory> GetInventoryByProductID(long productID)
        {
            List<StoreProductInventory> result = new List<StoreProductInventory>();
            using (CostarDataContext dc = new CostarDataContext())
            {
                result = dc.StoreProductInventories.Where(p => p.ProductID == productID).OrderBy(c => c.SortOrder).ToList();
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
            using (CostarDataContext linq = new CostarDataContext())
            {
                linq.ObjectTrackingEnabled = false;
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
            using (CostarDataContext dc = _conn.GetContext())
            {
                if (proInv.InventoryID > 0)
                    dc.StoreProductInventories.Attach(proInv, true);
                if (proInv.InventoryID == 0)
                {
                    dc.StoreProductInventories.InsertOnSubmit(proInv);
                }
                dc.SubmitChanges();
            }

            //using (CostarDataContext linq = new CostarDataContext())
            //{
            //    if (proInv.InventoryID > 0)
            //    {
            //        StoreProductInventory inv = linq.StoreProductInventories.Where(c => c.InventoryID == proInv.InventoryID).SingleOrDefault();
            //        Common.CommonClass.CopyObjectProperty(proInv, inv);
            //    }
            //    else
            //    {
            //        linq.StoreProductInventories.InsertOnSubmit(proInv);
            //    }
            //    linq.SubmitChanges();
            //}
        }

        /// <summary>
        /// SUM QtyAvail
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public decimal QtyAvailByProduct(long productID)
        {
            decimal QtyAvail = 0;
            using (CostarDataContext linq = new CostarDataContext())
            {
                int? b = linq.StoreProductInventories.Where(i => i.ProductID == productID).Sum(a => (int?)a.QtyAvail);
                decimal.TryParse(b.ToString(), out QtyAvail);
            }

            return QtyAvail;
        }
    }
}
