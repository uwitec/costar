using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.Data;
using System.Reflection;

namespace Common
{
    /// <summary>
    /// 公共方法类
    /// </summary>
    public static class MyCommon
    {

        #region 截取字符

        public static string CutRight(string sSource, int iLength)
        {
            return sSource.Substring(0, sSource.Length - iLength);
        }

        public static string Left(string sSource, int iLength)
        {
            return sSource.Substring(0, iLength > sSource.Length ? sSource.Length : iLength);
        }

        public static string Right(string sSource, int iLength)
        {
            return sSource.Substring(iLength > sSource.Length ? 0 : sSource.Length - iLength);
        }
        #endregion

        #region 系统错误代码

        public static string ErrorCode(int code)
        {
            switch (code)
            {
                case 600:
                    return "用户名或密码错误";
                case 601:
                    return "验证码错误。";
                case 602:
                    return "用户名已存在。";
                case 603:
                    return "用户已被锁定。";
                default:
                    return "";
            }
        }
        #endregion

        #region 星期

        public static string FormatWeek(DayOfWeek w)
        {
            string[] week = new string[7] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            return week[(int)w];
        }
        #endregion

        #region 链接
        
        public static string LinkUrl(string url, string link)
        {
            if (url.Contains("http://"))
                return url;
            else if (string.IsNullOrEmpty(link))
                return url;
            else
                return url + link + "/";
        }

        /// <summary>
        /// 新目标页
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string LinkUrl(string url)
        {
            if (url.Contains("http://"))
                return "target=\"_blank\"";
            else
                return "";
        }
        #endregion

        #region 抛出异常
        
        /// <summary>
        /// 抛出异常
        /// </summary>
        /// <param name="str"></param>
        public static void ShowError(string str)
        {
            throw new Exception(str);
        }

        public static void ShowError(string str, int num)
        {
            throw new HttpException(num, str);
        }
        #endregion

        #region 获取IP
        
        public static string GetIp()
        {
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                return System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else
            {
                return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
        }
        #endregion

        #region 转换INT
        
        public static int ToInt(string s)
        {
            int _int;
            int.TryParse(s, out _int);
            return _int;
        }
        #endregion

        public static long ToLong(string s)
        {
            long _long = 0;
            long.TryParse(s, out _long);
            return _long;
        }

        #region 转换Decimal

        public static decimal ToDecimal(string s)
        {
            decimal _decimal;
            decimal.TryParse(s, out _decimal);
            return _decimal;
        }
        #endregion

        #region 弹出对话框
        
        /// <summary>
        /// 弹出对话框并跳转
        /// </summary>
        /// <param name="alert">提示文本</param>
        public static void Alert(string alert)
        {
            Alert(alert, "");
        }

        public static void HistoryBack()
        {
            HttpContext.Current.Response.Write("<script>location.href='javascript:history.go(-1)';</script>");
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 弹出对话框并跳转
        /// </summary>
        /// <param name="alert">提示文本</param>
        /// <param name="url">跳转页</param>
        public static void Alert(string alert, string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                HttpContext.Current.Response.Write("<script>alert('" + alert + "');location.href='javascript:history.go(-1)';</script>");
                HttpContext.Current.Response.End();
            }
            else
            {
                HttpContext.Current.Response.Write("<script>alert('" + alert + "');location.href='" + url + "';</script>");
                HttpContext.Current.Response.End();
            }
        }
        #endregion

        #region MD5
        
        /// <summary>
        /// MD5方法
        /// </summary>
        /// <param name="str">要加密的字串</param>
        /// <param name="code">16位/32位</param>
        /// <returns></returns>
        public static string Md5(string str, int code)
        {
            if (code == 32)
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
            else
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16).ToLower();
        }
        #endregion

        #region 获取目标Url源文件
        
        /// <summary>
        /// 获取目标Url源文件
        /// </summary>
        /// <param name="url">目标url</param>
        /// <param name="enCode">编码</param>
        /// <returns></returns>
        public static string GetViewSource(string url, Encoding enCode)
        {
            try
            {
                HttpWebRequest _request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse _response = (HttpWebResponse)_request.GetResponse();
                StreamReader strm = new StreamReader(_response.GetResponseStream(), enCode);
                return strm.ReadToEnd();
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region 去除HTML标签
        /// <summary>
        /// 去除HTML标签
        /// </summary>
        /// <returns></returns>
        public static string LoseHTML(string Htmlstring)
        {
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"-->", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<!--.*", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //Htmlstring =System.Text.RegularExpressions. Regex.Replace(Htmlstring,@"<A>.*</A>","");
            //Htmlstring =System.Text.RegularExpressions. Regex.Replace(Htmlstring,@"<[a-zA-Z]*=\.[a-zA-Z]*\?[a-zA-Z]+=\d&\w=%[a-zA-Z]*|[A-Z0-9]","");
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(amp|#38);", "&", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(lt|#60);", "<", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(gt|#62);", ">", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&#(\d+);", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = Htmlstring.Trim();
            //Htmlstring=HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }
        #endregion

        #region Linq 转 DataTable
        
        /// <summary>
        /// linq 转 datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="varlist"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names
            PropertyInfo[] oProps = null;

            if (varlist == null)
                return dtReturn;

            foreach (T rec in varlist)
            {
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                             == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
        #endregion

    }
}
