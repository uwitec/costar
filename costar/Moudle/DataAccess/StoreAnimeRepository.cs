using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moudle.DataAccess
{
    public class StoreAnimeRepository
    {
        public List<StoreAnime> GetAllStoreAnimes()
        {
            List<StoreAnime> results = new List<StoreAnime>();
            using (LinqDataContext dc = new LinqDataContext())
            {
                results = dc.StoreAnimes.ToList();
            }
            return results;
        }
    }
}
