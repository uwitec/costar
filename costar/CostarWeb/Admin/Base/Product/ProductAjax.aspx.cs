﻿using Common;
using Moudle;
using Moudle.DataAccess;
using Moudle.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CostarWeb.Admin.Base.Product
{
    public partial class ProductAjax : System.Web.UI.Page
    {
        private StoreProductRepository _product = new StoreProductRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            int animeId = MyCommon.ToInt(Request["animeId"]);

            _OnPageLoad(animeId);
        }

        protected void _OnPageLoad(int animeId)
        {

            using (CostarDataContext linq = new CostarDataContext())
            {
                if (animeId == 0)
                {
                    var q = linq.StoreProducts.Where(c => c.DeleteDate == null).Select(c => new
                    {
                        img = c.ImageFile != "" ? c.ImageFile :
                              c.ImageFile2 != "" ? c.ImageFile2 :
                              c.ImageFile3 != "" ? c.ImageFile3 :
                              c.ImageFile4 != "" ? c.ImageFile4 :
                              "/images/ico_error-24.png",
                        Name = c.Name,
                        RetailPrice = c.RetailPrice,
                        SalePrice = c.SalePrice,
                        VIPPrice = c.VIPPrice,
                        SellCount = c.ProductID,
                        WarHouseCount = linq.StoreProductInventories.Where(i => i.ProductID == c.ProductID).Sum(a => (int?)a.QtyAvail),
                        ProductID = c.ProductID,
                        IsActive = c.IsActive == true ? "/images/ico_success-24.png" : "/images/ico_error-24.png"
                    });

                    rpt_list.DataSource = q;
                    rpt_list.DataBind();
                }
                else
                {
                    var q = linq.StoreProducts.Where(c => c.AnimeID == animeId && c.DeleteDate == null).Select(c => new
                    {
                        img = c.ImageFile != "" ? c.ImageFile :
                              c.ImageFile2 != "" ? c.ImageFile2 :
                              c.ImageFile3 != "" ? c.ImageFile3 :
                              c.ImageFile4 != "" ? c.ImageFile4 :
                              "/images/ico_error-24.png",
                        Name = c.Name,
                        RetailPrice = c.RetailPrice,
                        SalePrice = c.SalePrice,
                        VIPPrice = c.VIPPrice,
                        SellCount = c.ProductID,
                        WarHouseCount = linq.StoreProductInventories.Where(i => i.ProductID == c.ProductID).Sum(a => (int?)a.QtyAvail),
                        ProductID = c.ProductID,
                        IsActive = c.IsActive == true ? "/images/ico_success-24.png" : "/images/ico_error-24.png"
                    });

                    rpt_list.DataSource = q;
                    rpt_list.DataBind();
                }
            }
        }

    }
}