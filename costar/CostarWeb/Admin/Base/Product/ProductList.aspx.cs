using Common;
using Moudle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Moudle.DataAccess;
using Moudle.DataAccess.Domain;

namespace CostarWeb.Admin.Base.Product
{
    public partial class ProductList : System.Web.UI.Page
    {
        private StoreAnimeRepository _anime = new StoreAnimeRepository();
        private StoreProductRepository _product = new StoreProductRepository();
        private StoreProductColorRepository _color = new StoreProductColorRepository();
        private StoreProductInventoyRepository _inventoy = new StoreProductInventoyRepository();

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
            _product.DelProduct(ProductID);
            _color.DelProductColor(ProductID);
            _inventoy.DelProductInventoryByProduct(ProductID);

            Response.Redirect(Request.UrlReferrer.ToString());
        }
    }
}