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
    public partial class delete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is not authenticated
            if (Session["Authenticated"] == null || !(bool)Session["Authenticated"])
            {
                // If not authenticated, redirect to the Login page
                Response.Redirect("~/Auth/Login.aspx");
            }
            if (Request.QueryString["id"] != null)
            {
                if (!IsPostBack)
                {
                    int categoryId = Convert.ToInt32(Request.QueryString["id"]);
                    string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM categories WHERE Id=@id", conn);
                        cmd.Parameters.AddWithValue("@id", categoryId);
                        cmd.ExecuteNonQuery();

                        Response.Redirect("categories.aspx");
                    }
                }
            }
        }

    }
}