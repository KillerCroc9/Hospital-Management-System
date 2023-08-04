using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalSystem
{
    public partial class PatientCheckInForm : Form
    {
        private SqlConnection connection;
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hospital;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private List<string> availableRooms;
        private List<string> availableMachines;

        public PatientCheckInForm()
        {
            InitializeComponent();

            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    availableRooms = GetAvailableRooms(connection);
                    availableMachines = GetAvailableMachines(connection);
                    cmbRooms.DataSource = availableRooms;
                    cmbMachines.DataSource = availableMachines;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void PatientCheckInForm_Load(object sender, EventArgs e)
        {
            cmbRooms.DataSource = availableRooms;
            cmbMachines.DataSource = availableMachines;
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            string patientName = txtPatientName.Text;
            string ageString = txtAge.Text;
            string gender = cmbGender.SelectedItem?.ToString();
            string disease = txtDisease.Text;
            string bloodGroup = cmbBloodGroup.SelectedItem?.ToString();
            string selectedRoom = cmbRooms.SelectedItem?.ToString();
            string selectedMachine = cmbMachines.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(patientName) || string.IsNullOrEmpty(ageString) || string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(disease) || string.IsNullOrEmpty(bloodGroup) || string.IsNullOrEmpty(selectedRoom) || string.IsNullOrEmpty(selectedMachine))
            {
                MessageBox.Show("Please enter all required information.", "Incomplete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(ageString, out int age))
            {
                MessageBox.Show("Please enter a valid integer value for age.", "Invalid Age", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    int doctorId = GetRandomDoctorId(connection);

                    int machineId = GetMachineIdByType(connection, selectedMachine);
                    if (machineId == -1)
                    {
                        MessageBox.Show("Selected machine is not available.", "Unavailable Machine", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int roomId = GetRoomIdByNumber(connection, selectedRoom);
                    if (roomId == -1)
                    {
                        MessageBox.Show("Selected room is not available.", "Unavailable Room", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string insertPatientQuery = "INSERT INTO Patients (PatientName, Age, Gender, Disease, BloodGroup, CheckedIn, DoctorId, MachineId, RoomId) " +
                                                "VALUES (@PatientName, @Age, @Gender, @Disease, @BloodGroup, @CheckedIn, @DoctorId, @MachineId, @RoomId)";

                    using (SqlCommand insertPatientCommand = new SqlCommand(insertPatientQuery, connection))
                    {
                        insertPatientCommand.Parameters.AddWithValue("@PatientName", patientName);
                        insertPatientCommand.Parameters.AddWithValue("@Age", age);
                        insertPatientCommand.Parameters.AddWithValue("@Gender", gender);
                        insertPatientCommand.Parameters.AddWithValue("@Disease", disease);
                        insertPatientCommand.Parameters.AddWithValue("@BloodGroup", bloodGroup);
                        insertPatientCommand.Parameters.AddWithValue("@CheckedIn", true);
                        insertPatientCommand.Parameters.AddWithValue("@DoctorId", doctorId);
                        insertPatientCommand.Parameters.AddWithValue("@MachineId", machineId);
                        insertPatientCommand.Parameters.AddWithValue("@RoomId", roomId);

                        int rowsAffected = insertPatientCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            UpdateRoomAvailability(connection, roomId, false);
                            UpdateMachineAvailability(connection, machineId, false);

                            MessageBox.Show("Patient information has been saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show("Failed to save patient information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private List<string> GetAvailableRooms(SqlConnection connection)
        {
            List<string> availableRooms = new List<string>();

            string query = "SELECT RoomNumber FROM Rooms WHERE IsAvailable = 1";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string roomNumber = reader["RoomNumber"].ToString();
                        availableRooms.Add(roomNumber);
                    }
                }
            }

            return availableRooms;
        }

        private List<string> GetAvailableMachines(SqlConnection connection)
        {
            List<string> availableMachines = new List<string>();

            string query = "SELECT MachineType FROM Machines WHERE IsAvailable = 1";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string machineType = reader["MachineType"].ToString();
                        availableMachines.Add(machineType);
                    }
                }
            }

            return availableMachines;
        }

        private int GetRandomDoctorId(SqlConnection connection)
        {
            string query = "SELECT TOP 1 Id FROM Docters ORDER BY NEWID()";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int doctorId))
                {
                    return doctorId;
                }
            }

            throw new Exception("Failed to retrieve a random doctor's ID.");
        }

        private int GetMachineIdByType(SqlConnection connection, string machineType)
        {
            string query = "SELECT MachineID FROM Machines WHERE MachineType = @MachineType AND IsAvailable = 1";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MachineType", machineType);
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int machineId))
                {
                    return machineId;
                }
            }

            return -1;
        }

        private int GetRoomIdByNumber(SqlConnection connection, string roomNumber)
        {
            string query = "SELECT RoomID FROM Rooms WHERE RoomNumber = @RoomNumber AND IsAvailable = 1";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@RoomNumber", roomNumber);
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int roomId))
                {
                    return roomId;
                }
            }

            return -1;
        }

        private void UpdateRoomAvailability(SqlConnection connection, int roomId, bool isAvailable)
        {
            string query = "UPDATE Rooms SET IsAvailable = @IsAvailable WHERE RoomID = @RoomID";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@IsAvailable", isAvailable);
                command.Parameters.AddWithValue("@RoomID", roomId);
                command.ExecuteNonQuery();
            }
        }

        private void UpdateMachineAvailability(SqlConnection connection, int machineId, bool isAvailable)
        {
            string query = "UPDATE Machines SET IsAvailable = @IsAvailable WHERE MachineID = @MachineID";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@IsAvailable", isAvailable);
                command.Parameters.AddWithValue("@MachineID", machineId);
                command.ExecuteNonQuery();
            }
        }

        private void ClearForm()
        {
            txtPatientName.Text = "";
            txtAge.Text = "";
            cmbGender.SelectedItem = null;
            txtDisease.Text = "";
            cmbBloodGroup.SelectedItem = null;
            cmbRooms.SelectedItem = null;
            cmbMachines.SelectedItem = null;
            this.Close(); 
            DashboardForm dashboardForm = new DashboardForm();
            dashboardForm.Show(); 
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); 
            DashboardForm dashboardForm = new DashboardForm();
            dashboardForm.Show(); 
        }
    }

}
