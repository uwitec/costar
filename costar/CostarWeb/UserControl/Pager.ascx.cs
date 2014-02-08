using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CostarWeb.UserControl
{
  public partial class Pager : System.Web.UI.UserControl
  {
    protected string LinkName, PageLinkLast, PageLinkNext;
    private int _pageCount, _pageIndex, _recordsCount, _preCount, _pageSkip;
    protected int LeftNum = 3, RightNum = 3; //中部分页,左侧5页,右侧5页

    /// <summary>
    /// 获取或设置跳过记录条数(LinqToSql skip)
    /// </summary>
    public int PageSkip
    {
      get { return _pageSkip; }
    }

    /// <summary>
    /// 获取或设置总页数
    /// </summary>
    public int PageCount
    {
      get { return _pageCount; }
    }

    /// <summary>
    /// 获取或设置每页条数
    /// </summary>
    public int PreCount
    {
      get { return _preCount; }
      set { _preCount = value; }
    }

    /// <summary>
    /// 获取或设置当前页码
    /// </summary>
    public int PageIndex
    {
      get { return _pageIndex; }
      set
      {
        _pageIndex = value;
        if (_pageIndex <= 1) _pageIndex = 1;
        _pageSkip = (_pageIndex - 1) * _preCount;
      }
    }

    /// <summary>
    /// 获取或设置当前记录数
    /// </summary>
    public int RecordsCount
    {
      get { return _recordsCount; }
      set { _recordsCount = value; }
    }

    protected string PageLink(string link)
    {
      LinkName = Request.RawUrl;
      string _linkAnd = string.Empty;

      if (LinkName.Contains("page="))
      {
        LinkName = LinkName.Substring(0, LinkName.IndexOf("page=") - 1);
      }
      if (LinkName.Contains("=")) _linkAnd = "&"; else _linkAnd = "?";
      LinkName = LinkName + _linkAnd;

      return LinkName;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (_preCount > 0)
        _pageCount = (_recordsCount % _preCount > 0) ? _recordsCount / _preCount + 1 : _recordsCount / _preCount;
      //Response.Write(_preCount + "<br />");
      //Response.Write(_recordsCount + "<br />");
      //Response.Write(_recordsCount % _preCount + "<br />");
      //Response.Write(_recordsCount / _preCount + 1 + "<br />");
      //Response.Write(_recordsCount / _preCount + "<br />");
      //Response.Write(_pageCount + "<br />");

      LinkName = Request.RawUrl;
      string _linkAnd = string.Empty;

      if (LinkName.Contains("page="))
      {
        LinkName = LinkName.Substring(0, LinkName.IndexOf("page=") - 1);
      }
      if (LinkName.Contains("=")) _linkAnd = "&"; else _linkAnd = "?";
      LinkName = LinkName + _linkAnd;

      PageLinkLast = LinkName + "page=" + (_pageIndex - 1);
      PageLinkNext = LinkName + "page=" + (_pageIndex + 1);

      //if (_recordsCount > 0)
      //{
      //  ltl_recordsCount.Visible = true;
      //  ltl_recordsCount.Text = "共" + _recordsCount + "条";
      //}
    }
  }
}