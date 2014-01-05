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

            _OnPageLoad();
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
        protected void _OnPageContinue(int prodId)
        {
            using (LinqDataContext linq = new LinqDataContext())
            {
                StoreProductInventory inv = new StoreProductInventory();

                inv.ProductID = prodId;
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
                    var q = linq.StoreProducts.Where(c => c.ProductID == prodId).SingleOrDefault();

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

                    linq.SubmitChanges();

                    div_detail.Style.Value = "display: block;";

                    BindRepeater(prodId);
                }
            }
        }

        protected void BindRepeater(int prodId)
        {
            using (LinqDataContext linq = new LinqDataContext())
            {
                var q = linq.StoreProductInventories.Where(c => c.ProductID == prodId).Select(c => new
                {
                    QtySold = c.QtySold,
                    ProductID = prodId
                });

                rpt_list.DataSource = q;
                rpt_list.DataBind();
            }
        }

        protected void BindSVTO1()
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

        protected void rpt_list_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int Variant1 = MyCommon.ToInt(Request["ddl_Vaiant1"]);
            int Variant2 = MyCommon.ToInt(Request["ddl_Vaiant2"]);

            using (LinqDataContext linq = new LinqDataContext())
            {
                if (Variant1 != 0)
                {
                    DropDownList ddl = (DropDownList)e.Item.FindControl("ddl_SVTO1");
                    ddl.DataSource = linq.StoreVariantTypeOptions.Where(c => c.VariantTypeID == Variant1).Select(c => new { val = c.VariantOptionID, txt = c.Name });
                    ddl.DataTextField = "txt";
                    ddl.DataValueField = "val";
                    ddl.DataBind();

                    ((System.Web.UI.HtmlControls.HtmlTableCell)(e.Item.FindControl("td1"))).Style.Value = "display: block;";
                }
                if (Variant2 != 0)
                {
                    DropDownList ddl = (DropDownList)e.Item.FindControl("ddl_SVTO2");
                    ddl.DataSource = linq.StoreVariantTypeOptions.Where(c => c.VariantTypeID == Variant2).Select(c => new { val = c.VariantOptionID, txt = c.Name });
                    ddl.DataTextField = "txt";
                    ddl.DataValueField = "val";
                    ddl.DataBind();

                    ((System.Web.UI.HtmlControls.HtmlTableCell)(e.Item.FindControl("td2"))).Style.Value = "display: block;";
                }
            }
        }

        protected void btn_continue_Click(object sender, EventArgs e)
        {
            _OnPageContinue(_productID);
        }
    }
}