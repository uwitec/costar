using Common;
using Moudle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Moudle.DataAccess;

namespace CostarWeb.Admin.Base.Product
{
    public partial class ProductList : System.Web.UI.Page
    {
        private StoreAnimeRepository _anime = new StoreAnimeRepository();

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
            List<StoreAnime> anime = _anime.GetAllStoreAnimes();

            if (anime.Count > 0)
            {
                ddl_StoreAnimes.DataSource = anime;
                ddl_StoreAnimes.DataValueField = "AnimeID";
                ddl_StoreAnimes.DataTextField = "AnimeName";
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