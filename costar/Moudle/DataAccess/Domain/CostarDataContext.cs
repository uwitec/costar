using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moudle.DataAccess.Domain
{
    public partial class CostarDataContext
    {
        public CostarDataContext()
            : base(DBUtility.PubConstant.ConnectionString, mappingSource)
        {}
    }
}
