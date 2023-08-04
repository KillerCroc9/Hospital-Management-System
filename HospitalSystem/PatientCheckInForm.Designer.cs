using System;
using System.Windows.Forms;

namespace HospitalSystem
{
    public partial class PatientCheckInForm : Form
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtPatientName;
        private TextBox txtAge;
        private ComboBox cmbGender;
        private TextBox txtDisease;
        private ComboBox cmbBloodGroup;
        private ComboBox cmbMachines;
        private ComboBox cmbRooms;
        private Button btnCheckIn;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtPatientName = new System.Windows.Forms.TextBox();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.txtDisease = new System.Windows.Forms.TextBox();
            this.cmbBloodGroup = new System.Windows.Forms.ComboBox();
            this.cmbMachines = new System.Windows.Forms.ComboBox();
            this.cmbRooms = new System.Windows.Forms.ComboBox();
            this.btnCheckIn = new System.Windows.Forms.Button();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.lblGender = new System.Windows.Forms.Label();
            this.lblDisease = new System.Windows.Forms.Label();
            this.lblBloodGroup = new System.Windows.Forms.Label();
            this.lblMachine = new System.Windows.Forms.Label();
            this.lblRoom = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // 
            this.txtPatientName.Location = new System.Drawing.Point(170, 30);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.Size = new System.Drawing.Size(200, 27);
            this.txtPatientName.TabIndex = 5;
            // 
            // 
            // 
            this.txtAge.Location = new System.Drawing.Point(170, 70);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(200, 27);
            this.txtAge.TabIndex = 6;
            // 
            // 
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Items.AddRange(new object[] {
            "Male",
            "Female",
            "Other"});
            this.cmbGender.Location = new System.Drawing.Point(170, 110);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(200, 28);
            this.cmbGender.TabIndex = 7;
            // 
            // txtDisease
            // 
            this.txtDisease.Location = new System.Drawing.Point(170, 150);
            this.txtDisease.Name = "txtDisease";
            this.txtDisease.Size = new System.Drawing.Size(200, 27);
            this.txtDisease.TabIndex = 8;
            // 
            // cmbBloodGroup
            // 
            this.cmbBloodGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBloodGroup.FormattingEnabled = true;
            this.cmbBloodGroup.Items.AddRange(new object[] {
            "A+",
            "A-",
            "B+",
            "B-",
            "AB+",
            "AB-",
            "O+",
            "O-"});
            this.cmbBloodGroup.Location = new System.Drawing.Point(170, 190);
            this.cmbBloodGroup.Name = "cmbBloodGroup";
            this.cmbBloodGroup.Size = new System.Drawing.Size(200, 28);
            this.cmbBloodGroup.TabIndex = 9;
            // 
            // 
            this.cmbMachines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMachines.FormattingEnabled = true;
            this.cmbMachines.Location = new System.Drawing.Point(170, 230);
            this.cmbMachines.Name = "cmbMachines";
            this.cmbMachines.Size = new System.Drawing.Size(200, 28);
            this.cmbMachines.TabIndex = 11;
            // 
            // 
            this.cmbRooms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRooms.FormattingEnabled = true;
            this.cmbRooms.Location = new System.Drawing.Point(170, 270);
            this.cmbRooms.Name = "cmbRooms";
            this.cmbRooms.Size = new System.Drawing.Size(200, 28);
            this.cmbRooms.TabIndex = 13;
            // 
            // 
            this.btnCheckIn.Location = new System.Drawing.Point(150, 330);
            this.btnCheckIn.Name = "btnCheckIn";
            this.btnCheckIn.Size = new System.Drawing.Size(100, 30);
            this.btnCheckIn.TabIndex = 10;
            this.btnCheckIn.Text = "Check-In";
            this.btnCheckIn.UseVisualStyleBackColor = true;
            this.btnCheckIn.Click += new System.EventHandler(this.btnCheckIn_Click);
            // 
            // 
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.Location = new System.Drawing.Point(50, 30);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(101, 20);
            this.lblPatientName.TabIndex = 0;
            this.lblPatientName.Text = "Patient Name:";
            // 
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Location = new System.Drawing.Point(50, 70);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(39, 20);
            this.lblAge.TabIndex = 1;
            this.lblAge.Text = "Age:";
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Location = new System.Drawing.Point(50, 110);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(60, 20);
            this.lblGender.TabIndex = 2;
            this.lblGender.Text = "Gender:";
            // 
            // 
            this.lblDisease.AutoSize = true;
            this.lblDisease.Location = new System.Drawing.Point(50, 150);
            this.lblDisease.Name = "lblDisease";
            this.lblDisease.Size = new System.Drawing.Size(63, 20);
            this.lblDisease.TabIndex = 3;
            this.lblDisease.Text = "Disease:";
            // 
     
            this.lblBloodGroup.AutoSize = true;
            this.lblBloodGroup.Location = new System.Drawing.Point(50, 190);
            this.lblBloodGroup.Name = "lblBloodGroup";
            this.lblBloodGroup.Size = new System.Drawing.Size(97, 20);
            this.lblBloodGroup.TabIndex = 4;
            this.lblBloodGroup.Text = "Blood Group:";
       
            this.lblMachine.AutoSize = true;
            this.lblMachine.Location = new System.Drawing.Point(50, 230);
            this.lblMachine.Name = "lblMachine";
            this.lblMachine.Size = new System.Drawing.Size(68, 20);
            this.lblMachine.TabIndex = 10;
            this.lblMachine.Text = "Machine:";
        
            this.lblRoom.AutoSize = true;
            this.lblRoom.Location = new System.Drawing.Point(50, 270);
            this.lblRoom.Name = "lblRoom";
            this.lblRoom.Size = new System.Drawing.Size(52, 20);
            this.lblRoom.TabIndex = 12;
            this.lblRoom.Text = "Room:";
   
            this.btnBack.Location = new System.Drawing.Point(50, 330);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 30);
            this.btnBack.TabIndex = 14;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
     
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::HospitalSystem.Resource1.wallpaperflare_com_wallpaper__2_;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(836, 500);
            this.Controls.Add(this.lblPatientName);
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.lblGender);
            this.Controls.Add(this.lblDisease);
            this.Controls.Add(this.lblBloodGroup);
            this.Controls.Add(this.lblMachine);
            this.Controls.Add(this.lblRoom);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.txtPatientName);
            this.Controls.Add(this.txtAge);
            this.Controls.Add(this.cmbGender);
            this.Controls.Add(this.txtDisease);
            this.Controls.Add(this.cmbBloodGroup);
            this.Controls.Add(this.cmbMachines);
            this.Controls.Add(this.cmbRooms);
            this.Controls.Add(this.btnCheckIn);
            this.Name = "PatientCheckInForm";
            this.Text = "Patient Check-In";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Label lblPatientName;
        private Label lblAge;
        private Label lblGender;
        private Label lblDisease;
        private Label lblBloodGroup;
        private Label lblMachine;
        private Label lblRoom;
        private Button btnBack;
    }
}
