<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="CostarWeb.Admin.Base.Product.ProductList" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.Master" %>

<%@ Register Src="~/UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc_Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div>
        <div class="user_setting" style="padding-top: 0;">
            <dl class="usertopbar clearfix">
                <dt class="btns_box">
                    <input name="" type="button" value="新加产品" onclick="window.location.href = 'EditProduct.aspx'" />
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
</asp:Content>
