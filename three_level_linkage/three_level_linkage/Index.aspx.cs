using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace three_level_linkage
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DAL.ProvincialDal provincialDal = new DAL.ProvincialDal();
                this.ddlProvince.DataSource = provincialDal.GetList();
                this.ddlProvince.DataTextField = "provincialName";
                this.ddlProvince.DataValueField = "provincialId";
                this.ddlProvince.DataBind();

                DAL.CityDal cityDal = new DAL.CityDal();
                this.ddlCity.DataSource = cityDal.GetList(Convert.ToInt32(this.ddlProvince.SelectedValue));
                this.ddlCity.DataTextField = "cityName";
                this.ddlCity.DataValueField = "cityId";
                this.ddlCity.DataBind();
            }
        }

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            DAL.CityDal cityDal = new DAL.CityDal();
            this.ddlCity.DataSource = cityDal.GetList(Convert.ToInt32(this.ddlProvince.SelectedValue));
            this.ddlCity.DataTextField = "cityName";
            this.ddlCity.DataValueField = "cityId";
            this.ddlCity.DataBind();
        }
    }
}