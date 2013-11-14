<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="CostarWeb.Admin.Base.Product.ProductList" %>

<%@ Register Src="~/Admin/UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc_Page" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="/css/common.css" />
    <link rel="stylesheet" type="text/css" href="/css/Pager.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="http://cdn.jquerytools.org/1.2.7/full/jquery.tools.min.js"></script>
    <script src="/js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <title></title>
    <script type="text/javascript">
        $(function () {
            $("#ddl_StoreAnimes").html("<option value=\"0\">--全部动漫--</option>" + $("#ddl_StoreAnimes").html());
            $("#ddl_StoreAnimes").val("0");
            loadlist(0);
            $("#ddl_StoreAnimes").change(function () {
                loadlist($(this).val());
            });
        });

        function loadlist(id) {
            $.ajax({
                url: "ProductAjax.aspx?animeId=" + id + "&time=" + new Date().getTime(),
                success: function (data) {
                    $("#showlist").html(data);
                }
            });
        }

        function del(pid) {
            if (!confirm('确定要删除吗？'))
                return;
            $.ajax({
                url: "ProductList.aspx?ProductID=" + pid + "&a=delete&time=" + new Date().getTime(),
                success: function (data) {
                    loadlist($("#ddl_StoreAnimes").val());
                }
            });
        }

        function edit(pid) {
            alert(pid);
            $.ajax({
                url: "ProductEdit.aspx?ProductID=" + pid + "&time=" + new Date().getTime(),
                success: function (data) {
                    loadlist($("#ddl_StoreAnimes").val());
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="tabs" id="tp" runat="server">
                <a class="actived">产品管理</a>
                <a>订单管理</a>
                <a>配送方式管理</a>
                <a>问题管理</a>
                <a>活动管理</a>
                <a>报表</a>
            </div>

            <div class="user_setting" style="padding-top: 0;">
                <dl class="usertopbar clearfix">
                    <dt class="btns_box">
                        <input name="" type="button" value="新加产品" onclick="window.location.href = 'ProductEdit.aspx'" />
                        <input name="" type="button" value="查看已删除的产品" /></dt>
                </dl>
                <li>产品列表</li>
                <li>
                    <select runat="server" id="ddl_StoreAnimes" style="width: 100px">
                    </select>
                </li>
                <li>
                    <input name="" type="text" /></li>
                <li>
                    <input name="" type="button" value="搜索" class="btn_search" /></li>
                <div id="showlist">
                </div>
                <div class="pagectrl">
                    <uc_Page:Pager ID="Pager1" runat="server" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
