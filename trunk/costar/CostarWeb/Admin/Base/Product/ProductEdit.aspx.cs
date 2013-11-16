using Common;
using Moudle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace CostarWeb.Admin.Base.Product
{
    public partial class ProductEdit : System.Web.UI.Page
    {
        protected string _colors = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            int productID = MyCommon.ToInt(Request["ProductID"]);
            _OnPageLoad();

            switch (Request["b"])
            {
                case "upload1": _OnUpload("file1");
                    break;
                case "upload2": _OnUpload("file2");
                    break;
                case "upload3": _OnUpload("file3");
                    break;
                case "upload4": _OnUpload("file4");
                    break;
                case "imgDel1": _OnUploadDel("ImageFile1");
                    break;
                case "imgDel2": _OnUploadDel("ImageFile2");
                    break;
                case "imgDel3": _OnUploadDel("ImageFile3");
                    break;
                case "imgDel4": _OnUploadDel("ImageFile4");
                    break;
                case "submit": _OnSubmit(productID);
                    break;
                case "edit": _OnEdit(productID);
                    break;
            }
        }

        protected void _OnEdit(int pId)
        {
            using (LinqDataContext linq = new LinqDataContext())
            {
                var q = linq.StoreProducts.Where(c => c.ProductID == pId).Single();

                ProductName.Value = q.Name;
                Title.Value = q.Title;
                ProductCode.Value = q.ProductCode;
                PageName.Value = q.PageName;
                Description.Value = q.Description;
                img1.Src = q.ImageFile;
                ImageFile1.Value = q.ImageFile;
                img2.Src = q.ImageFile2;
                ImageFile2.Value = q.ImageFile2;
                img3.Src = q.ImageFile3;
                ImageFile3.Value = q.ImageFile3;
                img4.Src = q.ImageFile4;
                ImageFile4.Value = q.ImageFile4;
                RetailPrice.Value = q.RetailPrice.ToString();
                SalePrice.Value = q.SalePrice.ToString();
                VIPPrice.Value = q.VIPPrice.ToString();

                if (q.IsFeatured.ToString().ToLower() == "true")
                    IsFeatured.Checked = true;
                if (q.IsActive.ToString().ToLower() == "true")
                    IsActive.Checked = true;

                //Animes
                AnimeID.Value = q.AnimeID.ToString();
                ddl_StoreAnimes.Items.FindByValue(q.AnimeID.ToString()).Selected = true;
                //ProductColor
                var b = linq.StoreProductColors.Where(c => c.ProductID == pId).Select(c => c.ColorID);
                foreach (var color in b)
                    _colors += "," + color;
            }
        }

        protected void _OnSubmit(int productID)
        {
            using (LinqDataContext linq = new LinqDataContext())
            {
                StoreProduct product;
                if (productID < 1) product = new StoreProduct();
                else product = linq.StoreProducts.Where(c => c.ProductID == productID).SingleOrDefault();

                product.Name = Request["ProductName"];
                product.Title = Request["Title"];
                product.ProductCode = Request["ProductCode"];
                product.PageName = Request["PageName"];
                product.Description = Request["Description"];
                product.ImageFile = Request["ImageFile1"];
                product.ImageFile2 = Request["ImageFile2"];
                product.ImageFile3 = Request["ImageFile3"];
                product.ImageFile4 = Request["ImageFile4"];
                product.AnimeID = MyCommon.ToInt(Request["AnimeID"]);
                product.RetailPrice = MyCommon.ToDecimal(Request["RetailPrice"]);
                product.SalePrice = MyCommon.ToDecimal(Request["SalePrice"]);
                product.VIPPrice = MyCommon.ToDecimal(Request["VIPPrice"]);
                product.IsFeatured = Request["IsFeatured"] == "on" ? true : false;
                product.IsActive = Request["IsActive"] == "on" ? true : false;
                //添加
                if (productID < 1)
                {
                    product.AddeDate = DateTime.Now;
                    linq.StoreProducts.InsertOnSubmit(product);
                }
                linq.SubmitChanges();

                long pId = product.ProductID;
                //编辑前删除已有颜色
                if (productID > 0)
                {
                    linq.StoreProductColors.DeleteAllOnSubmit(linq.StoreProductColors.Where(c => c.ProductID == pId));
                    linq.SubmitChanges();
                }
                if (Request["color"] != null)
                {
                    for (int i = 0; i < Request["color"].Split(',').Length; i++)
                    {
                        StoreProductColor pcolor = new StoreProductColor();
                        pcolor.ProductID = pId;
                        pcolor.ColorID = MyCommon.ToInt(Request["color"].Split(',')[i]);

                        linq.StoreProductColors.InsertOnSubmit(pcolor);
                        linq.SubmitChanges();
                    }
                }

                HttpContext.Current.Response.Write("success");
                HttpContext.Current.Response.End();
            }
        }

        protected void _OnPageLoad()
        {
            using (LinqDataContext linq = new LinqDataContext())
            {
                //StoreAnimes
                ddl_StoreAnimes.DataSource = linq.StoreAnimes.Select(c => new { val = c.AnimeID, txt = c.AnimeName });
                ddl_StoreAnimes.DataTextField = "txt";
                ddl_StoreAnimes.DataValueField = "val";
                ddl_StoreAnimes.DataBind();

                //Color
                rpt_color.DataSource = linq.StoreColors.Select(c => new { ColorID = c.ColorID, ColorName = c.ColorName });
                rpt_color.DataBind();
            }
        }

        protected void _OnUpload(string Files)
        {
            if (!string.IsNullOrEmpty(Request.Files[Files].FileName))
            {
                string Anime = Request["AnimeID"];

                UploadFiles upLoad = new UploadFiles();
                upLoad.FileExtensionType = ".gif.png.jpeg.jpg";
                upLoad.Dircetory = "/UploadFiles/StoreAnimes/" + Anime + "/";
                upLoad.NewFileName = 2;
                upLoad.MaxFileSize = 500;

                string upfile = upLoad.Upload(Request.Files[Files]);
                if (upfile == "1")
                {
                    HttpContext.Current.Response.Write("<script>alert('上传类型不正确');</script>err");
                    HttpContext.Current.Response.End();
                }
                if (upfile == "2")
                {
                    HttpContext.Current.Response.Write("<script>alert('图片限制500K');</script>err");
                    HttpContext.Current.Response.End();
                }

                HttpContext.Current.Response.Write(upfile.Split('|')[0] + "|" + Request.Files[Files] + "|" + upfile.Split('|')[1] + "K");
                HttpContext.Current.Response.End();
            }
        }

        protected void _OnUploadDel(string TempImageFile)
        {
            string ImageFile = Request[TempImageFile];
            new UploadFiles().UploadDel(ImageFile);

            HttpContext.Current.Response.Write(ImageFile);
            HttpContext.Current.Response.End();
        }
    }
}