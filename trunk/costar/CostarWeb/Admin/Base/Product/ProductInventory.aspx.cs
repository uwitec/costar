﻿using Common;
using Moudle;
using Moudle.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CostarWeb.Admin.Base.Product
{
    public partial class ProductInventory : System.Web.UI.Page
    {
        private StoreProductInventoyRepository _StoreProductInventoy = new StoreProductInventoyRepository();
        private StoreVariantTypeOptionsRepository _StoreVariantTypeOptions = new StoreVariantTypeOptionsRepository();
        private StoreVariantTypesRepository _StoreVariantTypes = new StoreVariantTypesRepository();
        private StoreProductRepository _StoreProduct = new StoreProductRepository();

        protected string _type1 = "属性1";
        protected string _type2 = "属性2";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int productID = MyCommon.ToInt(Request["ProductID"]);
                int InventoryID = MyCommon.ToInt(Request["InventoryID"]);
                string a = Request["a"];
                this.HiddenField_proId.Value = productID.ToString();

                _OnPageLoad(productID);

                if (a == "edit") _OnPageLoadEdit(productID);

                if (a == "del") _OnPageDel(InventoryID);
            }
        }

        protected void _OnPageLoad(int productID)
        {
            this.lbl_ProductName.Text = _StoreProduct.GetProductByID(productID).Name;
            this.lbl_ProductName1.Text = this.lbl_ProductName.Text;
            //Vaiant1
            ddl_Vaiant1.DataSource = _StoreVariantTypes.GetAllStoreVariantType();
            ddl_Vaiant1.DataTextField = "GroupName";
            ddl_Vaiant1.DataValueField = "VariantTypeID";
            ddl_Vaiant1.DataBind();

            //Vaiant2
            ddl_Vaiant2.DataSource = _StoreVariantTypes.GetAllStoreVariantType();
            ddl_Vaiant2.DataTextField = "GroupName";
            ddl_Vaiant2.DataValueField = "VariantTypeID";
            ddl_Vaiant2.DataBind();
        }

        protected void _OnPageLoadEdit(int productID)
        {
            int Variant1 = MyCommon.ToInt(_StoreProduct.GetProductByID(productID).Variant1TypeID.ToString());
            int Variant2 = MyCommon.ToInt(_StoreProduct.GetProductByID(productID).Variant2TypeID.ToString());

            //Have Variant
            if (Variant1 > 0 || Variant2 > 0)
            {
                BindRepeater(productID);
                this.ProductYes.Checked = true;
                this.VaiantYes.Style.Value = "display: block;";
                this.ddl_Vaiant1.Value = Variant1.ToString();
                this.ddl_Vaiant2.Value = Variant2.ToString();
            }
            else
            {
                ProductNum.Value = _StoreProductInventoy.QtyAvailByProduct(productID).ToString();
                this.ProductNo.Checked = true;
                this.VaiantNo.Style.Value = "display: block;";
            }
        }

        protected void _OnPageDel(int InventoryID)
        {
            _StoreProductInventoy.DelProductInventoy(InventoryID);
            Response.Redirect(Request.UrlReferrer.ToString());
        }

        protected void BindRepeater(int productId)
        {
            using (LinqDataContext linq = new LinqDataContext())
            {
                var q = linq.StoreProductInventories.Where(c => c.ProductID == productId).OrderBy(c => c.SortOrder).Select(c => new
                {
                    ProductID = c.ProductID,
                    InventoryID = c.InventoryID,
                    QtySold = c.QtySold,
                    QtyAvail = c.QtyAvail,
                    SortOrder = c.SortOrder,
                    QtyOnHold = c.QtyOnHold
                });

                rpt_list.DataSource = q;
                rpt_list.DataBind();
            }
        }

        protected void ShowVariant(int Variant1, int Variant2)
        {
            if (Variant1 != 0)
            {
                _type1 = _StoreVariantTypes.GetStoreVariantTypeByID(Variant1).GroupName;
                th1.Style.Value = "display: block;";
            }
            if (Variant2 != 0)
            {
                _type2 = _StoreVariantTypes.GetStoreVariantTypeByID(Variant2).GroupName;
                th2.Style.Value = "display: block;";
            }

            div_detail.Style.Value = "display: block;";
        }

        protected void rpt_list_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int Variant1 = MyCommon.ToInt(_StoreProduct.GetProductByID(MyCommon.ToLong(this.HiddenField_proId.Value)).Variant1TypeID.ToString());
            int Variant2 = MyCommon.ToInt(_StoreProduct.GetProductByID(MyCommon.ToLong(this.HiddenField_proId.Value)).Variant2TypeID.ToString());
            ShowVariant(Variant1, Variant2);

            if (Variant1 != 0)
            {
                DropDownList ddl = (DropDownList)e.Item.FindControl("ddl_SVTO1");
                ddl.DataSource = _StoreVariantTypeOptions.GetStoreVariantTypeOptionByVariantTypeID(Variant1);
                ddl.DataTextField = "Name";
                ddl.DataValueField = "VariantOptionID";
                ddl.DataBind();

                HiddenField invId = (HiddenField)e.Item.FindControl("HiddenField_InventoryID");
                ddl.SelectedValue = _StoreProductInventoy.GetInventoryByInventoryID(MyCommon.ToInt(invId.Value)).Variant1OptionID.ToString();

                ((System.Web.UI.HtmlControls.HtmlTableCell)(e.Item.FindControl("td1"))).Style.Value = "display: block;";
            }
            if (Variant2 != 0)
            {
                DropDownList ddl = (DropDownList)e.Item.FindControl("ddl_SVTO2");
                ddl.DataSource = _StoreVariantTypeOptions.GetStoreVariantTypeOptionByVariantTypeID(Variant2);
                ddl.DataTextField = "Name";
                ddl.DataValueField = "VariantOptionID";
                ddl.DataBind();

                HiddenField invId = (HiddenField)e.Item.FindControl("HiddenField_InventoryID");
                ddl.SelectedValue = _StoreProductInventoy.GetInventoryByInventoryID(MyCommon.ToInt(invId.Value)).Variant2OptionID.ToString();

                ((System.Web.UI.HtmlControls.HtmlTableCell)(e.Item.FindControl("td2"))).Style.Value = "display: block;";
            }
        }

        protected void btn_continue_Click(object sender, EventArgs e)
        {
            if (Request["radio_vaiant"] != "ProductNo" && Request["radio_vaiant"] != "ProductYes")
            {
                MyCommon.Alert("请选择产品属性.");
                return;
            }
            if (Request["radio_vaiant"] == "ProductYes")
            {
                int Variant1 = MyCommon.ToInt(Request["ddl_Vaiant1"]);
                int Variant2 = MyCommon.ToInt(Request["ddl_Vaiant2"]);

                if (Variant1 == 0 && Variant2 == 0)
                {
                    MyCommon.Alert("请选择属性.");
                    return;
                }
                if (Variant1 > 0)
                {
                    if (_StoreVariantTypeOptions.GetStoreVariantTypeOptionByVariantTypeID(Variant1).Count == 0)
                    {
                        string name = _StoreVariantTypes.GetStoreVariantTypeByID(Variant1).GroupName;
                        MyCommon.Alert("属性1中[" + name + "]无数据,请添加.");
                        VaiantNo.Style.Value = "display: none;";
                        VaiantYes.Style.Value = "display: block;";
                        return;
                    }
                }
                if (Variant2 > 0)
                {
                    if (_StoreVariantTypeOptions.GetStoreVariantTypeOptionByVariantTypeID(Variant2).Count == 0)
                    {
                        string name = _StoreVariantTypes.GetStoreVariantTypeByID(Variant2).GroupName;
                        MyCommon.Alert("属性2中[" + name + "]无数据,请添加.");
                        VaiantNo.Style.Value = "display: none;";
                        VaiantYes.Style.Value = "display: block;";
                        return;
                    }
                }
            }

            _StoreProductInventoy.DelProductInventoryByProduct(MyCommon.ToLong(this.HiddenField_proId.Value));

            StoreProductInventory inv = new StoreProductInventory();
            inv.ProductID = MyCommon.ToLong(this.HiddenField_proId.Value);
            inv.QtyAvail = 0;
            inv.QtySold = 0;
            inv.QtyOnHold = 0;
            inv.SortOrder = 0;

            if (Request["radio_vaiant"] == "ProductNo")
            {
                inv.QtyAvail = MyCommon.ToInt(Request["ProductNum"]);
                _StoreProductInventoy.SaveProductInventory(inv);

                using (LinqDataContext linq = new LinqDataContext())
                {
                    StoreProduct product = linq.StoreProducts.Where(p => p.ProductID == MyCommon.ToLong(this.HiddenField_proId.Value)).FirstOrDefault();
                    product.Variant1TypeID = null;
                    product.Variant2TypeID = null;
                    linq.SubmitChanges();

                    MyCommon.Alert("保存库存数量成功.", "ProductList.aspx");
                }
            }

            if (Request["radio_vaiant"] == "ProductYes")
            {
                _StoreProductInventoy.SaveProductInventory(inv);

                int Variant1 = MyCommon.ToInt(Request["ddl_Vaiant1"]);
                int Variant2 = MyCommon.ToInt(Request["ddl_Vaiant2"]);

                using (LinqDataContext linq = new LinqDataContext())
                {
                    StoreProduct product = linq.StoreProducts.Where(p => p.ProductID == MyCommon.ToLong(this.HiddenField_proId.Value)).FirstOrDefault();
                    if (Variant1 == 0) product.Variant1TypeID = null;
                    else product.Variant1TypeID = Variant1;
                    if (Variant2 == 0) product.Variant2TypeID = null;
                    else product.Variant2TypeID = Variant2;
                    linq.SubmitChanges();

                    ShowVariant(Variant1, Variant2);
                    BindRepeater(MyCommon.ToInt(this.HiddenField_proId.Value));

                    VaiantNo.Style.Value = "display: none;";
                    VaiantYes.Style.Value = "display: block;";
                    ProductNum.Value = "0";
                }
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            int addcount = MyCommon.ToInt(Request["ddl_count"]);

            for (int i = 0; i < addcount; i++)
            {
                StoreProductInventory inv = new StoreProductInventory();

                inv.ProductID = long.Parse(this.HiddenField_proId.Value);
                inv.QtyAvail = 0;
                inv.QtySold = 0;
                inv.QtyOnHold = 0;
                inv.SortOrder = 0;
                inv.QtyAvail = 0;

                _StoreProductInventoy.SaveProductInventory(inv);
            }

            BindRepeater(MyCommon.ToInt(this.HiddenField_proId.Value));
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (rpt_list.Items.Count == 0)
            {
                MyCommon.Alert("无数据无法保存.");
                return;
            }

            foreach (RepeaterItem item in rpt_list.Items)
            {
                HiddenField invId = (HiddenField)item.FindControl("HiddenField_InventoryID");
                StoreProductInventory inv = _StoreProductInventoy.GetInventoryByInventoryID(MyCommon.ToInt(invId.Value));

                DropDownList SVTO1 = (DropDownList)item.FindControl("ddl_SVTO1");
                if (SVTO1.SelectedItem != null)
                    inv.Variant1OptionID = MyCommon.ToInt(SVTO1.SelectedItem.Value);
                else
                    inv.Variant1OptionID = null;

                DropDownList SVTO2 = (DropDownList)item.FindControl("ddl_SVTO2");
                if (SVTO2.SelectedItem != null)
                    inv.Variant2OptionID = MyCommon.ToInt(SVTO2.SelectedItem.Value);
                else
                    inv.Variant2OptionID = null;

                TextBox QtyAvail = (TextBox)item.FindControl("txt_QtyAvail");
                inv.QtyAvail = MyCommon.ToInt(QtyAvail.Text);

                TextBox SortOrder = (TextBox)item.FindControl("txt_SortOrder");
                inv.SortOrder = MyCommon.ToInt(SortOrder.Text);

                Label QtySold = (Label)item.FindControl("lbl_QtySold");
                inv.QtySold = MyCommon.ToInt(QtySold.Text);

                Label QtyOnHold = (Label)item.FindControl("lbl_QtyOnHold");
                inv.QtyOnHold = MyCommon.ToInt(QtyOnHold.Text);

                _StoreProductInventoy.SaveProductInventory(inv);
            }

            MyCommon.Alert("保存成功.", "ProductList.aspx");
        }

    }
}