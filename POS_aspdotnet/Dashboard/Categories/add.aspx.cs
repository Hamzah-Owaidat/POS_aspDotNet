using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POS_aspdotnet.Dashboard.Categories
{
    public partial class add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Authenticated"] == null || !(bool)Session["Authenticated"])
            {
                Response.Redirect("~/Auth/Login.aspx");
            }
        }

        protected void btnSaveCategory_Click(object sender, EventArgs e)
        {
            string categoryName = txtCategoryName.Text.Trim();

            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "INSERT INTO categories (name, created_by, updated_by, created_at) " +
                           "VALUES (@Name, @CreatedBy, @UpdatedBy, GETDATE())";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", categoryName);
                cmd.Parameters.AddWithValue("@CreatedBy", Session["UserID"]);
                cmd.Parameters.AddWithValue("@UpdatedBy", Session["UserID"]);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Response.Redirect("categories.aspx");
                }
            }
        }

    }
}