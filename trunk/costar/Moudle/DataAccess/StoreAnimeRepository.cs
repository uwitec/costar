using Moudle.DataAccess.Domain;
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
            using (CostarDataContext dc = new CostarDataContext())
            {
                results = dc.StoreAnimes.ToList();
            }
            return results;
        }
    }
}
