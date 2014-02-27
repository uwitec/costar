using Common;
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
    public partial class EditProduct : System.Web.UI.Page
    {
        StoreProduct _product = null;
        StoreProductRepository storeProductRep = null;
        StoreAnimeRepository storeAnimeRep = null;
        StoreColorRepository storeColorRep = null;
        public EditProduct()
        {
            storeProductRep = new StoreProductRepository();
            storeAnimeRep = new StoreAnimeRepository();
            storeColorRep = new StoreColorRepository();

            long productID = 0;
            if (!string.IsNullOrEmpty(HttpContext.Current.Request["ProductID"]))
                productID = MyCommon.ToLong(HttpContext.Current.Request["ProductID"]);
            if (productID > 0)
            {
                _product = storeProductRep.GetProductByID(productID);
                if (_product == null)
                    HttpContext.Current.Response.Redirect("ProductList.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadEditProduct();
        }

        private void LoadEditProduct()
        {
            List<StoreAnime> animes = storeAnimeRep.GetAllStoreAnimes();
            List<StoreColor> colors = storeColorRep.GetAllStoreColors();
            if (colors.Count > 0)
            {
                chkColors.DataSource = colors;
                chkColors.DataValueField = "ColorID";
                chkColors.DataTextField = "ColorName";
                chkColors.DataBind();
            }
            if (animes.Count > 0)
            {
                ddlAnimes.DataSource = animes;
                ddlAnimes.DataValueField = "AnimeID";
                ddlAnimes.DataTextField = "AnimeName";
                if (_product != null)
                    ddlAnimes.SelectedValue = _product.AnimeID.ToString();
                ddlAnimes.DataBind();
            }

            if (_product == null)
            {
                radImgCur.Visible = false;
                imgImg.Visible = false;
                radImgNew.Visible = true;
                radImgNew.Checked = true;
                pnlImg2.Visible = false;
                pnlImg3.Visible = false;
                pnlImg4.Visible = false;
            }
            else
            {
                List<StoreColor> pColors = storeColorRep.GetAllStoreColorsByProductID(_product.ProductID);
                txtName.Text = _product.Name;
                txtCode.Text = _product.ProductCode;
                txtShortDescription.Text = _product.Title;
                txtUrl.Text = _product.PageName;
                txtDescription.Text = _product.Description;
                if (_product.RetailPrice.HasValue)
                    txtRetailPrice.Text = _product.RetailPrice.Value.ToString("F2");
                txtSalePrice.Text = _product.SalePrice.ToString("F2");
                chkApproved.Checked = _product.IsActive;
                chkFeatured.Checked = _product.IsFeatured;
                if (_product.VIPPrice.HasValue)
                    txtVipPrice.Text = _product.VIPPrice.Value.ToString("F2");

                if (!string.IsNullOrEmpty(_product.ImageFile))
                {
                    radImgCur.Checked = true;
                    imgImg.ImageUrl = string.Format("~/images/{0}", _product.ImageFile);
                }
                else
                {
                    radImgNew.Checked = true;
                    radImgCur.Visible = false;
                    imgImg.Visible = false;
                }

                if (!string.IsNullOrEmpty(_product.ImageFile2))
                {
                    pnlImg2.Visible = true;
                    img2.ImageUrl = string.Format("~/images/{0}", _product.ImageFile2);
                    fup2.Visible = false;
                }
                else
                    pnlImg2.Visible = false;

                if (!string.IsNullOrEmpty(_product.ImageFile3))
                {
                    pnlImg3.Visible = true;
                    img3.ImageUrl = string.Format("~/images/{0}", _product.ImageFile3);
                    fup3.Visible = false;
                }
                else
                    pnlImg3.Visible = false;

                if (!string.IsNullOrEmpty(_product.ImageFile4))
                {
                    pnlImg4.Visible = true;
                    img4.ImageUrl = string.Format("~/images/{0}", _product.ImageFile4);
                    fup4.Visible = false;
                }
                else
                    pnlImg4.Visible = false;
                foreach (ListItem item in chkColors.Items)
                {
                    if (pColors.Where(c => c.ColorID == int.Parse(item.Value)).FirstOrDefault() != null)
                        item.Selected = true;
                }
            }
        }

        protected void btnDel2_Click(object sender, EventArgs e)
        {
            //TODO
        }

        protected void btnDel3_Click(object sender, EventArgs e)
        {
            //TODO
        }

        protected void btnDel4_Click(object sender, EventArgs e)
        {
            //TODO
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            decimal retailPrice = 0;
            if (!string.IsNullOrEmpty(txtRetailPrice.Text))
                decimal.TryParse(txtRetailPrice.Text, out retailPrice);
            decimal salePrice = 0;
            decimal.TryParse(txtSalePrice.Text, out salePrice);
            decimal vipPrice = 0;
            decimal.TryParse(txtVipPrice.Text, out vipPrice);

            HttpPostedFile imageFile = null;
            if (radImgNew.Checked && fupFile.PostedFile != null &&
                fupFile.PostedFile.ContentLength != 0)
                imageFile = fupFile.PostedFile;

            HttpPostedFile image2File = null;
            if (fup2.PostedFile != null && fup2.PostedFile.ContentLength != 0)
                image2File = fup2.PostedFile;

            HttpPostedFile image3File = null;
            if (fup3.PostedFile != null && fup3.PostedFile.ContentLength != 0)
                image3File = fup3.PostedFile;

            HttpPostedFile image4File = null;
            if (fup4.PostedFile != null && fup4.PostedFile.ContentLength != 0)
                image4File = fup4.PostedFile;

            List<int> colorIDs = new List<int>();
            foreach (ListItem item in chkColors.Items)
            {
                if (item.Selected)
                    colorIDs.Add(int.Parse(item.Value));
            }

            int animeID = 0;
            int.TryParse(ddlAnimes.SelectedValue, out animeID);

            if (_product == null)
                _product = new StoreProduct();
            _product.AnimeID = animeID;
            _product.ProductCode = txtCode.Text;
            _product.Name = txtName.Text;
            _product.Description = txtDescription.Text;
            _product.Title = txtShortDescription.Text;
            _product.PageName = txtUrl.Text;
            _product.IsActive = chkApproved.Checked;
            _product.IsFeatured = chkFeatured.Checked;
            if (imageFile != null)
            {
                string path = Server.MapPath("~/images/");
                imageFile.SaveAs(string.Format("{0}{1}", path, imageFile.FileName));
                _product.ImageFile = imageFile.FileName;
            }
            if (image2File != null)
                _product.ImageFile2 = image2File.FileName;
            if (image3File != null)
                _product.ImageFile3 = image3File.FileName;
            if (image4File != null)
                _product.ImageFile4 = image4File.FileName;
            if (retailPrice > 0)
                _product.RetailPrice = retailPrice;
            if (salePrice > 0)
                _product.SalePrice = salePrice;
            if (vipPrice > 0)
                _product.VIPPrice = vipPrice;

            storeProductRep.SaveProduct(_product);
        }
    }
}