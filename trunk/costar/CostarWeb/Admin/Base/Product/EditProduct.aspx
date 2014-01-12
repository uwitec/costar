<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs" Inherits="CostarWeb.Admin.Base.Product.EditProduct" %>
<%@ MasterType VirtualPath="~/Admin/AdminMaster.Master"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="jquery.autogrow.textarea.js"></script>
    <script type="text/javascript">
        $(function () {
            $("textarea").autogrow();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server"> 
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
                    <asp:TextBox ID="txtName" runat="server" MaxLength="100" />
                </td>
            </tr>
            <tr>
                <th>产品简单描述
                </th>
                <td>
                    <asp:TextBox ID="txtShortDescription" runat="server" MaxLength="100" />
                </td>
            </tr>
            <tr>
                <th>产品代码
                </th>
                <td>
                    <asp:TextBox ID="txtCode" runat="server" MaxLength="100" />
                </td>
            </tr>
            <tr>
                <th>产品URL名称
                </th>
                <td>
                    <asp:TextBox ID="txtUrl" runat="server" MaxLength="50" />
                </td>
            </tr>
            <tr>
                <th>产品内容描述
                </th>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" style="overflow: hidden; width: 300px; height: 100px" />
                </td>
            </tr>
            <tr>
                <th>产品颜色
                </th>
                <td>
                    <asp:CheckBoxList ID="chkColors" runat="server" RepeatDirection="Horizontal" RepeatColumns="4" />
                </td>
            </tr>
            <tr>
                <th>产品图片
                </th>
                <td>
                    <div><asp:Image ID="imgImg" runat="server" Width="200" /></div>
                    <div class="radio"><asp:RadioButton ID="radImgCur" runat="server" GroupName="grpimg" Checked="true" Text="Use current" /></div>
                    <div class="radio"><asp:RadioButton ID="radImgNew" runat="server" GroupName="grpimg" Text="Upload new" /></div>
                    <asp:FileUpload ID="fupFile" runat="server" />
                    <asp:RegularExpressionValidator ID="valFile" runat="server" CssClass="msg error" ForeColor=""
                        ControlToValidate="fupFile" Text="Only jpg file allowed" ValidationExpression="^.*\.(jpg|JPG|jpeg|JPEG)$"
                        ValidationGroup="vgEdit" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <th>更多产品图片
                </th>
                <td>
                    <asp:Panel ID="pnlImg2" runat="server"><asp:Image ID="img2" runat="server" Width="200" /> <asp:LinkButton ID="btnDel2" runat="server" Text="Delete" OnClick="btnDel2_Click" /></asp:Panel>
                    <asp:FileUpload ID="fup2" runat="server" />
                    <asp:RegularExpressionValidator ID="valFile2" runat="server" CssClass="msg error" ForeColor=""
                        ControlToValidate="fup2" Text="Only jpg files allowed" ValidationExpression="^.*\.(jpg|JPG|jpeg|JPEG)$"
                        ValidationGroup="vgEdit" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <th></th>
                <td>
                    <asp:Panel ID="pnlImg3" runat="server"><asp:Image ID="img3" runat="server" Width="200" /> <asp:LinkButton ID="btnDel3" runat="server" Text="Delete" OnClick="btnDel3_Click" /></asp:Panel>
                    <asp:FileUpload ID="fup3" runat="server" />
                    <asp:RegularExpressionValidator ID="valFile3" runat="server" CssClass="msg error" ForeColor=""
                        ControlToValidate="fup3" Text="Only jpg files allowed" ValidationExpression="^.*\.(jpg|JPG|jpeg|JPEG)$"
                        ValidationGroup="vgEdit" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <th></th>
                <td>
                    <asp:Panel ID="pnlImg4" runat="server"><asp:Image ID="img4" runat="server" Width="200" /> <asp:LinkButton ID="btnDel4" runat="server" Text="Delete" OnClick="btnDel4_Click" /></asp:Panel>
                    <asp:FileUpload ID="fup4" runat="server" />
                    <asp:RegularExpressionValidator ID="valFile4" runat="server" CssClass="msg error" ForeColor=""
                        ControlToValidate="fup4" Text="Only jpg files allowed" ValidationExpression="^.*\.(jpg|JPG|jpeg|JPEG)$"
                        ValidationGroup="vgEdit" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <th>产品所属动漫
                </th>
                <td>
                    <asp:DropDownList ID="ddlAnimes" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Value="0" Text="- 请选择 -" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>零售价格
                </th>
                <td>
                    <asp:TextBox ID="txtRetailPrice" runat="server" MaxLength="10" />
                </td>
            </tr>
            <tr>
                <th>实际价格
                </th>
                <td>
                    <asp:TextBox ID="txtSalePrice" runat="server" MaxLength="10" />
                </td>
            </tr>
            <tr>
                <th>VIP价格
                </th>
                <td>
                    <asp:TextBox ID="txtVipPrice" runat="server" MaxLength="10" />
                </td>
            </tr>
            <tr>
                <th>特别推荐
                </th>
                <td>
                    <asp:CheckBox ID="chkFeatured" runat="server" Text="显示在推荐产品区域" />

                </td>
            </tr>
            <tr>
                <th>发布上线
                </th>
                <td>
                    <asp:CheckBox ID="chkApproved" runat="server" Text="通过审阅，发布上线" />
                </td>
            </tr>
            <tr>
                <th></th>
                <td>
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text=" 保 存 " />
                    <span class="fontbtn"><a href="ProductList.aspx">取消当前编辑</a> </span>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
