using Common;
using Moudle;
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
        protected string _type1 = "属性1";
        protected string _type2 = "属性2";
        private int _productID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            _productID = MyCommon.ToInt(Request["ProductID"]);
            int InventoryID = MyCommon.ToInt(Request["InventoryID"]);
            string a = Request["a"];

            if (!IsPostBack)
            {
                if (a == "edit")
                {
                    _OnPageLoadEdit(_productID);
                }
                if (a == "del")
                {
                    _OnPageDel(InventoryID);
                }
                _OnPageLoad();
            }
        }

        protected void _OnPageDel(int InventoryID)
        {
            using (LinqDataContext linq = new LinqDataContext())
            {
                linq.StoreProductInventories.DeleteAllOnSubmit(linq.StoreProductInventories.Where(c => c.InventoryID == InventoryID));
                linq.SubmitChanges();
                Response.Redirect(Request.UrlReferrer.ToString());
            }
        }

        protected void _OnPageLoad()
        {
            using (LinqDataContext linq = new LinqDataContext())
            {
                //Vaiant1
                ddl_Vaiant1.DataSource = linq.StoreVariantTypes.Select(c => new { val = c.VariantTypeID, txt = c.GroupName });
                ddl_Vaiant1.DataTextField = "txt";
                ddl_Vaiant1.DataValueField = "val";
                ddl_Vaiant1.DataBind();

                //Vaiant2
                ddl_Vaiant2.DataSource = linq.StoreVariantTypes.Select(c => new { val = c.VariantTypeID, txt = c.GroupName });
                ddl_Vaiant2.DataTextField = "txt";
                ddl_Vaiant2.DataValueField = "val";
                ddl_Vaiant2.DataBind();
            }
        }

        protected void _OnPageLoadEdit(int productID)
        {
            using (LinqDataContext linq = new LinqDataContext())
            {
                var q = linq.StoreProducts.Where(c => c.ProductID == _productID).SingleOrDefault();
                int Variant1 = MyCommon.ToInt(q.Variant1TypeID.ToString());
                int Variant2 = MyCommon.ToInt(q.Variant2TypeID.ToString());

                if (Variant1 > 0 && Variant2 > 0)
                {
                    BindRepeater(productID);
                }
            }
        }

        protected void ShowVariant(int Variant1, int Variant2)
        {
            using (LinqDataContext linq = new LinqDataContext())
            {
                var q = linq.StoreProducts.Where(c => c.ProductID == _productID).SingleOrDefault();

                if (Variant1 != 0)
                {
                    var svt1 = linq.StoreVariantTypes.Where(c => c.VariantTypeID == Variant1).SingleOrDefault();
                    _type1 = svt1.GroupName;
                    q.Variant1TypeID = Variant1;
                    th1.Style.Value = "display: block;";
                }
                if (Variant2 != 0)
                {
                    var svt2 = linq.StoreVariantTypes.Where(c => c.VariantTypeID == Variant2).SingleOrDefault();
                    _type2 = svt2.GroupName;
                    q.Variant2TypeID = Variant2;
                    th2.Style.Value = "display: block;";
                }

                div_detail.Style.Value = "display: block;";
            }
        }

        protected void BindRepeater(int prodId)
        {
            using (LinqDataContext linq = new LinqDataContext())
            {
                var q = linq.StoreProductInventories.Where(c => c.ProductID == prodId).Select(c => new
                {
                    InventoryID = c.InventoryID,
                    QtySold = c.QtySold,
                    QtyAvail = c.QtyAvail,
                    SortOrder =c.SortOrder
                });

                rpt_list.DataSource = q;
                rpt_list.DataBind();
            }
        }

        protected void rpt_list_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            using (LinqDataContext linq = new LinqDataContext())
            {
                var q = linq.StoreProducts.Where(c => c.ProductID == _productID).SingleOrDefault();
                int Variant1 = MyCommon.ToInt(q.Variant1TypeID.ToString());
                int Variant2 = MyCommon.ToInt(q.Variant2TypeID.ToString());
                ShowVariant(Variant1, Variant2);

                if (Variant1 != 0)
                {
                    DropDownList ddl = (DropDownList)e.Item.FindControl("ddl_SVTO1");
                    ddl.DataSource = linq.StoreVariantTypeOptions.Where(c => c.VariantTypeID == Variant1).Select(c => new { val = c.VariantOptionID, txt = c.Name });
                    ddl.DataTextField = "txt";
                    ddl.DataValueField = "val";
                    ddl.DataBind();

                    HiddenField invId = (HiddenField)e.Item.FindControl("HiddenField_InventoryID");
                    StoreProductInventory inv = linq.StoreProductInventories.Where(c => c.InventoryID == MyCommon.ToInt(invId.Value)).SingleOrDefault();
                    ddl.SelectedValue = inv.Variant1OptionID.ToString();

                    ((System.Web.UI.HtmlControls.HtmlTableCell)(e.Item.FindControl("td1"))).Style.Value = "display: block;";
                }
                if (Variant2 != 0)
                {
                    DropDownList ddl = (DropDownList)e.Item.FindControl("ddl_SVTO2");
                    ddl.DataSource = linq.StoreVariantTypeOptions.Where(c => c.VariantTypeID == Variant2).Select(c => new { val = c.VariantOptionID, txt = c.Name });
                    ddl.DataTextField = "txt";
                    ddl.DataValueField = "val";
                    ddl.DataBind();
                    ddl.SelectedValue = Variant2.ToString();

                    HiddenField invId = (HiddenField)e.Item.FindControl("HiddenField_InventoryID");
                    StoreProductInventory inv = linq.StoreProductInventories.Where(c => c.InventoryID == MyCommon.ToInt(invId.Value)).SingleOrDefault();
                    ddl.SelectedValue = inv.Variant2OptionID.ToString();

                    ((System.Web.UI.HtmlControls.HtmlTableCell)(e.Item.FindControl("td2"))).Style.Value = "display: block;";
                }
            }
        }

        protected void btn_continue_Click(object sender, EventArgs e)
        {
            using (LinqDataContext linq = new LinqDataContext())
            {
                StoreProductInventory inv = new StoreProductInventory();
                inv.ProductID = _productID;
                inv.QtyAvail = 0;
                inv.QtySold = 0;
                inv.QtyOnHold = 0;
                inv.SortOrder = 0;
                if (Request["radio_vaiant"] == "ProductNo") inv.QtyAvail = MyCommon.ToInt(Request["ProductNum"]);
                linq.StoreProductInventories.InsertOnSubmit(inv);
                linq.SubmitChanges();

                if (Request["radio_vaiant"] == "ProductYes")
                {
                    int Variant1 = MyCommon.ToInt(Request["ddl_Vaiant1"]);
                    int Variant2 = MyCommon.ToInt(Request["ddl_Vaiant2"]);

                    if (Variant1 == 0 && Variant2 == 0)
                    {
                        MyCommon.Alert("请选择属性.");
                        return;
                    }

                    StoreProduct product = linq.StoreProducts.Where(c => c.ProductID == _productID).SingleOrDefault();
                    if (Variant1 == 0) product.Variant1TypeID = null;
                    else product.Variant1TypeID = Variant1;
                    if (Variant2 == 0) product.Variant2TypeID = null;
                    else product.Variant2TypeID = Variant2;
                    linq.SubmitChanges();

                    ShowVariant(Variant1, Variant2);
                    BindRepeater(_productID);
                }
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            int addcount = MyCommon.ToInt(Request["ddl_count"]);

            using (LinqDataContext linq = new LinqDataContext())
            {
                for (int i = 0; i < addcount; i++)
                {
                    StoreProductInventory inv = new StoreProductInventory();

                    inv.ProductID = _productID;
                    inv.QtyAvail = 0;
                    inv.QtySold = 0;
                    inv.QtyOnHold = 0;
                    inv.SortOrder = 0;
                    inv.QtyAvail = 0;

                    linq.StoreProductInventories.InsertOnSubmit(inv);
                    linq.SubmitChanges();
                }
            }

            BindRepeater(_productID);
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (rpt_list.Items.Count == 0)
            {
                MyCommon.Alert("无数据无法保存.");
                return;
            }

            using (LinqDataContext linq = new LinqDataContext())
            {
                foreach (RepeaterItem item in rpt_list.Items)
                {
                    HiddenField invId = (HiddenField)item.FindControl("HiddenField_InventoryID");
                    StoreProductInventory inv = linq.StoreProductInventories.Where(c => c.InventoryID == MyCommon.ToInt(invId.Value)).SingleOrDefault();

                    DropDownList SVTO1 = (DropDownList)item.FindControl("ddl_SVTO1");
                    inv.Variant1OptionID = MyCommon.ToInt(SVTO1.SelectedItem.Value);

                    DropDownList SVTO2 = (DropDownList)item.FindControl("ddl_SVTO2");
                    inv.Variant2OptionID = MyCommon.ToInt(SVTO2.SelectedItem.Value);

                    TextBox QtyAvail = (TextBox)item.FindControl("txt_QtyAvail");
                    inv.QtyAvail = MyCommon.ToInt(QtyAvail.Text);

                    TextBox SortOrder = (TextBox)item.FindControl("txt_SortOrder");
                    inv.SortOrder = MyCommon.ToInt(SortOrder.Text);

                    linq.SubmitChanges();
                }

                MyCommon.Alert("保存成功.");
            }
        }

    }
}