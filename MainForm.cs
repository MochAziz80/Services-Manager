namespace ServiceManager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        // Function to browse for .exe file
        private void BrowseFile()
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Executable Files (*.exe)|*.exe";
                dialog.Title = "Select .exe File";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtExePath.Text = dialog.FileName;
                }
            }
        }

        // Function to execute sc.exe command
        private void RunScCommand(string arguments)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("sc", arguments)
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    Verb = "runas" // to request administrator access
                };

                using (Process process = Process.Start(psi))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    MessageBox.Show(output, "SC Output", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to execute the command.\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Function to delete service associated with exePath
        private void DeleteServiceByExePath(string exePath)
        {
            try
            {
                ProcessStartInfo listServicesInfo = new ProcessStartInfo("sc", "query state= all")
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process listProcess = Process.Start(listServicesInfo))
                {
                    string output = listProcess.StandardOutput.ReadToEnd();
                    listProcess.WaitForExit();

                    var lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var line in lines)
                    {
                        if (line.Trim().StartsWith("SERVICE_NAME:"))
                        {
                            string serviceName = line.Split(':')[1].Trim();

                            ProcessStartInfo getConfigInfo = new ProcessStartInfo("sc", $"qc {serviceName}")
                            {
                                RedirectStandardOutput = true,
                                UseShellExecute = false,
                                CreateNoWindow = true
                            };

                            using (Process configProcess = Process.Start(getConfigInfo))
                            {
                                string configOutput = configProcess.StandardOutput.ReadToEnd();
                                configProcess.WaitForExit();

                                if (configOutput.ToLower().Contains(exePath.ToLower()))
                                {
                                    // Match found! Delete the service
                                    RunScCommand($"delete {serviceName}");
                                }
                            }
                        }
                    }

                    MessageBox.Show("The service associated with the exe file has been successfully deleted (if found).", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Function to install the service
        private void InstallService()
        {
            string serviceName = txtServiceName.Text;
            string prefix = txtPrefix.Text.Trim();  // Get the prefix from the TextBox

            // If prefix is provided, add it to the service name
            if (!string.IsNullOrEmpty(prefix))
            {
                serviceName = prefix + serviceName;
            }

            // Check if serviceName or exePath is empty
            if (string.IsNullOrEmpty(serviceName) || string.IsNullOrEmpty(txtExePath.Text))
            {
                MessageBox.Show("Please enter the service name and select an executable file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Run the SC command to create the service
            RunScCommand($"create {serviceName} binPath= \"{txtExePath.Text}\" start= auto");
        }

        // Event handler for the Install button
        private void btnInstall_Click(object sender, EventArgs e)
        {
            InstallService();
        }
    }
}
