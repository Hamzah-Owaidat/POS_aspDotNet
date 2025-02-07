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
    public partial class edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Authenticated"] == null || !(bool)Session["Authenticated"])
            {
                // If not authenticated, redirect to the Login page
                Response.Redirect("~/Auth/Login.aspx");
            }
            else
            {
                if (Request.QueryString["id"] != null)
                {
                    if (!IsPostBack)
                    {
                        int employeeId = Convert.ToInt32(Request.QueryString["id"]);
                        LoadEmployeeData(employeeId);
                    }
                }
                else
                {
                    lblMessage.Text = "Employee ID is missing!";
                }
            }
            
        }

        // Method to load employee data from the database
        private void LoadEmployeeData(int employeeId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "SELECT * FROM employees WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", employeeId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    // Populate the fields with the current data
                    txtUsername.Text = reader["username"].ToString();
                    txtPassword.Text = reader["password"].ToString();
                    ddlRole.SelectedValue = reader["role"].ToString();
                    txtSalary.Text = reader["salary"].ToString();
                }
                else
                {
                    lblMessage.Text = "Employee not found!";
                }
                conn.Close();
            }
        }

        // Button click event to save changes
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                int employeeId = Convert.ToInt32(Request.QueryString["id"]);
                SaveEmployeeChanges(employeeId);
            }
        }

        // Method to save the changes made by the user
        private void SaveEmployeeChanges(int employeeId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "UPDATE employees SET username = @Username, role = @Role, password = @Password, salary = @Salary, updated_by = @UpdatedBy, updated_at = GETDATE() WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@Role", ddlRole.SelectedValue);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@Salary", Convert.ToDecimal(txtSalary.Text));
                cmd.Parameters.AddWithValue("@UpdatedBy", Session["UserID"]);
                cmd.Parameters.AddWithValue("@Id", employeeId);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    Response.Redirect("/Dashboard/Employees/employees.aspx");
                }
                else
                {
                    lblMessage.Text = "Error while saving changes.";
                }
                conn.Close();
            }
        }
    }
}