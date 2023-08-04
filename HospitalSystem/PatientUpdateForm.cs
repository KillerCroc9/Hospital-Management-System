using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;

namespace HospitalSystem
{
    public partial class PatientUpdateForm : Form
    {
        public PatientUpdateForm()
        {
            InitializeComponent();
            PopulateMachines();
            PopulateRooms();
        }

        private void PopulateMachines()
        {
            List<Machine> machines = GetMachinesFromDatabase();
            cmbMachines.DisplayMember = "MachineType";
            cmbMachines.DataSource = machines;
        }

        private void PopulateRooms()
        {
            List<Room> rooms = GetRoomsFromDatabase();
            cmbRooms.DisplayMember = "RoomNumber";
            cmbRooms.DataSource = rooms;
        }
        private void PatientUpdateForm_Load(object sender, EventArgs e)
        {

        }
    

       
        public class Machine
        {
            public int MachineID { get; set; }
            public string MachineType { get; set; }
            public bool IsAvailable { get; set; }
        }

        public class Room
        {
            public int RoomID { get; set; }
            public string RoomNumber { get; set; }
            public bool IsAvailable { get; set; }
        }
        private List<Machine> GetMachinesFromDatabase()
        {
            List<Machine> machines = new List<Machine>();

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hospital;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT MachineID, MachineType, IsAvailable FROM Machines";
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Machine machine = new Machine
                    {
                        MachineID = Convert.ToInt32(reader["MachineID"]),
                        MachineType = reader["MachineType"].ToString(),
                        IsAvailable = Convert.ToBoolean(reader["IsAvailable"])
                    };

                    machines.Add(machine);
                }

                reader.Close();
            }

            return machines;
        }

        private List<Room> GetRoomsFromDatabase()
        {
            List<Room> rooms = new List<Room>();

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hospital;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT RoomID, RoomNumber, IsAvailable FROM Rooms";
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Room room = new Room
                    {
                        RoomID = Convert.ToInt32(reader["RoomID"]),
                        RoomNumber = reader["RoomNumber"].ToString(),
                        IsAvailable = Convert.ToBoolean(reader["IsAvailable"])
                    };

                    rooms.Add(room);
                }

                reader.Close();
            }

            return rooms;
        }


        private void btnFind_Click(object sender, EventArgs e)
        {
            string patientID = txtPatientID.Text;

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hospital;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Patients WHERE Id = @PatientID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PatientID", patientID);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Fill the form fields with the retrieved patient information
                    txtPatientName.Text = reader["PatientName"].ToString();
                    txtAge.Text = reader["Age"].ToString();
                    cmbGender.SelectedItem = reader["Gender"].ToString();
                    txtDisease.Text = reader["Disease"].ToString();
                    cmbBloodGroup.SelectedItem = reader["BloodGroup"].ToString();
                    cmbMachines.SelectedItem = reader["MachineId"].ToString();
                    cmbRooms.SelectedItem = reader["RoomId"].ToString();
                    int machineTypeID;
                    if (int.TryParse(reader["MachineId"].ToString(), out machineTypeID))
                    {
                        foreach (var item in cmbMachines.Items)
                        {
                            if (item is Machine machine && machine.MachineID == machineTypeID)
                            {
                                cmbMachines.SelectedItem = machine;
                                break;
                            }
                        }
                    }
                    int RoomId;
                    if (int.TryParse(reader["RoomId"].ToString(), out RoomId))
                    {
                        foreach (var item in cmbRooms.Items)
                        {
                            if (item is Room room && room.RoomID == RoomId)
                            {
                                cmbRooms.SelectedItem = room;
                                break;
                            }
                        }
                    }

                }
                
                  
                
                else
                {
                    MessageBox.Show("Patient not found.", "Error");
                }

                reader.Close();
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string patientID = txtPatientID.Text;
            string patientName = txtPatientName.Text;

            // Validate and parse age
            if (!int.TryParse(txtAge.Text, out int age))
            {
                MessageBox.Show("Invalid age.", "Error");
                return;
            }

            string gender = cmbGender.SelectedItem?.ToString();
            string disease = txtDisease.Text;
            string bloodGroup = cmbBloodGroup.SelectedItem?.ToString();
            Debug.WriteLine("++" + bloodGroup);

            // Validate patient ID, name, age, gender, and disease
            if (string.IsNullOrEmpty(patientID) || string.IsNullOrEmpty(patientName) || string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(disease))
            {
                MessageBox.Show("Please fill in all the required fields correctly.", "Error");
                return;
            }
            string machineName = (cmbMachines.SelectedItem as Machine)?.MachineType;

            Debug.WriteLine("++"+machineName);

            string roomName = (cmbRooms.SelectedItem as Room)?.RoomNumber;
            int machineID = 0;
            int roomID = 0;


            if (!string.IsNullOrEmpty(machineName))
            {
               
                    machineID = GetMachineID(machineName);
                
            
            }

            if (!string.IsNullOrEmpty(roomName))
            {
                
                
                    roomID = GetRoomID(roomName);
                
            }

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hospital;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE Patients SET PatientName = @PatientName, Age = @Age, Gender = @Gender, Disease = @Disease, BloodGroup = @BloodGroup, MachineId = @MachineId, RoomId = @RoomId WHERE Id = @PatientID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PatientName", patientName);
                command.Parameters.AddWithValue("@Age", age);
                command.Parameters.AddWithValue("@Gender", gender);
                command.Parameters.AddWithValue("@Disease", disease);
                command.Parameters.AddWithValue("@BloodGroup", bloodGroup);
                command.Parameters.AddWithValue("@MachineId", machineID);
                command.Parameters.AddWithValue("@RoomId", roomID);
                command.Parameters.AddWithValue("@PatientID", patientID);
                Debug.WriteLine("Command Text: " + command.CommandText);
                Debug.WriteLine("Parameters:");
                foreach (SqlParameter parameter in command.Parameters)
                {
                    Debug.WriteLine($"{parameter.ParameterName}: {parameter.Value}");
                }
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Patient updated successfully.", "Success");
                }
                else
                {
                    MessageBox.Show("Failed to update patient.", "Error");
                }
            }
        }

        private int GetRoomID(string roomName)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hospital;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT RoomID FROM Rooms WHERE RoomNumber = @RoomNumber";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RoomNumber", roomName);

                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return (int)result;
                }

                return 0; 
            }
        }
        private int GetMachineID(string machineType)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hospital;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT MachineID FROM Machines WHERE MachineType = @MachineType";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MachineType", machineType);

                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return (int)result;
                }

                return 0; // or any other default value that suits your needs
            }
        }



        private bool IsMachineAvailable(int machineID)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hospital;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT MachineID FROM Machines WHERE MachineID = @MachineID AND IsAvailable = 1";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MachineID", machineID);

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool IsRoomAvailable(int roomID)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hospital;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT RoomID FROM Rooms WHERE RoomID = @RoomID AND IsAvailable = 1";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RoomID", roomID);

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }




        private void btnCancel_Click(object sender, EventArgs e)
        {
            DashboardForm dashboardForm = new DashboardForm();

            dashboardForm.Show();

            this.Close();
        }

        private void PatientUpdateForm_Load_1(object sender, EventArgs e)
        {

        }

    }

}
