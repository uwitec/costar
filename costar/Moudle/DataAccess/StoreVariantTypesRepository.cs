using Moudle.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moudle.DataAccess
{
    public class StoreVariantTypesRepository
    {
        /// <summary>
        /// Get All StoreVariantType
        /// </summary>
        /// <returns></returns>
        public List<StoreVariantType> GetAllStoreVariantType()
        {
            List<StoreVariantType> results = new List<StoreVariantType>();
            using (CostarDataContext linq = new CostarDataContext())
            {
                results = linq.StoreVariantTypes.ToList();
            }
            return results;
        }

        /// <summary>
        /// Get StoreVariantType By VariantTypeID
        /// </summary>
        /// <returns></returns>
        public StoreVariantType GetStoreVariantTypeByID(int VariantTypeID)
        {
            StoreVariantType results = null;
            using (CostarDataContext linq = new CostarDataContext())
            {
                results = linq.StoreVariantTypes.Where(c => c.VariantTypeID == VariantTypeID).FirstOrDefault();
            }
            return results;
        }
    }
}
