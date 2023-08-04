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
    public partial class DoctorForm : Form
    {
        private SqlConnection con;
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = Hospital; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        public DoctorForm()
        {
            InitializeComponent();
            /*            AddDummyData();
            */
            LoadDoctorData();
        }


        private void AddDummyData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO Docters (DoctorName, Degree) VALUES (@DoctorName, @Degree)";

                    command.Parameters.AddWithValue("@DoctorName", "Dr. John Doe");
                    command.Parameters.AddWithValue("@Degree", "MD");
                    command.ExecuteNonQuery();

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@DoctorName", "Dr. Jane Smith");
                    command.Parameters.AddWithValue("@Degree", "DO");
                    command.ExecuteNonQuery();


                    MessageBox.Show("Dummy data added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding dummy data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LoadDoctorData()
        {
            string query = "SELECT ID, DoctorName, Degree FROM Docters";

            using (con = new SqlConnection(connectionString))
            {
                adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(query, con);
                dataTable = new DataTable(); // Instantiate the DataTable object

                try
                {
                    con.Open();
                    dataTable.Clear();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;

                    dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading doctor data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void btnViewPatients_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int doctorID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT PatientName, Age, Gender, Disease, BloodGroup FROM Patients WHERE DoctorID = @DoctorID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DoctorID", doctorID);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            try
                            {
                                connection.Open();
                                adapter.Fill(dataTable);

                                using (DataGridView dataGridView = new DataGridView())
                                {
                                    dataGridView.DataSource = dataTable;
                                    dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                                    dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                                    dataGridView.ReadOnly = true;

                                    using (Form form = new Form())
                                    {
                                        form.Text = "Patient List";
                                        form.StartPosition = FormStartPosition.CenterScreen;
                                        form.Controls.Add(dataGridView);
                                        form.ShowDialog();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("An error occurred while retrieving patient data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a doctor.", "No Doctor Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
