<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoreShippingManage.aspx.cs" Inherits="CostarWeb.Admin.Base.Shipping.StoreShippingManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <span style="font-size: x-large">配送方式管理</span>
        </div>
        <div>
            <a href="../Product/ProductList.aspx">返回至产品列表</a>
        </div>
        <div>
            <asp:Button ID="btn_add" runat="server" Text="添加配送方式" />
        </div>
        <div>
            配送公司<asp:TextBox ID="txt_Name" runat="server" />
        </div>
        <div>
            配送方式描述           
            <asp:TextBox ID="txt_Description" runat="server" TextMode="MultiLine" style="overflow: hidden; width: 300px; height: 100px" />
        </div>
        <div>
            配送价格           
           <asp:TextBox ID="txt_" runat="server" MaxLength="10" />
            <asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem Value="0">每件</asp:ListItem>
                <asp:ListItem Value="1">每单</asp:ListItem>
                <asp:ListItem Value="2">每斤</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div>
            激活           
            <asp:CheckBox ID="CheckBox_Active" runat="server" Text="激活配送方式" />
        </div>

        <div runat="server" id="div_detail" style="display: none;">
            <table class="listtable" width="800px" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <th>配送公司
                    </th>
                    <th>价格
                    </th>
                    <th>是否激活
                    </th>
                    <th>编辑
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt_list">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:TextBox ID="txt_QtyAvail" runat="server" Text='<%#Eval("QtyAvail") %>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lbl_QtySold" runat="server" Text='<%# Eval("QtySold") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbl_QtyOnHold" runat="server" Text='<%# Eval("QtyOnHold") %>'></asp:Label>
                            </td>
                            <td width="100" valign="center">
                                <span class="fontbtn">
                                    <a style="cursor: pointer" href="ProductInventory.aspx?InventoryID=<%# Eval("InventoryID") %>&a=edite">[编辑]</a>
                                </span>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </form>
</body>
</html>
