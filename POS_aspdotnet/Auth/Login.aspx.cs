using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace POS_aspdotnet.Auth
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // Check if the user is already authenticated
            if (Session["Authenticated"] != null && (bool)Session["Authenticated"])
            {
                // If user is authenticated, redirect to the default page (e.g., Dashboard or home page)
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                lblMessage.Text = ""; // Clear any existing messages on the login page
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if(password.Length < 8)
            {
                rfvPassword.ErrorMessage = "Password must be at least 8 characters long";
            }

            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT Id, role FROM employees WHERE username = @username AND password = @password";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        int userId = Convert.ToInt32(result);
                        int userRole = Convert.ToInt32(result);

                        Session["Authenticated"] = true;
                        // Save user ID and ROLE in Session
                        Session["UserID"] = userId;
                        Session["userRole"] = userRole;

                        Response.Redirect("~/Default.aspx");

                    }
                    else
                    {
                        lblMessage.Text = "Invalid username or password";
                    }

                }
            }
        }
    }
}
