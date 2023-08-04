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
    public partial class EquipmentRoomsForm : Form
    {
        private SqlConnection con;
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hospital;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private SqlDataAdapter roomsAdapter;
        private SqlDataAdapter machinesAdapter;
        private DataTable roomsDataTable;
        private DataTable machinesDataTable;
        public EquipmentRoomsForm()
        {
            InitializeComponent();

            LoadRoomsData();
            LoadMachinesData();
        }
        private void FillDummyData()
        {
            string insertRoomsQuery = "INSERT INTO Rooms (RoomNumber, IsAvailable) VALUES (@RoomNumber, @IsAvailable)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertRoomsQuery, connection))
                {
                    connection.Open();

                    for (int i = 1; i <= 10; i++)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@RoomNumber", "Room " + i);
                        command.Parameters.AddWithValue("@IsAvailable", true);
                        command.ExecuteNonQuery();
                    }
                }
            }

            string insertMachinesQuery = "INSERT INTO Machines (MachineType, IsAvailable) VALUES (@MachineType, @IsAvailable)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertMachinesQuery, connection))
                {
                    connection.Open();

                    for (int i = 1; i <= 10; i++)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@MachineType", "Machine " + i);
                        command.Parameters.AddWithValue("@IsAvailable", true);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }


        private void LoadRoomsData()
        {
            string query = "SELECT * FROM Rooms";

            using (con = new SqlConnection(connectionString))
            {
                roomsAdapter = new SqlDataAdapter();
                roomsAdapter.SelectCommand = new SqlCommand(query, con);
                roomsDataTable = new DataTable(); // Instantiate the DataTable object

                try
                {
                    con.Open();
                    roomsDataTable.Clear();
                    roomsAdapter.Fill(roomsDataTable);
                    dgvRooms.DataSource = roomsDataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading rooms data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void LoadMachinesData()
        {
            string query = "SELECT * FROM Machines";

            using (con = new SqlConnection(connectionString))
            {
                machinesAdapter = new SqlDataAdapter();
                machinesAdapter.SelectCommand = new SqlCommand(query, con);
                machinesDataTable = new DataTable(); 

                try
                {
                    con.Open();
                    machinesDataTable.Clear();
                    machinesAdapter.Fill(machinesDataTable);
                    dgvMachines.DataSource = machinesDataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading machines data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void dgvRooms_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
    
