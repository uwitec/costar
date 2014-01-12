using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moudle.DataAccess
{
    public class StoreColorRepository
    {
        public List<StoreColor> GetAllStoreColors()
        {
            List<StoreColor> results = new List<StoreColor>();
            using (LinqDataContext dc = new LinqDataContext())
            {
                results = dc.StoreColors.ToList();
            }
            return results;
        }

        public List<StoreColor> GetAllStoreColorsByProductID(long productID)
        {
            List<StoreColor> results = new List<StoreColor>();
            using (LinqDataContext dc = new LinqDataContext())
            {
                results = (from c in dc.StoreColors
                           join pc in dc.StoreProductColors on c.ColorID equals pc.ColorID
                           where pc.ProductID == productID
                           select c).ToList();
            }
            return results;
        }
    }
}
