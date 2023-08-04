using System;
using System.Windows.Forms;

namespace HospitalSystem
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
          
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {

        }
        private void BtnPatientCheckIn_Click(object sender, EventArgs e)
        {
            this.Close();
            // Open PatientCheckInForm
            PatientCheckInForm patientCheckInForm = new PatientCheckInForm();
            patientCheckInForm.ShowDialog();

        }

        private void BtnPatientCheckOut_Click(object sender, EventArgs e)
        {
            this.Close();
            // Open PatientCheckOutForm
            PatientCheckOutForm patientCheckOutForm = new PatientCheckOutForm();
            patientCheckOutForm.ShowDialog();

        }

        private void BtnDoctor_Click(object sender, EventArgs e)
        {
            this.Close();
            // Open DoctorForm
            DoctorForm doctorForm = new DoctorForm();
            doctorForm.ShowDialog();

        }

        private void BtnEquipmentRooms_Click(object sender, EventArgs e)
        {
            this.Close();
            // Open EquipmentRoomsForm
            EquipmentRoomsForm equipmentRoomsForm = new EquipmentRoomsForm();
            equipmentRoomsForm.ShowDialog();
        }
        private void BtnPatientInfo_Click(object sender, EventArgs e)
        {
            this.Close();
            // Open PatientInfoForm
            PatientInfoForm patientInfoForm = new PatientInfoForm();
            patientInfoForm.ShowDialog();

          
        }
        private void BtnUpdatePatient_Click(object sender, EventArgs e)
        {

            this.Close();
            PatientUpdateForm updateForm = new PatientUpdateForm();
            updateForm.ShowDialog();
        }
        private void BtnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }


    }
}
