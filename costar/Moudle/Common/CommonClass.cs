using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Moudle.Common
{
    public static class CommonClass
    {
        public static void CopyObjectProperty<T>(T tSource, T tDestination) where T : class
        {
            PropertyInfo[] properties = tSource.GetType().GetProperties();
            foreach (PropertyInfo p in properties)
            {
                p.SetValue(tDestination, p.GetValue(tSource, null), null);
            }
        }
    }
}
