<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductEdit.aspx.cs" Inherits="CostarWeb.Admin.Base.Product.ProductEdit" %>

<!DOCTYPE html>

<html>
<head>
    <link rel="stylesheet" type="text/css" href="/css/common.css" />
    <link rel="stylesheet" type="text/css" href="../../../js/easyvalidator/css/validate.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../../../js/jquery.js"></script>
    <script src="/js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="http://cdn.jquerytools.org/1.2.7/full/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../../../js/easyvalidator/js/easy_validator.pack.js" type="text/javascript"></script>
    <script src="../../../js/jquery.form.js" type="text/javascript"></script>

    <script src="jquery.autogrow.textarea.js"></script>
    <script type="text/javascript">
        $(function () {
            //textarea
            $("textarea").autogrow();

            $("#ddl_StoreAnimes").change(function () {
                $("#AnimeID").val($("#ddl_StoreAnimes").val());
            });

            if ($("#img1").attr("src") != null)
                $("#imgPath1").css("display", "block");
            if ($("#img2").attr("src") != null)
                $("#imgPath2").css("display", "block");
            if ($("#img3").attr("src") != null)
                $("#imgPath3").css("display", "block");
            if ($("#img4").attr("src") != null)
                $("#imgPath4").css("display", "block");

            //upload
            $("#btn_upload1").click(function () {
                $("#uploadhidden1").attr("name", "b");
                //$("#AnimeID").val($("#ddl_StoreAnimes").val());
                if (validateform($("#ProductForm"))) {
                    $('#ProductForm').ajaxSubmit(function (data) {
                        if (data == "err")
                            return;
                        //显示缩略图
                        $("#imgPath1").css("display", "block");
                        data = data.split("|");
                        $("#img1").attr("src", data[0]);
                        //存放路径
                        $("#ImageFile1").val(data[0]);
                        //还原a
                        $("#uploadhidden1").attr("name", "a");
                    });
                }
            });
            //uploadDel
            $("#imgDel1").click(function () {
                $("#imgDelhidden1").attr("name", "b");
                $('#ProductForm').ajaxSubmit(function (data) {
                    $("#imgPath1").css("display", "none");
                    var obj = document.getElementById('file1');
                    obj.outerHTML = obj.outerHTML;
                    //还原a
                    $("#imgDelhidden1").attr("name", "a");
                });
            });

            //upload2
            $("#btn_upload2").click(function () {
                $("#uploadhidden2").attr("name", "b");
                //$("#AnimeID").val($("#ddl_StoreAnimes").val());
                if (validateform($("#ProductForm"))) {
                    $('#ProductForm').ajaxSubmit(function (data) {
                        if (data == "err")
                            return;
                        $("#imgPath2").css("display", "block");
                        data = data.split("|");
                        $("#img2").attr("src", data[0]);
                        $("#ImageFile2").val(data[0]);
                        $("#uploadhidden2").attr("name", "a");
                    });
                }
            });
            //uploadDel2
            $("#imgDel2").click(function () {
                $("#imgDelhidden2").attr("name", "b");
                $('#ProductForm').ajaxSubmit(function (data) {
                    $("#imgPath2").css("display", "none");
                    var obj = document.getElementById('file2');
                    obj.outerHTML = obj.outerHTML;
                    $("#imgDelhidden2").attr("name", "a");
                });
            });

            //upload3
            $("#btn_upload3").click(function () {
                $("#uploadhidden3").attr("name", "b");
                //$("#AnimeID").val($("#ddl_StoreAnimes").val());
                if (validateform($("#ProductForm"))) {
                    $('#ProductForm').ajaxSubmit(function (data) {
                        if (data == "err")
                            return;
                        //显示缩略图
                        $("#imgPath3").css("display", "block");
                        data = data.split("|");
                        $("#img3").attr("src", data[0]);
                        //存放路径
                        $("#ImageFile3").val(data[0]);
                        $("#uploadhidden3").attr("name", "a");
                    });
                }
            });
            //uploadDel3
            $("#imgDel3").click(function () {
                $("#imgDelhidden3").attr("name", "b");
                $('#ProductForm').ajaxSubmit(function (data) {
                    $("#imgPath3").css("display", "none");
                    var obj = document.getElementById('file3');
                    obj.outerHTML = obj.outerHTML;
                    $("#imgDelhidden3").attr("name", "a");
                });
            });

            //upload4
            $("#btn_upload4").click(function () {
                $("#uploadhidden4").attr("name", "b");
                //$("#AnimeID").val($("#ddl_StoreAnimes").val());
                if (validateform($("#ProductForm"))) {
                    $('#ProductForm').ajaxSubmit(function (data) {
                        if (data == "err")
                            return;
                        //显示缩略图
                        $("#imgPath4").css("display", "block");
                        data = data.split("|");
                        $("#img4").attr("src", data[0]);
                        //存放路径
                        $("#ImageFile4").val(data[0]);
                        $("#uploadhidden4").attr("name", "a");
                    });
                }
            });
            //uploadDel4
            $("#imgDel4").click(function () {
                $("#imgDelhidden4").attr("name", "b");
                $('#ProductForm').ajaxSubmit(function (data) {
                    $("#imgPath4").css("display", "none");
                    var obj = document.getElementById('file4');
                    obj.outerHTML = obj.outerHTML;
                    $("#imgDelhidden4").attr("name", "a");
                });
            });

            $("#btn_save").click(function () {
                $("#savehidden").attr("name", "b");
                $('#ProductForm').ajaxSubmit(function (data) {
                    if (data == "success") {
                        location.href = 'ProductList.aspx';
                    }
                });
            });

            //绑定评估类型多选
            var colors = "<%=_colors%>";
            for (var i = 0; i < colors.split(',').length; i++) {
                $("#color_" + colors.split(',')[i]).attr("checked", true);
            }
        });
    </script>
</head>
<body>
    <div>
        <div class="tabs" id="tp" runat="server">
            <a class="actived">产品管理</a>
            <a>订单管理</a>
            <a>配送方式管理</a>
            <a>问题管理</a>
            <a>活动管理</a>
            <a>报表</a>
        </div>
        <form action="ProductEdit.aspx" method="post" id="ProductForm" enctype="multipart/form-data">
            <div class="user_setting" style="padding-top: 0;">
                <table class="linetable" width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <th style="font-size: x-large">产品编辑
                        </th>
                        <td></td>
                    </tr>
                    <tr>
                        <th>产品名称
                        </th>
                        <td>
                            <input type="text" runat="server" id="ProductName" style="width: 300px" />
                        </td>
                    </tr>
                    <tr>
                        <th>产品简单描述
                        </th>
                        <td>
                            <input type="text" runat="server" id="Title" style="width: 300px" />
                        </td>
                    </tr>
                    <tr>
                        <th>产品代码
                        </th>
                        <td>
                            <input type="text" runat="server" id="ProductCode" style="width: 300px" />
                        </td>
                    </tr>
                    <tr>
                        <th>产品URL名称
                        </th>
                        <td>
                            <input type="text" runat="server" id="PageName" style="width: 300px" />
                        </td>
                    </tr>
                    <tr>
                        <th>产品内容描述
                        </th>
                        <td>
                            <textarea runat="server" id="Description" name="Description" style="overflow: hidden; width: 300px; height: 100px"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <th>产品颜色
                        </th>
                        <td>
                            <asp:Repeater runat="server" ID="rpt_color">
                                <ItemTemplate>
                                    <input type="checkbox" value="<%# Eval("ColorID") %>" name="color" id='color_<%# Eval("ColorID") %>' />
                                    <%# Eval("ColorName") %>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                    <tr>
                        <th>产品图片
                        </th>
                        <td>
                            <input type="file" name="file1" id="file1" style="width: 300px; height: 25px" />
                            <input type="button" value="上传" id="btn_upload1" />
                            <input type="hidden" id="uploadhidden1" name="a" value="upload1" />
                            <input runat="server" type="hidden" name="ImageFile1" id="ImageFile1" value="" />
                        </td>
                    </tr>
                    <tr id="imgPath1" runat="server" style="display: none">
                        <th></th>
                        <td>
                            <img runat="server" name="img1" id="img1" src="" height="200" width="200" />
                            <img name="imgDel1" id="imgDel1" src="../../../images/ico_error-24.png" height="20" width="20" style="cursor: pointer" />
                            <input type="hidden" id="imgDelhidden1" name="a" value="imgDel1" />
                        </td>
                    </tr>
                    <tr>
                        <th>更多产品图片
                        </th>
                        <td>
                            <input type="file" name="file2" id="file2" style="width: 300px; height: 25px" />
                            <input type="button" value="上传" id="btn_upload2" />
                            <input type="hidden" id="uploadhidden2" name="a" value="upload2" />
                            <input runat="server" type="hidden" name="ImageFile2" id="ImageFile2" value="" />
                        </td>
                    </tr>
                    <tr id="imgPath2" style="display: none">
                        <th></th>
                        <td>
                            <img runat="server" name="img2" id="img2" src="" height="200" width="200" />
                            <img name="imgDel2" id="imgDel2" src="../../../images/ico_error-24.png" height="20" width="20" style="cursor: pointer" />
                            <input type="hidden" id="imgDelhidden2" name="a" value="imgDel2" />
                        </td>
                    </tr>
                    <tr>
                        <th></th>
                        <td>
                            <input type="file" name="file3" id="file3" style="width: 300px; height: 25px" />
                            <input type="button" value="上传" id="btn_upload3" />
                            <input type="hidden" id="uploadhidden3" name="a" value="upload3" />
                            <input runat="server" type="hidden" name="ImageFile3" id="ImageFile3" value="" />
                        </td>
                    </tr>
                    <tr id="imgPath3" style="display: none">
                        <th></th>
                        <td>
                            <img runat="server" name="img3" id="img3" src="" height="200" width="200" />
                            <img name="imgDel3" id="imgDel3" src="../../../images/ico_error-24.png" height="20" width="20" style="cursor: pointer" />
                            <input type="hidden" id="imgDelhidden3" name="a" value="imgDel3" />
                        </td>
                    </tr>
                    <tr>
                        <th></th>
                        <td>
                            <input type="file" name="file4" id="file4" style="width: 300px; height: 25px" />
                            <input type="button" value="上传" id="btn_upload4" />
                            <input type="hidden" id="uploadhidden4" name="a" value="upload4" />
                            <input runat="server" type="hidden" name="ImageFile4" id="ImageFile4" value="" />
                        </td>
                    </tr>
                    <tr id="imgPath4" style="display: none">
                        <th></th>
                        <td>
                            <img runat="server" name="img4" id="img4" src="" height="200" width="200" />
                            <img name="imgDel4" id="imgDel4" src="../../../images/ico_error-24.png" height="20" width="20" style="cursor: pointer" />
                            <input type="hidden" id="imgDelhidden4" name="a" value="imgDel4" />
                        </td>
                    </tr>
                    <tr>
                        <th>产品所属动漫
                        </th>
                        <td>
                            <input runat="server" type="hidden" name="AnimeID" id="AnimeID" value="3" />
                            <select runat="server" id="ddl_StoreAnimes" style="width: 200px">
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <th>零售价格
                        </th>
                        <td>
                            <input type="text" runat="server" id="RetailPrice" style="width: 200px" />
                        </td>
                    </tr>
                    <tr>
                        <th>实际价格
                        </th>
                        <td>
                            <input type="text" runat="server" id="SalePrice" style="width: 200px" />
                        </td>
                    </tr>
                    <tr>
                        <th>VIP价格
                        </th>
                        <td>
                            <input type="text" runat="server" id="VIPPrice" style="width: 200px" />
                        </td>
                    </tr>
                    <tr>
                        <th>特别推荐
                        </th>
                        <td>
                            <input type="checkbox" runat="server" id="IsFeatured" />
                            <label for="IsFeatured">显示在推荐产品区域</label>

                        </td>
                    </tr>
                    <tr>
                        <th>发布上线
                        </th>
                        <td>
                            <input type="checkbox" runat="server" id="IsActive" />
                            <label for="IsActive">通过审阅，发布上线</label>
                        </td>
                    </tr>
                    <tr>
                        <th></th>
                        <td>
                            <input id="btn_save" type="button" value=" 保 存 " />
                            <input type="hidden" id="savehidden" name="a" value="submit" />
                            <input type="hidden" name="ProductID" value="<%=Request["ProductID"] %>" />
                            <span class="fontbtn"><a href="ProductList.aspx">取消当前编辑</a> </span>
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </div>
</body>
</html>
