using Moudle.DataAccess.Domain;
using Moudle.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moudle.DataAccess
{
    public class Connection
    {
        public CostarDataContext GetContext()
        {
            string connString = "";
            connString = Settings.Default.costarConnectionString;
            CostarDataContext cdc = new CostarDataContext(connString);
            return cdc;
        }
    }
}
