using Moudle.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moudle.DataAccess
{
    public class AccountRepository
    {
        public Account GetAccountByID(long accountID)
        {
            Account result = new Account();
            using (CostarDataContext dc = new CostarDataContext())
            {
                result = dc.Accounts.Where(a => a.AccountID == accountID).FirstOrDefault();
            }
            return result;
        }

        internal List<Domain.Permission> GetAccountPermissionsByID(long AccountID)
        {
            throw new NotImplementedException();
        }
    }
}
