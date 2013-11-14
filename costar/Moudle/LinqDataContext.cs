using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moudle
{
  public partial class LinqDataContext
  {
    public LinqDataContext() :
      base(global::DBUtility.PubConstant.ConnectionString, mappingSource)
    {
      OnCreated();
    }

  }
}
