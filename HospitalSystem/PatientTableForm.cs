using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalSystem
{
    public partial class PatientTableForm : Form
    {
        private SqlConnection con;
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hospital;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private SqlDataAdapter adapter;
        private DataTable dataTable;
        private int doctorID;

        public PatientTableForm(int doctorID)
        {
            InitializeComponent();
            this.doctorID = doctorID;
            LoadPatientData();
        }

        private void LoadPatientData()
        {
            string query = "SELECT * FROM Patients WHERE DoctorID = @DoctorID";

            using (con = new SqlConnection(connectionString))
            {
                adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(query, con);
                adapter.SelectCommand.Parameters.AddWithValue("@DoctorID", doctorID);
                dataTable = new DataTable(); // Instantiate the DataTable object

                try
                {
                    con.Open();
                    dataTable.Clear();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading patient data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
    