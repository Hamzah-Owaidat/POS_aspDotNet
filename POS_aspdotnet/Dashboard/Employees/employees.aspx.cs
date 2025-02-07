using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POS_aspdotnet.Dashboard.Employees
{
    public partial class employees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is not authenticated
            if (Session["Authenticated"] == null || !(bool)Session["Authenticated"])
            {
                // If not authenticated, redirect to the Login page
                Response.Redirect("~/Auth/Login.aspx");
            }
            if (!IsPostBack)
            {
                LoadEmployeeData();
            }
        }

        private void LoadEmployeeData()
        {
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "SELECT e.Id, e.username, r.role_name, e.salary, e.created_by, e.created_at FROM employees e JOIN roles r ON e.role = r.Id";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                // Bind data to the Repeater control
                rptEmployees.DataSource = reader;
                rptEmployees.DataBind();
            }
        }

        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {
            Response.Redirect("add.aspx");
        }
    }
}