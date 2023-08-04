namespace HospitalSystem
{
    partial class DashboardForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::HospitalSystem.Resource1._10130;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

            this.ClientSize = new System.Drawing.Size(1019, 613);
            this.Name = "DashboardForm";
            this.Text = "DashboardForm";
            this.Load += new System.EventHandler(this.DashboardForm_Load);
            this.ResumeLayout(false);
            AddButtons();
            CenterLabelText();

        }
        private void CenterLabelText()
        {
            Label lblCenterText = new Label();
            lblCenterText.Text = "Hospital Management System";
            lblCenterText.AutoSize = true;
            lblCenterText.Font = new Font(lblCenterText.Font.FontFamily, 25, FontStyle.Bold | FontStyle.Italic); // Set the font style to bold and italic

            lblCenterText.Left = 250;
            lblCenterText.Top = 50;

            lblCenterText.ForeColor = Color.DarkBlue; 
            lblCenterText.BackColor = Color.Transparent; 
            lblCenterText.TextAlign = ContentAlignment.MiddleCenter; 

            this.Controls.Add(lblCenterText);
        }
    


    private void AddButtons()
        {
            // Create buttons
            Button btnPatientCheckIn = new Button();
            btnPatientCheckIn.Text = "Patient Check-In";
            btnPatientCheckIn.Location = new System.Drawing.Point(50, 100);
            btnPatientCheckIn.Size = new System.Drawing.Size(150, 30);
            btnPatientCheckIn.Click += BtnPatientCheckIn_Click; 
            this.Controls.Add(btnPatientCheckIn);

            Button btnPatientCheckOut = new Button();
            btnPatientCheckOut.Text = "Patient Check-Out";
            btnPatientCheckOut.Location = new System.Drawing.Point(50, 150);
            btnPatientCheckOut.Size = new System.Drawing.Size(150, 30);
            btnPatientCheckOut.Click += BtnPatientCheckOut_Click; 
            this.Controls.Add(btnPatientCheckOut);

            Button btnDoctor = new Button();
            btnDoctor.Text = "Doctor";
            btnDoctor.Location = new System.Drawing.Point(50, 200);
            btnDoctor.Size = new System.Drawing.Size(150, 30);
            btnDoctor.Click += BtnDoctor_Click; 
            this.Controls.Add(btnDoctor);

            Button btnEquipmentRooms = new Button();
            btnEquipmentRooms.Text = "Equipment Rooms";
            btnEquipmentRooms.Location = new System.Drawing.Point(50, 250);
            btnEquipmentRooms.Size = new System.Drawing.Size(150, 30);
            btnEquipmentRooms.Click += BtnEquipmentRooms_Click; 
            this.Controls.Add(btnEquipmentRooms);


            Button btnPatientInfo = new Button();
            btnPatientInfo.Text = "Patient Info";
            btnPatientInfo.Location = new System.Drawing.Point(50, 300);
            btnPatientInfo.Size = new System.Drawing.Size(150, 30);
            btnPatientInfo.Click += BtnPatientInfo_Click; 
            this.Controls.Add(btnPatientInfo);

            Button btnUpdatePatient = new Button();
            btnUpdatePatient.Text = "Update Patient";
            btnUpdatePatient.Location = new System.Drawing.Point(50, 350);
            btnUpdatePatient.Size = new System.Drawing.Size(150, 30);
            btnUpdatePatient.Click += BtnUpdatePatient_Click; 

            this.Controls.Add(btnUpdatePatient);
            Button btnQuit = new Button();
            btnQuit.Text = "Quit";
            btnQuit.Location = new System.Drawing.Point(50, 400);
            btnQuit.Size = new System.Drawing.Size(150, 30);
            btnQuit.Click += BtnQuit_Click; 
            this.Controls.Add(btnQuit);

        }

        #endregion
    }
}