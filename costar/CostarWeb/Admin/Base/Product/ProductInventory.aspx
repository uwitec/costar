<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductInventory.aspx.cs" Inherits="CostarWeb.Admin.Base.Product.ProductInventory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="/css/common.css" />
    <script src="/js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <title></title>
    <script type="text/javascript">
        $(function () {
            $("#ddl_Vaiant1").html("<option value=\"0\">--请选择--</option>" + $("#ddl_Vaiant1").html());
            $("#ddl_Vaiant2").html("<option value=\"0\">--请选择--</option>" + $("#ddl_Vaiant2").html());

            //点击无属性显示数量，隐藏属性
            $("#ProductNo").click(function () {
                $("#ProductNum").val("0");
                $("#ddl_Vaiant1").val("0");
                $("#ddl_Vaiant2").val("0");

                $("#VaiantNo").css("display", "block");
                $("#VaiantYes").css("display", "none");
            });
            //点击有属性显示属性，隐藏数量
            $("#ProductYes").click(function () {
                $("#ProductNum").val("0");
                $("#ddl_Vaiant1").val("0");
                $("#ddl_Vaiant2").val("0");

                $("#VaiantNo").css("display", "none");
                $("#VaiantYes").css("display", "block");
            });
        });
    </script>
</head>
<body>
    <form runat="server"><%--action="ProductInventory.aspx" method="post" id="ProductInv" enctype="multipart/form-data">--%>
        <div>
            <span style="font-size: x-large">商品库存</span>
        </div>
        <div>
            <asp:Literal ID="lbl_ProductName" runat="server" Text="产品名称"></asp:Literal>
        </div>
        <div>
            <a href="ProductList.aspx">返回至产品编辑列</a>
        </div>
        <div>
            产品属性
        </div>
        <div>
            <input runat="server" id="ProductNo" type="radio" name="radio_vaiant" />此产品无任何属性
        </div>
        <div id="VaiantNo" style="display: none;">
            数量  
            <input runat="server" id="ProductNum" type="text" style="width: 100px" />
        </div>
        <div>
            <input runat="server" id="ProductYes" type="radio" name="radio_vaiant" />此产品含有属性
        </div>
        <div id="VaiantYes" style="display: none;">
            <div>
                属性1
                <select runat="server" id="ddl_Vaiant1" style="width: 100px"></select>
            </div>
            <div>
                属性2    
                <select runat="server" id="ddl_Vaiant2" style="width: 100px"></select>
            </div>
        </div>
        <div>
            <asp:Button ID="btn_continue" runat="server" Text="继续" OnClick="btn_continue_Click" />
            <%--<input type="submit" name="btn_continue" value="继续" style="width: 100px" />
            <input type="hidden" id="continuehidden" name="a" value="continue" />
            <input type="hidden" name="ProductID" value="<%=Request["ProductID"] %>" />--%>
            <br />
            <br />
        </div>
        <div runat="server" id="div_detail" style="display: none;">
            添加
            <select runat="server" id="ddl_count" style="width: 60px">
                <option>1</option>
                <option>2</option>
                <option>3</option>
                <option>4</option>
                <option>5</option>
            </select>
            条数据
            <input type="submit" name="btn_add" value="添加" style="width: 60px" />
            <br />
            <asp:Literal ID="Literal1" runat="server" Text="(产品名称)"></asp:Literal>库存
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
                                <asp:TextBox ID="txt_QtyAvail" runat="server" Text="0"></asp:TextBox>
                            </td>
                            <td>
                                <%# Eval("QtySold") %>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_SortOrder" runat="server" Text="1"></asp:TextBox>
                            </td>
                            <td width="100" valign="center">
                                <span class="fontbtn">
                                    <a style="cursor: pointer" onclick="del(<%# Eval("ProductID") %>)">[删除]</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>

        </div>
    </form>
</body>
</html>
