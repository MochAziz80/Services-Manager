namespace ServiceManager
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblServiceName;
        private TextBox txtServiceName;
        private Label lblExePath;
        private TextBox txtExePath;
        private Button btnBrowse;
        private GroupBox groupBoxActions;
        private Button btnInstall;
        private Button btnDelete;
        private CheckBox chkAutoStart;
        private Label lblPrefix;
        private TextBox txtPrefix;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblServiceName = new Label();
            this.txtServiceName = new TextBox();
            this.lblExePath = new Label();
            this.txtExePath = new TextBox();
            this.btnBrowse = new Button();
            this.groupBoxActions = new GroupBox();
            this.btnInstall = new Button();
            this.btnDelete = new Button();
            this.chkAutoStart = new CheckBox();
            this.lblPrefix = new Label();
            this.txtPrefix = new TextBox();

            this.SuspendLayout();

            // Label Service Name
            this.lblServiceName.Text = "Service Name:";
            this.lblServiceName.Location = new System.Drawing.Point(20, 20);
            this.lblServiceName.AutoSize = true;

            // TextBox Service Name
            this.txtServiceName.Location = new System.Drawing.Point(130, 18);
            this.txtServiceName.Size = new System.Drawing.Size(230, 23);

            // Label Exe Path
            this.lblExePath.Text = "Exe Path:";
            this.lblExePath.Location = new System.Drawing.Point(20, 60);
            this.lblExePath.AutoSize = true;

            // TextBox Exe Path
            this.txtExePath.Location = new System.Drawing.Point(130, 58);
            this.txtExePath.Size = new System.Drawing.Size(230, 23);
            this.txtExePath.ReadOnly = true;

            // Button Browse
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.Location = new System.Drawing.Point(370, 57);
            this.btnBrowse.Click += (s, e) => BrowseFile();

            // CheckBox Auto Start
            this.chkAutoStart.Text = "Auto Start After Install";
            this.chkAutoStart.Location = new System.Drawing.Point(130, 90);
            this.chkAutoStart.AutoSize = true;

            // Label Prefix
            this.lblPrefix.Text = "Prefix (Optional):";
            this.lblPrefix.Location = new System.Drawing.Point(20, 120);
            this.lblPrefix.AutoSize = true;

            // TextBox Prefix
            this.txtPrefix.Location = new System.Drawing.Point(130, 118);
            this.txtPrefix.Size = new System.Drawing.Size(230, 23);

            // GroupBox Actions
            this.groupBoxActions.Text = "Service Actions";
            this.groupBoxActions.Location = new System.Drawing.Point(20, 150);
            this.groupBoxActions.Size = new System.Drawing.Size(430, 70);

            // Button Install
            this.btnInstall.Text = "Install";
            this.btnInstall.Location = new System.Drawing.Point(20, 30);
            this.btnInstall.Size = new System.Drawing.Size(90, 25);
            this.btnInstall.Click += (s, e) =>
            {
                string serviceName = txtServiceName.Text;
                if (!string.IsNullOrEmpty(txtPrefix.Text))
                {
                    serviceName = txtPrefix.Text + serviceName; // Menambahkan prefix jika ada
                }
                RunScCommand($"create {serviceName} binPath= \"{txtExePath.Text}\"");
                if (chkAutoStart.Checked)
                {
                    RunScCommand($"start {serviceName}");
                }
            };

            // Button Delete
            this.btnDelete.Text = "Delete";
            this.btnDelete.Location = new System.Drawing.Point(320, 30);
            this.btnDelete.Size = new System.Drawing.Size(90, 25);
            this.btnDelete.Click += (s, e) =>
            {
                if (!string.IsNullOrEmpty(txtExePath.Text))
                {
                    DeleteServiceByExePath(txtExePath.Text);
                }
                else
                {
                    MessageBox.Show("Please select an executable file first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            // Add buttons and controls to groupbox
            this.groupBoxActions.Controls.AddRange(new Control[] {
                this.btnInstall, this.btnDelete
            });

            // MainForm
            this.ClientSize = new System.Drawing.Size(480, 230);
            this.Controls.Add(this.lblServiceName);
            this.Controls.Add(this.txtServiceName);
            this.Controls.Add(this.lblExePath);
            this.Controls.Add(this.txtExePath);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.chkAutoStart);
            this.Controls.Add(this.lblPrefix);
            this.Controls.Add(this.txtPrefix);
            this.Controls.Add(this.groupBoxActions);
            this.Text = "Service Manager";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
