using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moudle.DataAccess.Domain
{
    public partial class Account
    {
        private List<Permission> _permissions = null;
        public List<Permission> Permissions
        {
            get
            {
                if (_permissions == null)
                {
                    AccountRepository accRep = new AccountRepository();
                    _permissions = accRep.GetAccountPermissionsByID(AccountID);
                }
                return _permissions;
            }
        }
    }
}
