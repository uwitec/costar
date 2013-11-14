using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Web
{
  public class Global : System.Web.HttpApplication
  {

    protected void Application_Start(object sender, EventArgs e)
    {

    }

    protected void Session_Start(object sender, EventArgs e)
    {

    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {

    }

    protected void Application_AuthenticateRequest(object sender, EventArgs e)
    {
      if (HttpContext.Current.User != null)//如果当前的http信息中存在用户信息
      {
        if (HttpContext.Current.User.Identity.IsAuthenticated)//如果当前用户的身份已经通过了验证
        {
          if (HttpContext.Current.User.Identity is FormsIdentity)
          {
            //如果当前用户身份是FormsIdentity类即窗体验证类,此类有个属性能够访问当前用户的验证票
            FormsIdentity fi = (FormsIdentity)HttpContext.Current.User.Identity;//创建个FormsIdentity类,用他来访问当前用户的验证票
            //获得用户的验证票
            FormsAuthenticationTicket ticket = fi.Ticket;
            //从验证票中获得用户数据也就是角色数据
            //string userData = ticket.UserData;
            //把用户数据用,分解成角色数组(从验证票中获得用户数据也就是角色数据)
            string[] roles = ticket.UserData.Split(',');
            //重写当前用户信息,就是把角色信息也加入到用户信息中
            HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(fi, roles); //这样当前用户就拥有角色信息了
          }
        }
      }
    }

    protected void Application_Error(object sender, EventArgs e)
    {

    }

    protected void Session_End(object sender, EventArgs e)
    {

    }

    protected void Application_End(object sender, EventArgs e)
    {

    }
  }
}