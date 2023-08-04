using HospitalManagementSystem;

namespace HospitalSystem
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        private static LoginForm loginForm;
        private static DashboardForm dashboardForm;

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            loginForm = new LoginForm();
            dashboardForm = new DashboardForm();

            loginForm.LoginSuccess += LoginForm_LoginSuccess;

            Application.Run(loginForm);
        }

        private static void LoginForm_LoginSuccess(object sender, EventArgs e)
        {
            loginForm.Hide();
            dashboardForm.Show();
        }
    }
}
