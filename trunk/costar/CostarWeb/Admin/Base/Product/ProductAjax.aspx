<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductAjax.aspx.cs" Inherits="CostarWeb.Admin.Base.Product.ProductAjax" %>

<table class="listtable" width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <th width="100">预览图
        </th>
        <th>产品名称
        </th>
        <th>零售价格
        </th>
        <th>实际价格
        </th>
        <th>VIP价格
        </th>
        <th>卖出
        </th>
        <th>库存
        </th>
        <th>是否激活
        </th>
        <th width="150">操作
        </th>
    </tr>
    <asp:repeater runat="server" id="rpt_list">
     <ItemTemplate>
         <tr>
             <td width="150" nowrap="nowrap">
                  <img src="<%# Eval("img") %>" width="40" height="40" />
             </td>
             <td nowrap="nowrap">
                  <%# Eval("Name") %>
             </td>
             <td nowrap="nowrap">
                  <%# Eval("RetailPrice") %>
             </td>
             <td>
                  <%# Eval("SalePrice") %>
             </td>
             <td>
                  <%# Eval("VIPPrice") %>
             </td>
             <td>
                  <%# Eval("SellCount") %>
             </td>
             <td>
                  <%# Eval("WarHouseCount") %>
             </td>
             <td>
                  <img src="<%# Eval("IsActive") %>" width="20" height="20" />
             </td>
             <td width="100" valign="center">
                <span class="fontbtn">
                  <a style="cursor:pointer" href="ProductEdit.aspx?ProductID=<%# Eval("ProductID") %>&b=edit">[编辑]</a>
                  <a style="cursor:pointer"  href="ProductInventory.aspx?ProductID=<%# Eval("ProductID") %>">[库存]</a>
                  <a style="cursor:pointer" onclick="del(<%# Eval("ProductID") %>)">[删除]</a>
             </td>
         </tr>
      </ItemTemplate>
    </asp:repeater>
</table>
