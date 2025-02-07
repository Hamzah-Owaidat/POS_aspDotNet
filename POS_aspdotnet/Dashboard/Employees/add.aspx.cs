using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace POS_aspdotnet.Dashboard.Employees
{
    public partial class add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Authenticated"] == null || !(bool)Session["Authenticated"])
            {
                // If not authenticated, redirect to the Login page
                Response.Redirect("~/Auth/Login.aspx");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "INSERT INTO employees (role, username, password, salary, created_by, updated_by, created_at, updated_at) " +
                "VALUES (@Role, @Username, @Password, @Salary, @CreatedBy, @UpdatedBy, GETDATE(), GETDATE())";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Role", ddlRole.SelectedValue);
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                    cmd.Parameters.AddWithValue("@Salary", Convert.ToDecimal(txtSalary.Text.Trim()));
                    cmd.Parameters.AddWithValue("@CreatedBy", Session["UserID"]);
                    cmd.Parameters.AddWithValue("@UpdatedBy", Session["UserID"]);
                    cmd.ExecuteNonQuery();
                }
            }

            Response.Redirect("employees.aspx");
        }
    }
}