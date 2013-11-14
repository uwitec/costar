using System.Configuration;
using System.Data.SqlClient;

namespace DBUtility
{
  public class PubConstant
  {
    /// <summary>
    /// 连接字符串
    /// </summary>
    public static string ConnectionString
    {
      get { return ConfigurationManager.ConnectionStrings["ConnectionString"].ToString(); }
    }

  }
}
