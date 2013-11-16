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
            //点击无属性显示数量，隐藏属性
            $("#ProductNo").click(function () {
                $("#VaiantNo").css("display", "block");
                $("#VaiantYes").css("display", "none");
            }); 
            //点击有属性显示属性，隐藏数量
            $("#ProductYes").click(function () {
                $("#VaiantNo").css("display", "none");
                $("#VaiantYes").css("display", "block");
            });
        });
    </script>
</head>
<body>
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
        <input id="ProductNo" type="radio" name="radio_vaiant" />此产品无任何属性
    </div>
    <div id="VaiantNo" style="display: none;">
        数量                 
        <input runat="server" id="ProductNum" type="text" style="width: 100px" />
    </div>
    <div>
        <input id="ProductYes" type="radio" name="radio_vaiant" />此产品含有属性
    </div>
    <div id="VaiantYes" style="display: none;">
        <div>
            属性1              
        <select id="ddl_Vaiant1" style="width: 100px"></select>
        </div>
        <div>
            属性2             
        <select id="ddl_Vaiant2" style="width: 100px"></select>
        </div>
    </div>
    <div>
        <input type="button" value="继续" style="width: 100px" />
    </div>
</body>
</html>
