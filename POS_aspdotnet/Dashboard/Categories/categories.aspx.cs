using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POS_aspdotnet.Dashboard.Categories
{
    public partial class categories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Authenticated"] == null || !(bool)Session["Authenticated"])
            {
                // If not authenticated, redirect to the Login page
                Response.Redirect("~/Auth/Login.aspx");
            }
            if (!IsPostBack)
            {
                LoadCategoryData();
            }
        }

        private void LoadCategoryData()
        {
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "SELECT c.Id, c.name, e.username FROM categories c JOIN employees e ON c.created_by = e.Id";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                // Bind data to the Repeater control
                rptCategories.DataSource = reader;
                rptCategories.DataBind();
            }
        }

        protected void btnAddCategories_Click(object sender, EventArgs e)
        {
            Response.Redirect("add.aspx");
        }
    }
}