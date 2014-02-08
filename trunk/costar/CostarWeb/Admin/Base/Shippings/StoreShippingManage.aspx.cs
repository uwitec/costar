using Common;
using Moudle;
using Moudle.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CostarWeb.Admin.Base.Shipping
{
    public partial class StoreShippingManage : System.Web.UI.Page
    {
        private StoreShippingOptionRepository _StoreShippingOption = new StoreShippingOptionRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            int ShippingOptionID = MyCommon.ToInt(Request["ShippingOptionID"]);
            string a = Request["a"];

            if (!IsPostBack)
            {
                _OnPageLoad();

                if (a == "edit")
                {
                    _OnPageLoadEdit(ShippingOptionID);
                    this.HiddenField1.Value = "edit";
                }
            }
        }

        protected void _OnPageLoad()
        {
            this.div_show.Style.Value = "display: none;";

            using (LinqDataContext linq = new LinqDataContext())
            {
                var q = linq.StoreShippingOptions.Select(c => new
                {
                    Name = c.Name,
                    Price = c.PerOrderFlatRate > 0 ? c.PerOrderFlatRate + "(每单)" :
                            (c.PerItemFlatRate > 0 ? c.PerItemFlatRate + "(每件)" :
                            (c.PerKGRate > 0 ? c.PerKGRate + "(每斤)" : "")),
                    IsActive = c.IsActive == true ? "/images/ico_success-24.png" : "/images/ico_error-24.png",
                    ShippingOptionID = c.ShippingOptionID
                });

                rpt_list.DataSource = q;
                rpt_list.DataBind();
            }
        }

        protected void _OnPageLoadEdit(int id)
        {
            this.div_show.Style.Value = "display: block;";

            this.txt_Name.Text = _StoreShippingOption.GetShippingByID(id).Name;
            this.txt_Description.Text = _StoreShippingOption.GetShippingByID(id).Instruction;
            this.CheckBox_Active.Checked = _StoreShippingOption.GetShippingByID(id).IsActive;

            if (_StoreShippingOption.GetShippingByID(id).PerItemFlatRate > 0)
            {
                this.txt_Price.Text = _StoreShippingOption.GetShippingByID(id).PerItemFlatRate.ToString();
                this.ddl_Per.SelectedValue = "0";
            }
            if (_StoreShippingOption.GetShippingByID(id).PerKGRate > 0)
            {
                this.txt_Price.Text = _StoreShippingOption.GetShippingByID(id).PerKGRate.ToString();
                this.ddl_Per.SelectedValue = "2";
            }
            if (_StoreShippingOption.GetShippingByID(id).PerOrderFlatRate > 0)
            {
                this.txt_Price.Text = _StoreShippingOption.GetShippingByID(id).PerOrderFlatRate.ToString();
                this.ddl_Per.SelectedValue = "1";
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            this.HiddenField1.Value = "add";
            this.div_show.Style.Value = "display: block;";

            this.txt_Name.Text = "";
            this.txt_Description.Text = "";
            this.txt_Price.Text = "";
            this.ddl_Per.SelectedValue = "0";
            this.CheckBox_Active.Checked = false;
        }

        protected void btn_cancle_Click(object sender, EventArgs e)
        {
            this.HiddenField1.Value = "add";
            this.div_show.Style.Value = "display: none;";

            this.txt_Name.Text = "";
            this.txt_Description.Text = "";
            this.txt_Price.Text = "";
            this.ddl_Per.SelectedValue = "0";
            this.CheckBox_Active.Checked = false;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            using (LinqDataContext linq = new LinqDataContext())
            {
                int ShippingOptionID = MyCommon.ToInt(Request["ShippingOptionID"]);

                StoreShippingOption shipping;
                if (this.HiddenField1.Value == "add") shipping = new StoreShippingOption();
                else shipping = linq.StoreShippingOptions.Where(c => c.ShippingOptionID == ShippingOptionID).SingleOrDefault();

                shipping.Name = this.txt_Name.Text;
                shipping.Instruction = this.txt_Description.Text;
                shipping.IsActive = this.CheckBox_Active.Checked;

                decimal price = 0;
                decimal.TryParse(this.txt_Price.Text, out price);

                switch (this.ddl_Per.SelectedValue)
                {
                    case "0":
                        shipping.PerItemFlatRate = price;
                        shipping.PerKGRate = null;
                        shipping.PerOrderFlatRate = null;
                        break;
                    case "1":
                        shipping.PerItemFlatRate = null;
                        shipping.PerKGRate = null;
                        shipping.PerOrderFlatRate = price;
                        break;
                    case "2":
                        shipping.PerItemFlatRate = null;
                        shipping.PerKGRate = price;
                        shipping.PerOrderFlatRate = null;
                        break;
                }

                if (this.HiddenField1.Value == "add")
                    linq.StoreShippingOptions.InsertOnSubmit(shipping);

                linq.SubmitChanges();

                MyCommon.Alert("保存成功.", "StoreShippingManage.aspx");
            }
        }
    }
}