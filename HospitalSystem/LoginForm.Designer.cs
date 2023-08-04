using System.Windows.Forms;

namespace HospitalManagementSystem
{
    public partial class LoginForm : Form
    {
        public TextBox TxtUsername { get; private set; }
        public TextBox TxtPassword { get; private set; }
        public Button BtnLogin { get; private set; }
        private PictureBox pictureBox1;

        partial void InitializeLoginForm();

        public LoginForm()
        {
            InitializeComponent();
            InitializeLoginForm();
        }

        private void InitializeComponent()
        {
            this.TxtUsername = new System.Windows.Forms.TextBox();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.BtnLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            pictureBox1 = new PictureBox();
            pictureBox1.Location = new Point(0, 170);
            pictureBox1.Size = new Size(350, 350);
            this.Controls.Add(pictureBox1);
            pictureBox1.Image = HospitalSystem.Resource1.ezgif_com_video_to_gif;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
           
            this.TxtUsername.Location = new System.Drawing.Point(120, 50);
            this.TxtUsername.Name = "TxtUsername";
            this.TxtUsername.Size = new System.Drawing.Size(200, 20);
            this.TxtUsername.TabIndex = 0;

            this.TxtPassword.Location = new System.Drawing.Point(120, 90);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.Size = new System.Drawing.Size(200, 20);
            this.TxtPassword.TabIndex = 1;
            this.TxtPassword.PasswordChar = '*';

            this.BtnLogin.Location = new System.Drawing.Point(160, 130);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(120, 30);
            this.BtnLogin.TabIndex = 2;
            this.BtnLogin.Text = "Login";
            this.BtnLogin.UseVisualStyleBackColor = true;
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);

          
            Label lblUsername = new Label();
            lblUsername.Location = new System.Drawing.Point(20, 50);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new System.Drawing.Size(100, 20);
            lblUsername.Text = "Username:";

            
            Label lblPassword = new Label();
            lblPassword.Location = new System.Drawing.Point(20, 90);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new System.Drawing.Size(100, 20);
            lblPassword.Text = "Password:";

         
            this.ClientSize = new System.Drawing.Size(350, 450);
            this.Controls.Add(lblUsername);
            this.Controls.Add(lblPassword);
            this.Controls.Add(this.BtnLogin);
            this.Controls.Add(this.TxtPassword);
            this.Controls.Add(this.TxtUsername);
            this.Name = "LoginForm";
            this.Text = "HOSPITAL LOGIN FORM";
            this.ResumeLayout(false);

        }
    }
}
