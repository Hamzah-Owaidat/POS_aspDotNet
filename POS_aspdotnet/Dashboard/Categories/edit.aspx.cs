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
    public partial class edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Authenticated"] == null || !(bool)Session["Authenticated"])
            {
                // Redirect to login if not authenticated
                Response.Redirect("~/Auth/Login.aspx");
            }

            if (!IsPostBack)
            {
                int categoryId = Convert.ToInt32(Request.QueryString["id"]);
                LoadCategoryData(categoryId);
            }
        }

        private void LoadCategoryData(int categoryId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "SELECT Id, name FROM categories WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", categoryId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtCategoryName.Text = reader["name"].ToString();
                }
                conn.Close();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int categoryId = Convert.ToInt32(Request.QueryString["id"]);
            string categoryName = txtCategoryName.Text;
       

            // Save updated stock data to the database
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "UPDATE categories SET name = @Name, updated_by = @UpdatedBy, updated_at = GETDATE() WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", categoryName);
                cmd.Parameters.AddWithValue("@UpdatedBy", Session["UserID"]);
                cmd.Parameters.AddWithValue("@Id", categoryId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            Response.Redirect("categories.aspx");
        }
    }
}