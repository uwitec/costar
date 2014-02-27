using Common.Util;
using Moudle;
using Moudle.DataAccess.Domain;
using Moudle.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Common
{
    public class WebContext
    {
        private const string ENC_USER_KEY = "SH723HSDFUE88";

        private Account _currentUser = null;
        public Account CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    string key = "lastsslogid";
                    string strAccountID = GetEncryptedCookieValue(key, key);
                    long accountID = MyCommon.ToLong(strAccountID);
                    if (accountID > 0)
                    {
                        AccountService accService = new AccountService();
                        _currentUser = accService.GetAccountByID(accountID);
                        if (_currentUser != null)
                        {
                            // slide cookie expiration time
                            SetEncryptedCookie(key, strAccountID, DateTime.UtcNow.AddMinutes(30), key);
                        }
                    }
                }
                return _currentUser;
            }
            set
            {

            }
        }

        private void SetEncryptedCookie(string name, string value, DateTime expires, string key)
        {
            HttpCookie cookie = new HttpCookie(name, value.Encrypt(key));
            if (expires != DateTime.MinValue)
                cookie.Expires = expires;
            cookie.HttpOnly = true;
            //if (Configuration.EnableSSL)
            //    cookie.Secure = true;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        private string GetEncryptedCookieValue(string name, string key)
        {
            string result = null;
            try
            {
                if (HttpContext.Current.Request.Cookies[name] != null)
                    result = HttpContext.Current.Request.Cookies[name].Value.Decrypt(key);
            }
            catch (Exception) { }
            return result;
        }
    }
}
