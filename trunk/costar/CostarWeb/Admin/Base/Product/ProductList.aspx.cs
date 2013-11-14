using Common;
using Moudle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CostarWeb.Admin.Base.Product
{
    public partial class ProductList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int pId = MyCommon.ToInt(Request["ProductID"]);

            switch (Request["a"])
            {
                case "delete":
                    _OnDelete(pId);
                    break;
                default:
                    _OnPageLoad();
                    break;
            }
        }

        protected void _OnPageLoad()
        {
            using (LinqDataContext linq = new LinqDataContext())
            {
                ddl_StoreAnimes.DataSource = linq.StoreAnimes.Select(c => new { val = c.AnimeID, txt = c.AnimeName });
                ddl_StoreAnimes.DataTextField = "txt";
                ddl_StoreAnimes.DataValueField = "val";
                ddl_StoreAnimes.DataBind();
            }
        }
        protected void _OnDelete(int ProductID)
        {
            using (LinqDataContext linq = new LinqDataContext())
            {
                linq.StoreProductColors.DeleteAllOnSubmit(linq.StoreProductColors.Where(c => c.ProductID == ProductID));
                linq.StoreProducts.DeleteOnSubmit(linq.StoreProducts.Where(c => c.ProductID == ProductID).Single());
                linq.SubmitChanges();

                Response.Redirect(Request.UrlReferrer.ToString());
            }
        }
    }
}