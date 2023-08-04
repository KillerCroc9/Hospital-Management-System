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
    public partial class PatientCheckOutForm : Form
    {
        private SqlConnection con;
        private SqlDataAdapter adapter;
        private DataTable dataTable;
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = Hospital; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public PatientCheckOutForm()
        {
            InitializeComponent();
            con = new SqlConnection(connectionString); 
            adapter = new SqlDataAdapter();
            dataTable = new DataTable();
        }
        private void PatientCheckOutForm_Load(object sender, EventArgs e)
        {
            LoadCheckedInPatients();
        }

        private void LoadCheckedInPatients()
        {
            string query = "SELECT * FROM Patients WHERE CheckedIn = 1";
            adapter.SelectCommand = new SqlCommand(query, con);

            try
            {
                con.Open();
                dataTable.Clear();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading checked-in patients: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }


        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnCheckOut.Enabled = dataGridView1.SelectedRows.Count > 0;
        }

        private void BtnCheckOut_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int patientId = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;

                CheckOutPatient(patientId);
            }
            else
            {
                MessageBox.Show("Please select a patient to check out.", "No Patient Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CheckOutPatient(int patientId)
        {
            string updatePatientQuery = "UPDATE patients SET CheckedIn = 0 WHERE Id = @PatientId";
            string updateRoomQuery = "UPDATE rooms SET IsAvailable = 1 WHERE RoomId = (SELECT RoomId FROM patients WHERE Id = @PatientId)";
            string updateMachineQuery = "UPDATE machines SET IsAvailable = 1 WHERE MachineId = (SELECT MachineId FROM patients WHERE Id = @PatientId)";

            using (SqlCommand updatePatientCommand = new SqlCommand(updatePatientQuery, con))
            using (SqlCommand updateRoomCommand = new SqlCommand(updateRoomQuery, con))
            using (SqlCommand updateMachineCommand = new SqlCommand(updateMachineQuery, con))
            {
                updatePatientCommand.Parameters.AddWithValue("@PatientId", patientId);
                updateRoomCommand.Parameters.AddWithValue("@PatientId", patientId);
                updateMachineCommand.Parameters.AddWithValue("@PatientId", patientId);

                try
                {
                    con.Open();
                    updatePatientCommand.ExecuteNonQuery();
                    updateRoomCommand.ExecuteNonQuery();
                    updateMachineCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error checking out patient: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }
            }

            LoadCheckedInPatients();
        }

    }
}