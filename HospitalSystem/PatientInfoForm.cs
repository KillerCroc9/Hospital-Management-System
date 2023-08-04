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
    public partial class PatientInfoForm : Form
    {
        private SqlConnection con;
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = Hospital; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private SqlDataAdapter adapter;
        private DataTable dataTable;
        public PatientInfoForm()
        {
            InitializeComponent();
            con = new SqlConnection(connectionString); 
            adapter = new SqlDataAdapter();
            dataTable = new DataTable();
            LoadPatientData();
        }
        private void LoadPatientData()
        {
            try
            {
                con.Open();

                string query = "SELECT * FROM Patients";

                adapter.SelectCommand = new SqlCommand(query, con);
                dataTable.Clear();
                adapter.Fill(dataTable);

                dataGridView1.Columns.Clear();

                dataGridView1.DataSource = dataTable;

                // Format the Check-In column
                DataGridViewImageColumn checkInColumn = new DataGridViewImageColumn();
                checkInColumn.Name = "CheckIn";
                checkInColumn.HeaderText = "Check-In";
                dataGridView1.Columns.Add(checkInColumn);

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    bool checkIn = (bool)row.Cells["CheckedIn"].Value;

                    if (checkIn)
                    {
                        row.Cells["CheckIn"].Value = "✓"; 
                    }
                    else
                    {
                        row.Cells["CheckIn"].Value = "✗"; // 
                    };
                }

                // Hide the original CheckIn column
                dataGridView1.Columns["CheckedIn"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading patient data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void PatientInfoForm_Load_1(object sender, EventArgs e)
        {

        }
    }

}
