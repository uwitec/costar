using Moudle.DataAccess;
using Moudle.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moudle.Services
{
    public class AccountService
    {
        public Account GetAccountByID(long accountID)
        {
            AccountRepository accRep = new AccountRepository();
            Account account = accRep.GetAccountByID(accountID);
            return account;
        }
    }
}
