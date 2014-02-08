<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pager.ascx.cs" Inherits="CostarWeb.UserControl.Pager" %>
<div class="pager">
  <%if (PageCount > 0)
    { %>
  <%if (PageIndex == 1)
    {
  %>
  <a class="no_link">&lt;</a>
  <%
    }
    else
    {
  %>
  <a href="<% = PageLinkLast %>">&lt;</a>
  <%
    } %>
  <%if (PageIndex >= LeftNum + 2)
    {
      int _i;
      if (PageIndex == LeftNum + 2) _i = 1; else _i = 2;
      for (int i = 1; i <= _i; i++)
      {
  %>
  <a href="<%=LinkName %>page=<%= i%>">
    <% =i%></a>
  <%
}
  %>
  <a class="no_link">...</a>
  <%
    for (int i = LeftNum; i >= 1; i--)
    {
  %>
  <a href="<%=LinkName %>page=<%= PageIndex-i%>">
    <% =PageIndex - i%></a>
  <%
    }
    }
    else
    {

      for (int i = PageIndex - 1; i >= 1; i--)
      {
  %>
  <a href="<%=LinkName %>page=<%= PageIndex-i%>">
    <% =PageIndex - i%></a>
  <%
    }
    }
  %>
  <a class="justone">
    <%=PageIndex %></a>
  <%if (PageCount - PageIndex >= RightNum + 1)
    { %>
  <%
    for (int i = 1; i <= RightNum; i++)
    {
  %>
  <a href="<%=LinkName %>page=<%= PageIndex+i%>">
    <% =PageIndex + i%></a>
  <%
    }
  %>
  <a class="no_link">...</a>
  <%
    int _i;
    if (PageCount - PageIndex == RightNum + 1) _i = 0; else _i = 1;
    for (int i = _i; i >= 0; i--)
    {
  %>
  <a href="<%=LinkName %>page=<%= PageCount -i %>">
    <% =PageCount - i%></a>
  <%
    }
    }
    else
    {
      for (int i = PageCount - PageIndex - 1; i >= 0; i--)
      {
  %>
  <a href="<%=LinkName %>page=<%= PageCount -i %>">
    <% =PageCount - i%></a>
  <%
    }
    } %>
  <%
    //右侧下一页
    if (PageIndex == PageCount)
    {
  %>
  <a class="no_link">&gt;</a>
  <%
    }
    else
    {
  %>
  <a href="<% = PageLinkNext %>">&gt;</a>
  <%
    } %>
  <!--
  <form class="formPage" onsubmit="location.href='<%=LinkName %>page='+document.getElementById('page').value;return false;">
  <input type="text" class="inputPage" name="page" id="page" />
  <input type="submit" value="Go" />
  </form>
  -->
  <%} %>
</div>
