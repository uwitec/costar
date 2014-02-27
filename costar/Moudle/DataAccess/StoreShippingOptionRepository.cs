using Moudle.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moudle.DataAccess
{
    public class StoreShippingOptionRepository
    {
        /// <summary>
        /// Get StoreProductInventory By ProductID
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public StoreShippingOption GetShippingByID(long ShippingOptionID)
        {
            StoreShippingOption result = null;
            using (CostarDataContext linq = new CostarDataContext())
            {
                result = linq.StoreShippingOptions.Where(p => p.ShippingOptionID == ShippingOptionID).SingleOrDefault();
            }
            return result;
        }


    }
}
