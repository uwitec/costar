<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ProductInventory.aspx.cs" Inherits="CostarWeb.Admin.Base.Product.ProductInventory" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
           

            //点击无属性显示数量，隐藏属性
            $("#<%=ProductNo.ClientID %>").click(function () {
                //$("#ProductNum").val("0");
                //$("#ddl_Vaiant1").val("0");
                //$("#ddl_Vaiant2").val("0");

                $("#<%=VaiantNo.ClientID %>").css("display", "block");
                $("#<%=VaiantYes.ClientID %>").css("display", "none");
                $("#<%=div_detail.ClientID %>").css("display", "none");
            });
            //点击有属性显示属性，隐藏数量
            $("#<%=ProductYes.ClientID %>").click(function () {
                //$("#ProductNum").val("0");
                //$("#ddl_Vaiant1").val("0");
                //$("#ddl_Vaiant2").val("0");

                $("#<%=VaiantNo.ClientID %>").css("display", "none");
                $("#<%=VaiantYes.ClientID %>").css("display", "block");
                $("#<%=div_detail.ClientID %>").css("display", "block");
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <table class="linetable" width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <th style="font-size: x-large; width: 200px">商品库存
            </th>
            <td></td>
        </tr>
        <tr>
            <th style="font-size: large; width: 200px">[<asp:Literal ID="lbl_ProductName" runat="server" Text="产品名称"></asp:Literal>]
            </th>
            <td></td>
        </tr>
        <tr>
            <th style="font-size: initial; width: 200px">
                <a href="ProductList.aspx">返回至产品列表</a>
            </th>
            <td></td>
        </tr>
        <tr>
            <th style="font-size: initial; width: 200px">产品属性
            </th>
            <td></td>
        </tr>
    </table>
    <div style="padding-top: 0;">
        <input runat="server" id="ProductNo" type="radio" name="radio_vaiant" />此产品无任何属性
    </div>
    <div id="VaiantNo" runat="server" style="display: none;">
        数量  
         <asp:TextBox ID="ProductNum" runat="server" MaxLength="10" CssClass="positive-decimal" />
    </div>
    <div>
        <input runat="server" id="ProductYes" type="radio" name="radio_vaiant" />此产品含有属性
    </div>
    <div id="VaiantYes" runat="server" style="display: none;">
        <div>
            属性1
            <asp:DropDownList ID="ddl_Vaiant1" runat="server"></asp:DropDownList>
            <%-- <select runat="server" id="ddl_Vaiant1" style="width: 100px"></select>--%>
        </div>
        <div>
            属性2    
            <asp:DropDownList ID="ddl_Vaiant2" runat="server"></asp:DropDownList>
            <%--<select runat="server" id="ddl_Vaiant2" style="width: 100px"></select>--%>
        </div>
    </div>
    <div style="padding-top: 0;">
        <asp:Button ID="btn_continue" runat="server" Text="继续" OnClick="btn_continue_Click" OnClientClick="return confirm('改变属性后将清除原属性库存，确定要继续吗？');" />
        <br />
        <br />
    </div>
    <div runat="server" id="div_detail" style="display: none;">
        添加
            <select runat="server" id="ddl_count" style="width: 60px">
                <option value="1">1</option>
                <option value="2">2</option>
                <option value="3">3</option>
                <option value="4">4</option>
                <option value="5">5</option>
            </select>
        条数据
            <asp:Button ID="btn_add" runat="server" Text="添加" OnClick="btn_add_Click" />
        <br />
        (<asp:Literal ID="lbl_ProductName1" runat="server" Text="产品名称"></asp:Literal>)库存
            <table class="listtable" width="800px" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <th id="th1" runat="server" style="display: none;"><%=_type1%>
                    </th>
                    <th id="th2" runat="server" style="display: none;"><%=_type2%>
                    </th>
                    <th>库存
                    </th>
                    <th>已售出
                    </th>
                    <th>在购物车数量
                    </th>
                    <th>排序
                    </th>
                    <th>删除
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt_list" OnItemDataBound="rpt_list_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td nowrap="nowrap" id="td1" runat="server" style="display: none;">
                                <asp:DropDownList ID="ddl_SVTO1" runat="server" DataTextField="txt" DataValueField="val"></asp:DropDownList>
                            </td>
                            <td nowrap="nowrap" id="td2" runat="server" style="display: none;">
                                <asp:DropDownList ID="ddl_SVTO2" runat="server" DataTextField="txt" DataValueField="val"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_QtyAvail" runat="server" Text='<%#Eval("QtyAvail") %>' CssClass="positive-decimal"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lbl_QtySold" runat="server" Text='<%# Eval("QtySold") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbl_QtyOnHold" runat="server" Text='<%# Eval("QtyOnHold") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_SortOrder" runat="server" Text='<%#Eval("SortOrder") %>' CssClass="positive-decimal"></asp:TextBox>
                            </td>
                            <td width="100" valign="center">
                                <span class="fontbtn">
                                    <a style="cursor: pointer" href="ProductInventory.aspx?ProductID=<%# Eval("ProductID") %>&InventoryID=<%# Eval("InventoryID") %>&a=del">[删除]</a>
                                </span>
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenField_InventoryID" runat="server" Value='<%#Eval("InventoryID") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        <asp:Button ID="btn_save" runat="server" Text="保存" OnClick="btn_save_Click" />
        <span class="fontbtn"><a href="ProductList.aspx">取消当前编辑</a> </span>
    </div>
    <asp:HiddenField ID="HiddenField_proId" runat="server" />
</asp:Content>
