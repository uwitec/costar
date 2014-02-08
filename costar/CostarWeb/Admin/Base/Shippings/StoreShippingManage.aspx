<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="StoreShippingManage.aspx.cs" Inherits="CostarWeb.Admin.Base.Shipping.StoreShippingManage" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="jquery.autogrow.textarea.js"></script>
    <script type="text/javascript">
        $(function () {
            $("textarea").autogrow();
        });

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div>
        <span style="font-size: x-large">配送方式管理</span>
    </div>
    <div>
        <a href="../Product/ProductList.aspx">返回至产品列表</a>
    </div>
    <div>
        <asp:Button ID="btn_add" runat="server" Text="添加配送方式" OnClick="btn_add_Click" />
    </div>
    <div id="div_show" runat="server" style="display: none;">
        <div>
            配送公司<asp:TextBox ID="txt_Name" runat="server" />
        </div>
        <div>
            配送方式描述           
            <asp:TextBox ID="txt_Description" runat="server" TextMode="MultiLine" Style="overflow: hidden; width: 300px; height: 100px" />
        </div>
        <div>
            配送价格           
           <asp:TextBox ID="txt_Price" runat="server" MaxLength="10" />
            <asp:DropDownList ID="ddl_Per" runat="server">
                <asp:ListItem Value="0">每件</asp:ListItem>
                <asp:ListItem Value="1">每单</asp:ListItem>
                <asp:ListItem Value="2">每斤</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div>
            激活           
            <asp:CheckBox ID="CheckBox_Active" runat="server" Text="激活配送方式" />
        </div>
        <div>
            <asp:Button ID="btn_save" runat="server" Text="保存" OnClick="btn_save_Click" />
            <asp:Button ID="btn_cancle" runat="server" Text="取消" OnClick="btn_cancle_Click" />
            <br />
            <br />
            <br />
            <br />
            <br />
        </div>
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
