using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moudle.DataAccess
{
    public class StoreProductColorRepository
    {
        public void DelProductColor(long productID)
        {
            using (LinqDataContext linq = new LinqDataContext())
            {
                linq.StoreProductColors.DeleteOnSubmit(linq.StoreProductColors.Where(c => c.ProductID == productID).Single());
                linq.SubmitChanges();
            }
        }

    }
}
