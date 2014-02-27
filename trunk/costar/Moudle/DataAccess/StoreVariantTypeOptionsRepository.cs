using Moudle.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moudle.DataAccess
{
    public class StoreVariantTypeOptionsRepository
    {
        /// <summary>
        /// Get All StoreVariantTypeOption
        /// </summary>
        /// <returns></returns>
        public List<StoreVariantTypeOption> GetAllStoreVariantTypeOption()
        {
            List<StoreVariantTypeOption> results = new List<StoreVariantTypeOption>();
            using (CostarDataContext linq = new CostarDataContext())
            {
                results = linq.StoreVariantTypeOptions.ToList();
            }
            return results;
        }

        /// <summary>
        /// Get StoreVariantTypeOption By VariantTypeID
        /// </summary>
        /// <returns></returns>
        public List<StoreVariantTypeOption> GetStoreVariantTypeOptionByVariantTypeID(int VariantTypeID)
        {
            List<StoreVariantTypeOption> results = new List<StoreVariantTypeOption>();
            using (CostarDataContext linq = new CostarDataContext())
            {
                results = linq.StoreVariantTypeOptions.Where(c => c.VariantTypeID == VariantTypeID).ToList();
            }
            return results;
        }

    }
}
