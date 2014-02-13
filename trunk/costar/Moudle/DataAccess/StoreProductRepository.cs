using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moudle.DataAccess
{
    public class StoreProductRepository
    {
        public StoreProduct GetProductByID(long productID)
        {
            StoreProduct result = null;
            using (LinqDataContext dc = new LinqDataContext())
            {
                result = dc.StoreProducts.Where(p => p.ProductID == productID).FirstOrDefault();
            }
            return result;
        }

        public void SaveProduct(StoreProduct product)
        {
            using (LinqDataContext dc = new LinqDataContext())
            {
                if (product.ProductID > 0)
                    dc.StoreProducts.Attach(product);
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
            using (LinqDataContext linq = new LinqDataContext())
            {
                linq.StoreProducts.DeleteOnSubmit(linq.StoreProducts.Where(c => c.ProductID == productID).Single());
                linq.SubmitChanges();
            }
        }

    }
}
