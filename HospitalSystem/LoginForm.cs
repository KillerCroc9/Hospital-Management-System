using HospitalSystem;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;

namespace HospitalManagementSystem
{
    public partial class LoginForm : Form
    {
        private SqlConnection connection;
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = Hospital; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public event EventHandler LoginSuccess;

        partial void InitializeLoginForm()
        {
            BtnLogin.Click += BtnLogin_Click;

        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = TxtUsername.Text;
            string password = TxtPassword.Text;
            if (AuthenticateUser(username, password))
            {
                MessageBox.Show("Login successful!");
                OnLoginSuccess();


            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }
        private void OnLoginSuccess()
        {
            LoginSuccess?.Invoke(this, EventArgs.Empty);
        }
        private bool AuthenticateUser(string username, string password)
        {
            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM [dbo].[Admins] WHERE Username = @Username AND Password = @Password";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    Debug.WriteLine("Command Text: " + command.CommandText);
                    Debug.WriteLine("Parameters:");
                    foreach (SqlParameter parameter in command.Parameters)
                    {
                        Debug.WriteLine($"{parameter.ParameterName}: {parameter.Value}");
                    }
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while authenticating user. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

    }
}
