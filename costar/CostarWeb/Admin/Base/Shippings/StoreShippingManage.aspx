<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="StoreShippingManage.aspx.cs" Inherits="CostarWeb.Admin.Base.Shipping.StoreShippingManage" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("textarea").autogrow();
        });
    </script>
    <style type="text/css">
        .auto-style1
        {
            width: 100px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <table class="linetable" width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <th style="font-size: x-large; width: 200px">配送方式管理
            </th>
            <td></td>
        </tr>
        <tr>
            <th style="font-size: initial; width: 200px"><a href="../Product/ProductList.aspx">返回至产品列表</a>
            </th>
            <td></td>
        </tr>
        <tr>
            <th>
                <asp:Button ID="btn_add" runat="server" Text="添加配送方式" OnClick="btn_add_Click" />
            </th>
            <td></td>
        </tr>
    </table>
    <div id="div_show" runat="server" style="display: block;">
        <table class="linetable" width="500px" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <th>配送公司
                </th>
                <td colspan="2">
                    <asp:TextBox ID="txt_Name" runat="server" />
                </td>
            </tr>
            <tr>
                <th>配送方式描述
                </th>
                <td colspan="2">
                    <asp:TextBox ID="txt_Description" runat="server" TextMode="MultiLine" Style="overflow: hidden; width: 300px; height: 100px" />
                </td>
            </tr>
            <tr>
                <th>配送价格
                </th>
                <td class="auto-style1">
                    <asp:TextBox ID="txt_Price" runat="server" MaxLength="10" />
                </td>
                <td>
                    <asp:DropDownList ID="ddl_Per" runat="server">
                        <asp:ListItem Value="0">每件</asp:ListItem>
                        <asp:ListItem Value="1">每单</asp:ListItem>
                        <asp:ListItem Value="2">每斤</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>激活
                </th>
                <td class="auto-style1">
                    <asp:CheckBox ID="CheckBox_Active" runat="server" Text="激活配送方式" />
                </td>
                <td></td>
            </tr>
            <tr>
                <th></th>
                <td class="auto-style1">
                    <asp:Button ID="btn_save" runat="server" Text="保存" OnClick="btn_save_Click" />
                </td>
                <td>
                    <asp:Button ID="btn_cancle" runat="server" Text="取消" OnClick="btn_cancle_Click" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td class="auto-style1"></td>
                <td></td>
            </tr>
        </table>
    </div>
    <div runat="server" id="div_detail">
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
                            <asp:Label ID="lbl_Name" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_Price" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                        </td>
                        <td>
                            <img src="<%# Eval("IsActive") %>" width="20" height="20" />
                        </td>
                        <td width="100" valign="center">
                            <span class="fontbtn">
                                <a style="cursor: pointer" href="StoreShippingManage.aspx?ShippingOptionID=<%# Eval("ShippingOptionID") %>&a=edit">[编辑]</a>
                            </span>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    <asp:HiddenField ID="HiddenField1" runat="server" />
</asp:Content>
