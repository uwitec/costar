using Moudle.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moudle.DataAccess
{
    public class StoreProductRepository
    {
        private Connection _conn;

        public StoreProductRepository()
        {
            _conn = new Connection();
        }

        public StoreProduct GetProductByID(long productID)
        {
            StoreProduct result = null;
            using (CostarDataContext dc = _conn.GetContext())
            {
                result = dc.StoreProducts.Where(p => p.ProductID == productID).FirstOrDefault();
            }
            return result;
        }

        public void SaveProduct(StoreProduct product)
        {
            using (CostarDataContext dc = _conn.GetContext())
            {
                if (product.ProductID > 0)
                    dc.StoreProducts.Attach(product, true);
                if (product.ProductID == 0)
                {
                    product.AddeDate = DateTime.Now;
                    dc.StoreProducts.InsertOnSubmit(product);
                }
                dc.SubmitChanges();
            }
        }

        public void DelProduct(long productID)
        {
            using (CostarDataContext linq = _conn.GetContext())
            {
                linq.StoreProducts.DeleteOnSubmit(linq.StoreProducts.Where(c => c.ProductID == productID).Single());
                linq.SubmitChanges();
            }
        }

    }
}
